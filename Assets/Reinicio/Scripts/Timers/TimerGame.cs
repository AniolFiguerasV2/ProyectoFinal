using TMPro;
using UnityEngine;

public class TimerGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainTime;
    public static TimerGame instance;
    public GameObject panelLose;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
        }
        else if (remainTime < 0) 
        { 
            remainTime = 0;
            timerText.color = Color.red;
            panelLose.SetActive(true);
            Time.timeScale = 0f;

        }
        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddTime(float seconds)
    {
        remainTime += seconds;
    }

    public void SubtractTime(float seconds)
    {
        remainTime -= seconds;

        if(remainTime < 0)
        {
            remainTime = 0;
        }
    }
}
