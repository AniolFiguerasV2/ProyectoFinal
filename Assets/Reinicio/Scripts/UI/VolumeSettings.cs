using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer MasterMix;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        MasterMix.SetFloat("MusicParameter", Mathf.Log10(volume)*20);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        MasterMix.SetFloat("SFXParameter", Mathf.Log10(volume)*20);
    }
}
