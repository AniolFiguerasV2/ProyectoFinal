using UnityEngine;

public class AmbulanceDriverSlot : MonoBehaviour
{
    private bool isFull = false;
    [SerializeField] AmbulanceManager manager;

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
       {
            isFull = true;
            manager.PlayerIn(other.tag);
            Debug.Log("Player in");
       }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
