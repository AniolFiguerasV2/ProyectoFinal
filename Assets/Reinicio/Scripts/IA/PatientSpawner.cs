using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private PatientDeathTime npcPrefab;
    [SerializeField] private int maxNPCs = 3;
    [SerializeField] private float spawnRadius = 40f;
    [SerializeField] private float spawnHeight = 100f;
    //[SerializeField] private LayerMask defaultLayer;
    [SerializeField] private float maxSlope = 45f;

    public List<PatientDeathTime> patients = new();

    public Transform refTrCamilla;
    public Transform refTrSalaEspera;

    public static PatientSpawner Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this);
    }

    private void Start()
    {
        RefillNPCS();
    }
    private void TrySpawnNPC()
    {
        Vector3 randomPos = GetRandomPoint();

        if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, spawnHeight * 2f) && hit.collider.gameObject.layer == 11)
        {
            float slope = Vector3.Angle(hit.normal, Vector3.up);

            if (slope <= maxSlope)
            {
                Vector3 spawnPosition = hit.point + Vector3.up * 1f;

                PatientDeathTime npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);

                npc.spawner = this;

                patients.Add(npc);

                return;
            }
        }
    }
    private Vector3 GetRandomPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        return new Vector3(randomCircle.x, spawnHeight, randomCircle.y) + transform.position;
    }

    public void NotifyNPCDeath(PatientDeathTime deathNPC)
    {
        patients.Remove(deathNPC);

        RefillNPCS();
    }

    private void RefillNPCS()
    {
        while (patients.Count < maxNPCs)
        {
            TrySpawnNPC();
        }
    }
}
/*
 * Este script sirve para que spawnen 3 pacientes random por el mapa.
 * 
 * Para que funcione este script se tiene que assiganar a un Game Object vacio en la escena y rellenar los campos con un prefab del paciente que solo tiene que tener el
 * PutPatientStretcher. Es necessario que tambien exsista el script PatientDeathTime sino el script va a petar.
 */