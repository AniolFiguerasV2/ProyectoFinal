using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PacienteInfo : MonoBehaviour
{
    public Image spritePaciente;
    public Image barhealth;

    public float lifeInseconds;
    public float currentlifeInseconds;

    public TextMeshProUGUI timerText;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        currentlifeInseconds -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentlifeInseconds / 60);
        int seconds = Mathf.FloorToInt(currentlifeInseconds % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
