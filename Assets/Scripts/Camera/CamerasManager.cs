using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    public static CamerasManager Instance
    {
        get; private set;
    }

    [SerializeField] private Camera _ambulanceCamera;
    [SerializeField] private Camera _playersCamera;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (Instance == this)
        {
            ActivePlayersCamera();
        }
    }

    public static void ActiveAmbulanceCamera()
    {
        CamerasManager instance = Instance;

        instance._playersCamera.gameObject.SetActive(false);
        instance._ambulanceCamera.gameObject.SetActive(true);
    }

    public static void ActivePlayersCamera()
    {
        CamerasManager instance = Instance;

        instance._ambulanceCamera.gameObject.SetActive(false);
        instance._playersCamera.gameObject.SetActive(true);
    }
}
