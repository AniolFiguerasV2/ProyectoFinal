using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Unity.VisualScripting;


public abstract class MiniGameBase : ScriptableObject
{
    [Header("Patient Life")]
    public float normalLife = 100f;
    public float addLife = 40f;
    public float removeLife = -40f;

    [Header("Referencias")]
    private PatientDeathTime _patientOnStretcher;

    [NonSerialized] public bool finished;

    public Action<MiniGameBase> OnMinigameFinished;


    public virtual void StartMinigame(PatientDeathTime patientDeathTime)
    {
        _patientOnStretcher = patientDeathTime;
        _patientOnStretcher.PauserTimer(true);
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
        _patientOnStretcher.PauserTimer(false);
        ActivateCanvas();
    }
    public virtual void Fail()
    {
        float newLife = _patientOnStretcher.Lifetime + removeLife;
        newLife = Mathf.Max(10f, newLife);
        _patientOnStretcher.PauserTimer(false);
        _patientOnStretcher.SetLifetime(newLife);
        ActivateCanvas();
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