using UnityEditor;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Handles handle1;
    public Handles handle2;

    public GameObject strecher;
    public GameObject spawpoint;

    public bool ZonaCarga = false;

    public bool IsInside = false;
    public Transform middleObject;
    public Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        strecher.transform.position = spawpoint.transform.position;
    }

    void Update()
    {
        if (handle1.IsBeingHeld && handle2.IsBeingHeld)
        {
            transform.position = middleObject.position;
            transform.rotation = middleObject.rotation;
            body.linearVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            Debug.Log("zona camilla");
            IsInside = true;


            /*if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("entrar camilla");
                transform.position = spawpoint.transform.position;
            }*/

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("zona camilla");
            IsInside = false;


            /*if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("entrar camilla");
                transform.position = spawpoint.transform.position;
            }*/

        }
    }
}
