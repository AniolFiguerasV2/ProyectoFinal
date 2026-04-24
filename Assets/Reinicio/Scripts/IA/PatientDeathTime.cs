using UnityEngine;

public class PatientDeathTime : MonoBehaviour
{
    [Header("Porcentaje de dificultad")]
    public float easyDificulty = 3f;
    public float normalDificulty = 6f;
    public float hardDificulty = 1f;

    [Header("Tiempos por dificultad (segundos)")]
    public bool debugTime = false;
    public float easyTime = 600f;
    public float normalTime = 300f;
    public float hardTime = 150f;

    private float lifetime;
    public float Lifetime => lifetime;

    private float timer = 0f;
    public float Timer => timer;

    public PatientSpawner spawner;

    private void Start()
    {
        float numeroRandom = Random.Range(0f, easyDificulty + normalDificulty + hardDificulty);

        if (numeroRandom <= easyDificulty)
        {
            lifetime = easyTime;
        }
        else if (numeroRandom <= easyDificulty + normalDificulty)
        {
            lifetime = normalTime;
        }
        else
        {
            lifetime = hardTime;
        }

        if (debugTime)
            lifetime *= 0.01f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            ScoreManager.Instance.PenalizePatientDeath(this);
            GameManager.Instance.PatientDied();
            spawner.NotifyNPCDeath(this);
            Destroy(gameObject);
        }
    }

    public void SetLifetime(float newLifetime)
    {
        lifetime = newLifetime;
    }

    public void SetTimer(float newTimer)
    {
        timer = newTimer;
    }
}
