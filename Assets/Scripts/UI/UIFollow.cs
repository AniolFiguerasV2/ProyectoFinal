using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public Transform target; // el jugador
    public Vector3 offset;   // para subir la UI (por ejemplo encima de la cabeza)

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + offset);

        // Si está detrás de la cámara, ocultar
        if (screenPos.z < 0)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        transform.position = screenPos;
    }
}
