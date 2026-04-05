using UnityEngine;

public class PutPatientStrecher : MonoBehaviour
{
    private MoveObject currentMove;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Strecher"))
        {
            MoveObject move = collision.gameObject.GetComponent<MoveObject>();
            if(move != null)
            {
                move.hasPatient = true;
                currentMove = move;
            }
            Transform camilla = collision.transform;
            Transform slot = camilla.Find("ZonaPaciente");
            if (slot != null)
            {
                transform.SetParent(slot);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.SetParent(camilla);
                transform.localPosition = new Vector3(0f, 0.5f, 0f);
            }

            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hospital"))
        {
            if(currentMove != null)
            {
                currentMove.hasPatient = false;
                currentMove.alreadyScored = false;
            }
            Destroy(gameObject);
            //Logica de sumar tiempo al contar del tiempo
            ScoreManager.Instance.AddPoints(300);
            
            
        }
    }
}
