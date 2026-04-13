using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class VehicleSpawnManager : MonoBehaviour
{
    [Header("Vehicle Settings")]
    [SerializeField] private List<GameObject> vehiclePrefabs;

    [SerializeField] private int maxVehicles = 5;

    [Header("Spawn Timing")]
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 5f;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private List<GameObject> activeVehicles = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            TrySpawnVehicle();
        }
    }

    void TrySpawnVehicle()
    {
        CleanList();

        if (activeVehicles.Count >= maxVehicles)
            return;

        if (spawnPoints.Count == 0 || vehiclePrefabs.Count == 0)
            return;

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject prefab = vehiclePrefabs[Random.Range(0, vehiclePrefabs.Count)];

        GameObject vehicle = Instantiate(prefab, spawn.position, spawn.rotation);

        activeVehicles.Add(vehicle);
    }

    void CleanList()
    {
        for (int i = activeVehicles.Count - 1; i >= 0; i--)
        {
            if (activeVehicles[i] == null)
            {
                activeVehicles.RemoveAt(i);
            }
        }
    }
}
