using UnityEngine;
using System.Collections;

public class TraficLights : MonoBehaviour
{
    public enum TrafficState
    {
        Red, 
        Yellow,
        Green
    }


    public TrafficState currentState;

    [Header("Puntos de Movimiento de personas")]
    public GameObject[] waypoints;

    [Header("Duracion de las luces")]
    public float redtime = 5f;
    public float yellowtime = 2f;
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
            yield return new WaitForSeconds(redtime);

            //Yellow
            currentState = TrafficState.Yellow;
            yield return new WaitForSeconds(yellowtime);

            currentState = TrafficState.Green;
            SetWaypointActive(true);
            yield return new WaitForSeconds(greenTime);
        }

    }

    void SetWaypointActive(bool active)
    {
        foreach (GameObject wp in waypoints)
        {
            if (wp != null)
            {
                wp.SetActive(active);
            }
        }
    }
}
