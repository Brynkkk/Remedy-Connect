using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    [HideInInspector] public PatientSpawner spawner; // Assigned dynamically
    [HideInInspector] public int spawnIndex; // Area index where this patient was spawned

    [Header("Dialogue Box Setup")]
    public GameObject dialogueBoxPrefab; // Assign the Dialogue Box Prefab in Inspector
    private GameObject dialogueBoxInstance; // Instance of the dialogue box
    private TextMeshProUGUI dialogueText; // Text component to update dialogue

    [Header("Dialogue Data")]
    public string[] dialogues; // Predefined dialogues for this patient

    [Header("Anger Timer Settings")]
    public Image angerBar; // Assign this in the Inspector
    private float angerTime; // Total time before patient gets angry
    private float angerCountdown;

    [Header("Score and HP Settings")]
    public int points = 10;
    public int hpLoss = 1;

    void Start()
    {
        // Initialize dialogue
        dialogueBoxInstance = Instantiate(dialogueBoxPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        dialogueBoxInstance.transform.SetParent(transform); // Make it a child of the patient
        dialogueText = dialogueBoxInstance.GetComponentInChildren<TextMeshProUGUI>();

        // Set initial dialogue
        SetDialogue(dialogues[Random.Range(0, dialogues.Length)]);

        // Initialize anger timer
        angerTime = GetAngerTimer(spawner.totalSpawnedPatients);
        angerCountdown = angerTime;
        UpdateAngerBar();
    }

    void Update()
    {
        // Update anger timer
        if (angerCountdown > 0)
        {
            angerCountdown -= Time.deltaTime;
            UpdateAngerBar();

            // Update anger bar color
            float percentage = angerCountdown / angerTime;
            if (percentage > 0.75f)
            {
                angerBar.color = Color.green;
            }
            else if (percentage > 0.3f)
            {
                angerBar.color = Color.yellow;
            }
            else
            {
                angerBar.color = Color.red;
            }
        }
        else
        {
            OnAngerTimerExpired();
        }
    }

    public void SetDialogue(string text)
    {
        if (dialogueText != null)
        {
            dialogueText.text = text;
        }
    }

    void UpdateAngerBar()
    {
        if (angerBar != null)
        {
            angerBar.fillAmount = angerCountdown / angerTime;
        }
    }

    float GetAngerTimer(int totalSpawns)
    {
        if (totalSpawns <= 3) return 120f;
        if (totalSpawns <= 10) return 90f;
        if (totalSpawns <= 25) return 60f;
        if (totalSpawns <= 50) return 45f;
        if (totalSpawns <= 75) return 30f;
        return 20f;
    }

    void OnAngerTimerExpired()
    {
        Debug.Log("Patient got angry and left!");
        spawner.FreeArea(spawnIndex); // Free the spawn area
        Destroy(dialogueBoxInstance); // Clean up dialogue box
        Destroy(gameObject); // Remove patient
    }

    public void Leave(bool isCorrect)
    {
        spawner.FreeArea(spawnIndex); // Free the spawn area
        Destroy(dialogueBoxInstance); // Clean up dialogue box

        if (isCorrect)
        {
            Debug.Log("Correct medicine delivered! Points awarded: " + points);
            // Add points logic here
        }
        else
        {
            Debug.Log("Incorrect medicine delivered! HP lost: " + hpLoss);
            // Deduct HP logic here
        }

        Destroy(gameObject); // Remove patient
    }
}
