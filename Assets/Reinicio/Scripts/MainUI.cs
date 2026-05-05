using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;
    public GameObject normalCanvasPrefab;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
