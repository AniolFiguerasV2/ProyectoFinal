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

    public bool IsInside = false;
    public Transform middleObject;
    public Rigidbody body;
    public Animator animator;
    public Animator animator1;
    public bool interactPlayed = false;

    [Header("Tutorial camilla")]
    public StartTutorialManager tutorialManager;
    private bool stretcherTutorialShown = false;
    public bool alreadyScored = false;

    [Header("UI de interacción")]
    public GameObject player1UI;
    public GameObject player2UI;

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

            if (player1UI != null)
                player1UI.SetActive(false);

            if (player2UI != null)
                player2UI.SetActive(false);

            if (!interactPlayed)
            {
                animator.SetTrigger("Intercat");
                animator1.SetTrigger("Intercat");
                interactPlayed = true;
            }

            if (!stretcherTutorialShown && tutorialManager != null)
            {
                tutorialManager.ShowStretcherTutorial();
                stretcherTutorialShown = true;
            }
        }
        else
        {
            interactPlayed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            IsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            IsInside = false;
        }
    }

    void TryScore()
    {
        if (IsInside && hasPatient && !alreadyScored)
        {
            alreadyScored = true;

            ScoreManager.Instance.AddPoints(100);
        }
    }
}