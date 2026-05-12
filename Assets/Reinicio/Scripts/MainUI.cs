using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    public GameObject normalCanvasPrefab;
    public Canvas baseCanvas;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
