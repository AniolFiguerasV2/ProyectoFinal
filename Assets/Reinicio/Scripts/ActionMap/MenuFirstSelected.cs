using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFirstSelected : MonoBehaviour
{
    public GameObject firstSelectedButton;

    private void OnEnable()
    {
        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }
}
