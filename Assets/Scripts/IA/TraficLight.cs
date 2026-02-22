using UnityEngine;
using System.Collections;

public class TraficLight : MonoBehaviour
{
    public enum TrafficState
    {
        Red, 
        Yellow,
        Green
    }

    public TrafficState currentState;

    [Header("Puntos de Movimiento de Npc")]
    public GameObject[] wayPoints;

    [Header("Duracion de las luces")]
    public float redTime = 5f;
    public float yellowTime = 2f;
    public float greenTime = 5f;

    void Start()
    {
        StartCoroutine(TrafficCycle());
    }
    
    IEnumerator TrafficCycle()
    {
        while (true)
        {
            //Red
            currentState = TrafficState.Red;
            SetWaypointActive(false);
            yield return new WaitForSeconds(redTime);

            //Yellow
            currentState = TrafficState.Yellow;
            yield return new WaitForSeconds(yellowTime);

            currentState = TrafficState.Green;
            SetWaypointActive(true);
            yield return new WaitForSeconds(greenTime);
        }
       
    }

    void SetWaypointActive(bool active)
    {
        foreach (GameObject wp in wayPoints)
        {
            if (wp != null)
            {
                wp.SetActive(active);
            }
        }
    }
}
