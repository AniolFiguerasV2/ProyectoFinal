using UnityEngine;

public class PlayersMiddleCalculate : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject middleObject;

    public float pickedHeight = 2.0f;

    public float maxDistance = 5f;

    void Update()
    {
        Vector3 pos1 = player1.transform.position;
        Vector3 pos2 = player2.transform.position;

        float distance = Vector3.Distance(pos1, pos2);

        if (distance > maxDistance)
        {
            Vector3 middle = (pos1 + pos2) / 2f;
            Vector3 dir = (pos1 - pos2).normalized;

            player1.transform.position = middle + dir * (maxDistance / 2f);
            player2.transform.position = middle - dir * (maxDistance / 2f);

            pos1 = player1.transform.position;
            pos2 = player2.transform.position;
        }

        middleObject.transform.position = (pos1 + pos2)/2f;

        middleObject.transform.position = new Vector3(middleObject.transform.position.x, pickedHeight, middleObject.transform.position.z);

        Vector3 direction = pos1 - pos2;
        middleObject.transform.rotation = Quaternion.LookRotation(direction);
    }
}
