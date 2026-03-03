using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int maxNPCs = 3;
    [SerializeField] private float spawnRadius = 40f;
    [SerializeField] private float spawnHeight = 50f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxSlope = 45f;

    private int currentNPCs;

    private void Start()
    {
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

                    npc.AddComponent<PatientDeathTime>().spawner = this;

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
