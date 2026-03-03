using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PutPacientStrecher : MonoBehaviour
{
    public Transform refcamilla;
    public Transform salaespera;

    public GameObject panelwin;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Strecher"))
        {
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            gameObject.transform.position = refcamilla.position + offset;
            gameObject.transform.SetParent(refcamilla);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hospital"))
        {
            panelwin.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
