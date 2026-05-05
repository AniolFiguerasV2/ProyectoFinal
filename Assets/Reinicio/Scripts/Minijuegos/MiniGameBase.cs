using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public abstract class MiniGameBase : ScriptableObject
{
    [Header("Patient Life")]
    public float normalLife = 100f;
    public float addLife = 40f;
    public float removeLife = -40f;

    [Header("Referencias")]
    private PatientDeathTime _patientOnStretcher;


    public virtual void StartMinigame(PatientDeathTime patientDeathTime)
    {
        _patientOnStretcher = patientDeathTime;
        PatientTimeModificator();
        PrepareUIMinigame();
        DesactivateCanvas();
    }

    public void PatientTimeModificator()
    {
        float lifeTime = _patientOnStretcher.Lifetime;

        if (lifeTime > normalLife)
        {
            _patientOnStretcher.SetLifetime(normalLife);
        }
    }

    public virtual void Succes()
    {
        float addLifeTime = _patientOnStretcher.Lifetime + addLife;
        _patientOnStretcher.SetLifetime(addLifeTime);
        ActivateCanvas();
    }
    public virtual void Fail()
    {
        float removeLifeTime = Mathf.Max(0, _patientOnStretcher.Lifetime + removeLife);

        _patientOnStretcher.SetLifetime(removeLifeTime);
    }

    public void DesactivateCanvas()
    {
        MainUI.instance.normalCanvasPrefab.SetActive(false);
    }
    public void ActivateCanvas()
    {
        MainUI.instance.normalCanvasPrefab.SetActive(true);
    }
    protected abstract void PrepareUIMinigame();
}