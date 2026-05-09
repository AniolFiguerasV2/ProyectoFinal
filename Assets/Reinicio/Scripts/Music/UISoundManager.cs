using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    public static UISoundManager Instance;

    public AudioSource audioSource;

    [Header("Menu Sounds")]
    public AudioClip selectClip;
    public AudioClip confirmClip;
    public AudioClip backClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySelect()
    {
        if (audioSource != null && selectClip != null)
            audioSource.PlayOneShot(selectClip);
    }

    public void PlayConfirm()
    {
        if (audioSource != null && confirmClip != null)
            audioSource.PlayOneShot(confirmClip);
    }

    public void PlayBack()
    {
        if (audioSource != null && backClip != null)
            audioSource.PlayOneShot(backClip);
    }
}
