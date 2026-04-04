using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;

    public int easyPoints = 100;
    public int normalPoints = 250;
    public int hardPoints = 500;

    public float maxMultiplier = 2f;

    private int score;
    public int Score => score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }
    private void Start()
    {
        UpdateUI();
    }

    public void AddPatientScore(PatientDeathTime patient)
    {
        int basePoints = GetBasePoints(patient);

        float remainingPercent = (patient.Lifetime - patient.Timer) / patient.Lifetime;

        float multiplier = 1f + remainingPercent;

        int finalPoints = Mathf.RoundToInt(basePoints * multiplier);

        score += finalPoints;

        UpdateUI();
    }

    public void PenalizePatientDeath(PatientDeathTime patient)
    {
        int basePoints = GetBasePoints(patient);

        int penalty = Mathf.RoundToInt(basePoints * 0.5f);

        score -= penalty;

        if (score < 0)
        {
            score = 0;
        }

        UpdateUI();
    }

    private int GetBasePoints(PatientDeathTime patient)
    {
        float lifetime = patient.Lifetime;

        if (Mathf.Approximately(lifetime, patient.easyTime))
        {
            return easyPoints;
        }

        if (Mathf.Approximately(lifetime, patient.normalTime))
        {
            return normalPoints;
        }
            
        return hardPoints;
    }
    private void UpdateUI()
    {
        scoreText.text = score.ToString();
    }
}
