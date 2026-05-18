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

        bool bothHolding =
            MoveObject.Instance.handle1.IsBeingHeld &&
            MoveObject.Instance.handle2.IsBeingHeld;

        if (bothHolding)
        {
            Vector3 difference = pos1 - pos2;

            float distance = difference.magnitude;

            if (distance > maxDistance)
            {
                Vector3 direction = difference.normalized;

                Vector3 middle = (pos1 + pos2) * 0.5f;

                pos1 = middle + direction * (maxDistance * 0.5f);
                pos2 = middle - direction * (maxDistance * 0.5f);

                player1.transform.position = pos1;
                player2.transform.position = pos2;
            }
        }

        middleObject.transform.position = (pos1 + pos2) / 2f;

        middleObject.transform.position = new Vector3(
            middleObject.transform.position.x,
            pickedHeight,
            middleObject.transform.position.z
        );

        Vector3 directionLook = pos1 - pos2;

        if (directionLook != Vector3.zero)
        {
            middleObject.transform.rotation =
                Quaternion.LookRotation(directionLook);
        }
    }
}
