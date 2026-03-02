using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public List<PutPacientStrecher> listaObjetivos = new List<PutPacientStrecher>();

    private Transform objetivoActual;

    void Start()
    {
        ActualizarLista();
        SeleccionarPrimerObjetivo();
    }

    void Update()
    {
        if (objetivoActual != null)
        {
            ApuntarAObjetivo();
        }
    }

    void ActualizarLista()
    {
        listaObjetivos.Clear();

        PutPacientStrecher[] encontrados = FindObjectsOfType<PutPacientStrecher>();

        foreach (PutPacientStrecher t in encontrados)
        {
            listaObjetivos.Add(t);
        }
    }

    void SeleccionarPrimerObjetivo()
    {
        if (listaObjetivos.Count > 0)
        {
            objetivoActual = listaObjetivos[0].transform;
        }
    }

    void ApuntarAObjetivo()
    {
        Vector3 direccion = objetivoActual.position - transform.position;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = rotacion;
    }
}
