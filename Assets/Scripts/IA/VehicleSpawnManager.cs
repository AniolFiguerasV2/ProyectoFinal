using UnityEngine;
using System.Collections.Generic;

public class VehicleSpawnManager : MonoBehaviour
{
    [Header("Vehicle Settings")]
    [SerializeField] private GameObject vehiclePrefab;

    [SerializeField] private int maxVehicles = 5;

    [SerializeField] private float spawnInterval = 5f;

    [SerializeField] private List<Transform> viewPoints;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private List<GameObject> activeVehicles = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnVehicle), 2f, spawnInterval);
    }

    void SpawnVehicle()
    {
        CleanList();

        if (activeVehicles.Count >= maxVehicles)
            return;
        if (spawnPoints.Count == 0)
            return;

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject vehicle = Instantiate(vehiclePrefab, spawn.position, spawn.rotation);

        VehicleMovements movements = vehicle.GetComponent<VehicleMovements>();

        if(movements != null)
        {
            movements.SetWayPoints(viewPoints);
        }

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
