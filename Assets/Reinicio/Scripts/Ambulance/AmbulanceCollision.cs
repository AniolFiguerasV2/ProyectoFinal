using UnityEngine;

public class AmbulanceCollision : MonoBehaviour
{
    private float lastHitTime = 0f;
    public float hitCooldown = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastHitTime < hitCooldown)
            return;

        if (collision.gameObject.CompareTag("Car") ||collision.gameObject.CompareTag("Building") ||collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("He collisionado");
            lastHitTime = Time.time;
            TimerGame.instance.SubtractTime(20f);
        }
    }
}
