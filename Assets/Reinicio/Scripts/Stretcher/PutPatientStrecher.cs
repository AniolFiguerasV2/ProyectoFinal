using UnityEngine;

public class PutPatientStrecher : MonoBehaviour
{
    private MoveObject currentMove;
    private MiniGamesController miniGamesController;
    [Header("Audio")]
    public AudioClip patientDeliveredClip;


    private void Start()
    {
        miniGamesController = FindAnyObjectByType<MiniGamesController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Strecher"))
        {
            MoveObject move = collision.gameObject.GetComponent<MoveObject>();
            if(move != null)
            {
                move.hasPatient = true;
                move.currentPatient = GetComponent<PatientDeathTime>();
                currentMove = move;

                if(miniGamesController != null)
                {
                    miniGamesController.StartMinigame(move.currentPatient);
                }
            }
            Transform camilla = collision.transform;
            Transform slot = camilla.Find("ZonaPaciente");
            if (slot != null)
            {
                transform.SetParent(slot, true);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.SetParent(camilla, true);
                transform.localPosition = new Vector3(0f, 0.5f, 0f);

            }
            
            //transform.localScale = Vector3.one;
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
            if (currentMove != null)
            {
                currentMove.hasPatient = false;
                currentMove.currentPatient = null;
                GameManager.Instance.PatientDelivered();
                TimerGame.instance.AddTime(60);
            }

            PatientDeathTime patient = GetComponent<PatientDeathTime>();

            if (patient != null && patient.spawner != null)
            {
                patient.spawner.NotifyNPCDeath(patient);
            }

            if (PatientSpawner.Instance != null)
            {
                PatientSpawner.Instance.ActivateNormalPatientMode();
            }

            GuidedTutorialManager tutorial = FindFirstObjectByType<GuidedTutorialManager>();

            if (tutorial != null)
            {
                tutorial.ShowGameplayUI();
                tutorial.SetTemporaryObjective(
                    "Pick up the remaining patients",
                    7f
                );
            }

            if (patientDeliveredClip != null)
            {
                AudioSource.PlayClipAtPoint(
                    patientDeliveredClip,
                    transform.position,
                    1f
                );
            }

            Destroy(gameObject);
        }
    }
}
