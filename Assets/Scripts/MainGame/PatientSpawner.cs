using System.Collections.Generic;
using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [Header("Spawn Area Setup")]
    public Transform[] spawnAreas; // Areas to spawn patients
    private bool[] isAreaOccupied; // Tracks if an area is occupied

    [Header("Patient Setup")]
    public GameObject[] patientPrefabs; // Assign patient prefabs

    [Header("Timer Settings")]
    public float spawnTimer = 120f; // Initial spawn timer
    private float spawnCountdown;
    public int totalSpawnedPatients = 0;

    void Start()
    {
        isAreaOccupied = new bool[spawnAreas.Length];
        spawnCountdown = GetSpawnTimer();
    }

    void Update()
    {
        // Reset timer if all spawn areas are empty
        if (AreAllAreasEmpty())
        {
            spawnCountdown = 0f;
        }

        // Countdown and spawn if timer reaches 0
        if (spawnCountdown > 0)
        {
            spawnCountdown -= Time.deltaTime;
        }
        else
        {
            SpawnPatient();
            spawnCountdown = GetSpawnTimer();
        }
    }

    void SpawnPatient()
    {
        List<int> availableAreas = new List<int>();
        for (int i = 0; i < isAreaOccupied.Length; i++)
        {
            if (!isAreaOccupied[i]) availableAreas.Add(i);
        }

        if (availableAreas.Count > 0)
        {
            int chosenIndex = availableAreas[Random.Range(0, availableAreas.Count)];
            GameObject newPatient = Instantiate(
                patientPrefabs[Random.Range(0, patientPrefabs.Length)],
                spawnAreas[chosenIndex].position,
                Quaternion.identity
            );

            isAreaOccupied[chosenIndex] = true;
            totalSpawnedPatients++;

            // Assign spawner data to patient
            Patient patient = newPatient.GetComponent<Patient>();
            patient.spawner = this;
            patient.spawnIndex = chosenIndex;
        }
    }

    bool AreAllAreasEmpty()
    {
        foreach (bool occupied in isAreaOccupied)
        {
            if (occupied) return false;
        }
        return true;
    }

    float GetSpawnTimer()
    {
        if (totalSpawnedPatients <= 3) return 120f;
        if (totalSpawnedPatients <= 5) return 90f;
        if (totalSpawnedPatients <= 10) return 75f;
        if (totalSpawnedPatients <= 15) return 60f;
        if (totalSpawnedPatients <= 20) return 45f;
        if (totalSpawnedPatients <= 25) return 30f;
        if (totalSpawnedPatients <= 50) return 10f;
        return 5f;
    }

    public void FreeArea(int index)
    {
        isAreaOccupied[index] = false;
    }
}
