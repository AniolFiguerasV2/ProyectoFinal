using UnityEditor;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool hasPatient = false;
    public Handles handle1;
    public Handles handle2;

    public GameObject strecher;
    public GameObject spawpoint;

    public bool ZonaCarga = false;

    public bool IsInside = true;
    public Transform middleObject;
    public Transform backDoor;
    public Rigidbody body;
    public Animator animator;
    public Animator animator1;
    public bool interactPlayed = false;

    public bool alreadyScored = false;

    [Header("UI de interacción")]
    public GameObject player1UI;
    public GameObject player2UI;

    private bool wasHolding = false;

    public static MoveObject Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        strecher.transform.position = spawpoint.transform.position;
        IsInside = true;
    }

    void Update()
    {
        bool isHolding = handle1.IsBeingHeld || handle2.IsBeingHeld;
        bool bothHolding = handle1.IsBeingHeld && handle2.IsBeingHeld;

        if (isHolding)
        {
            if (player1UI != null)
                player1UI.SetActive(false);

            if (player2UI != null)
                player2UI.SetActive(false);
        }

        if (bothHolding)
        {
            transform.position = middleObject.position;
            transform.rotation = middleObject.rotation;
            body.linearVelocity = Vector3.zero;

            if (!interactPlayed)
            {
                animator.SetTrigger("Intercat");
                animator1.SetTrigger("Intercat");

                interactPlayed = true;
            }

            ControlHintsManager.Instance.ShowStretcherCarryHints();
        }
        else
        {
            interactPlayed = false;
        }

        if (wasHolding && !isHolding)
        {
            if (ControlHintsManager.Instance != null)
                ControlHintsManager.Instance.ShowOnFootHints();
        }

        wasHolding = isHolding;
    }

    public void SpawnStrecher()
    {
        if (IsInside)
        {
            strecher.transform.SetPositionAndRotation(
                backDoor.position,
                backDoor.rotation
            );
            IsInside = false;
        }

        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    public void DespawnStrecher()
    {
        if (!IsInside)
        {
            strecher.transform.SetPositionAndRotation(
                spawpoint.transform.position,
                spawpoint.transform.rotation
            );

            IsInside = true;

            if (IsInside && hasPatient && !alreadyScored)
            {
                ScoreManager.Instance.AddPoints(100);
                alreadyScored = true;
            }
        }
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }
}