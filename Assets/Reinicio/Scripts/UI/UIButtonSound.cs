using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        if (UISoundManager.Instance != null)
            UISoundManager.Instance.PlaySelect();
    }

    public void PlayConfirm()
    {
        if (UISoundManager.Instance != null)
            UISoundManager.Instance.PlayConfirm();
    }

    public void PlayBack()
    {
        if (UISoundManager.Instance != null)
            UISoundManager.Instance.PlayBack();
    }
}
