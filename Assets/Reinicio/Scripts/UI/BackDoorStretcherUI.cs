using UnityEngine;
using UnityEngine.InputSystem;

public class BackDoorStretcherUI : MonoBehaviour
{
    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    [Header("UI sacar camilla")]
    public GameObject player1UI;
    public GameObject player2UI;

    public InputActionReference p1ActionSpawn;
    public InputActionReference p2ActionSpawn;

    [SerializeField] private MoveObject _moveObjectInstance;

    private void Start()
    {
        if (player1UI != null)
            player1UI.SetActive(false);

        if (player2UI != null)
            player2UI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        MoveObject moveObject = other.gameObject.GetComponentInChildren<MoveObject>();

        if (moveObject != null)
        {
            if (_moveObjectInstance.gameObject == other.gameObject)
            {
                
                //_moveObjectInstance despawn camilla
                _moveObjectInstance.DespawnStrecher();
                //_moveObjectInstance = null;
            }
        }
        else
        {
            if (_moveObjectInstance == null)
            {
                if (other.gameObject == player1 || other.transform.IsChildOf(player1.transform))
                {
                    if (player1UI != null)
                    {
                        player1UI.SetActive(true);
                        p1ActionSpawn.action.performed += SpawnCamilla;
                    }
                }

                if (other.gameObject == player2 || other.transform.IsChildOf(player2.transform))
                {
                    if (player2UI != null)
                    {
                        player2UI.SetActive(true);
                        p2ActionSpawn.action.performed += SpawnCamilla;
                    }
                }
            }
        }
    }

    private void SpawnCamilla(InputAction.CallbackContext obj)
    {
        //IF No esta spawneada, spawneo
        _moveObjectInstance = MoveObject.Instance;
        _moveObjectInstance.SpawnStrecher();
        //Cuando ya este spawneada y no pueda espawnear no mostrar la ui del botón
        player1UI.SetActive(false);
        player2UI.SetActive(false);
        //_moveObjectInstance == MoveObject.Instance
        //_moveObjectInstance.SwpawnHere

        Debug.Log("SpawnCamilla");
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1 || other.transform.IsChildOf(player1.transform))
        {
            if (player1UI != null)
            {
                player1UI.SetActive(false);
                p1ActionSpawn.action.performed -= SpawnCamilla;
            }
        }

        if (other.gameObject == player2 || other.transform.IsChildOf(player2.transform))
        {
            if (player2UI != null)
            {
                player2UI.SetActive(false);
                p2ActionSpawn.action.performed -= SpawnCamilla;
            }
        }
    }
}
