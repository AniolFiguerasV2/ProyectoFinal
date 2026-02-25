using UnityEngine;
using UnityEngine.InputSystem;

public class CamillaHandler : MonoBehaviour
{
    public MoveObject camilla;
    public Transform StretcherPivot;
    private PlayerActions actions;
    private bool StrecherInside = true;
    void Start()
    {
        actions = new PlayerActions();
        actions.Enable();
        actions.Player1.Interact.performed += ToglleStretcherState;
    }

    
    void Update()
    {
        
    }

    public bool CamillaHeld()
    { 
    
       
       return camilla.handle1.IsBeingHeld && camilla.handle2.IsBeingHeld;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.GetComponent<InteractPlayers>().SetstretcherRange(true);
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.GetComponent<InteractPlayers>().SetstretcherRange(false);
        }
            
    }

    public void ToglleStretcherState(InputAction.CallbackContext context)
    {
        if (CamillaHeld())
        {
            StrecherInside = true;
            camilla.transform.parent.gameObject.SetActive(false);
            camilla.transform.parent.SetParent(transform);
        }
        else if (StrecherInside)
        {
            StrecherInside = false;
            camilla.transform.parent.gameObject.SetActive(true);
            camilla.transform.parent.SetParent(null);
            //camilla.transform.parent.transform.SetPositionAndRotation(StretcherPivot.position, StretcherPivot.rotation);
            camilla.transform.localPosition = Vector3.zero;
        }
    }
}
