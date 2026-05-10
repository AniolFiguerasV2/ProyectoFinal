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
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySelect()
    {
        audioSource.PlayOneShot(selectClip);
    }

    public void PlayConfirm()
    {
        audioSource.PlayOneShot(confirmClip);
    }

    public void PlayBack()
    {
        audioSource.PlayOneShot(backClip);
    }
}