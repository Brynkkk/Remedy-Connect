using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject patientPrefab;

    public float spawnInterval = 5f;
    public float angerTimer = 10f;

    private GameObject[] spawnedPatients;
    private float spawnTimer;

    [SerializeField]
    private int totalSpawnedPatients;

    void Start()
    {
        spawnedPatients = new GameObject[spawnPositions.Length];
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            TrySpawnPatient();
            spawnTimer = spawnInterval;
        }
    }

    private void TrySpawnPatient()
    {
        int emptySpot = FindEmptySpot();
        if (emptySpot != -1)
        {
            SpawnPatientAt(emptySpot);
        }
        else
        {
            Debug.Log("No empty spots available.");
        }
    }

    private int FindEmptySpot()
    {
        for (int i = 0; i < spawnedPatients.Length; i++)
        {
            if (spawnedPatients[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    private void SpawnPatientAt(int index)
    {
        if (index < 0 || index >= spawnPositions.Length) return;

        GameObject newPatient = Instantiate(patientPrefab, spawnPositions[index].position, Quaternion.identity);
        spawnedPatients[index] = newPatient;

        totalSpawnedPatients++;

        Patient patientScript = newPatient.GetComponent<Patient>();
        if (patientScript != null)
        {
            patientScript.Initialize(index, angerTimer, OnPatientDespawned);
        }
    }

    public void DespawnPatientManually(int index)
    {
        if (index < 0 || index >= spawnedPatients.Length) return;

        if (spawnedPatients[index] != null)
        {
            Destroy(spawnedPatients[index]);
            spawnedPatients[index] = null;
        }
    }

    private void OnPatientDespawned(int index)
    {
        if (index < 0 || index >= spawnedPatients.Length) return;

        spawnedPatients[index] = null;
    }
}
