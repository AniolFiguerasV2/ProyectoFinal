using UnityEngine;

public class PutPatientStrecher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Strecher"))
        {
            Transform camilla = collision.transform;
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            transform.position = camilla.position + offset;
            transform.SetParent(camilla);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hospital"))
        {
            Destroy(gameObject);
            //Logica de sumar tiempo al contar del tiempo
            //Logica de sumar puntuacion a la score
        }
    }
}
