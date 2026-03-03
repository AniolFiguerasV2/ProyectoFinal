using UnityEngine;

public class PatientDeathTime : MonoBehaviour
{
    [Header("Porcentaje de dificultad")]
    public float easyDificulty = 30f;
    public float normalDificulty = 60f;
    public float hardDificulty = 10f;

    [Header("Tiempos por dificultad (segundos)")]
    public float easyTime = 600f;
    public float normalTime = 300f;
    public float hardTime = 150f;

    private float lifetime;
    private float timer = 0f;

    public PatientSpawner spawner;

    private void Start()
    {

        int numeroRandom = Random.Range(1, 101);

        if (numeroRandom >= 1 && numeroRandom <= easyDificulty)
        {
            lifetime = easyTime;
        }
        else if (numeroRandom >= (easyDificulty + 1) && numeroRandom <= (easyDificulty + normalDificulty))
        {
            lifetime = normalTime;
        }
        else if (numeroRandom >= (easyDificulty + normalDificulty + 1) && numeroRandom <= (easyDificulty + normalDificulty + hardDificulty))
        {
            lifetime = hardTime;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            spawner.NotifyNPCDeath();
            Destroy(gameObject);
        }
    }
}
