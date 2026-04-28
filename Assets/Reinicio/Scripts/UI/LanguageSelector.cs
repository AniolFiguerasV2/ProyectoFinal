using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(ChangeLanguage);    
    }

    public void ChangeLanguage(int index)
    {
        StartCoroutine(SetLocale(index));
    }

    IEnumerator SetLocale(int index)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
