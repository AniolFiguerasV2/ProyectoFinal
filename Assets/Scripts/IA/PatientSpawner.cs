using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int maxNPCs = 3;
    [SerializeField] private float spawnRadius = 40f;
    [SerializeField] private float spawnHeight = 100f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxSlope = 45f;

    public List<PatientDeathTime> patients;

    private int currentNPCs;

    private void Start()
    {
        patients = new List<PatientDeathTime>();
        for (int i = 0; i < maxNPCs; i++)
        {
            SpawnNPC();
        }
    }
    private void SpawnNPC()
    {
        int attempts = 0;
        int maxAttempts = 10;

        while (attempts < maxAttempts)
        {
            Vector3 randomPos = GetRandomPoint();

            if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, spawnHeight * 2f, groundLayer))
            {
                float slope = Vector3.Angle(hit.normal, Vector3.up);

                if (slope <= maxSlope)
                {
                    Vector3 spawnPosition = hit.point;

                    GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);

                    currentNPCs++;

                    PatientDeathTime patientdtComp =  npc.AddComponent<PatientDeathTime>();

                    patientdtComp.spawner = this;

                    patients.Add(patientdtComp);

                    return;
                }
            }

            attempts++;
        }
    }
    private Vector3 GetRandomPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        return new Vector3(randomCircle.x, spawnHeight, randomCircle.y) + transform.position;
    }
    public void NotifyNPCDeath()
    {
        currentNPCs--;

        if (currentNPCs < maxNPCs)
        {
            SpawnNPC();
        }
    }
}
/*
 * Este script sirve para que spawnen 3 pacientes random por el mapa.
 * 
 * Para que funcione este script se tiene que assiganar a un Game Object vacio en la escena y rellenar los campos con un prefab del paciente que solo tiene que tener el
 * PutPatientStretcher. Es necessario que tambien exsista el script PatientDeathTime sino el script va a petar.
 */