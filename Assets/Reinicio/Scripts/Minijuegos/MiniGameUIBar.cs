using UnityEngine;
using System.Collections;

public class MiniGameUIBar : MonoBehaviour
{

    public BarMinigame barminigame;

    [Header("UI")]
    public RectTransform syringe;
    public RectTransform syringe2;

    [Header("Movement")]
    [SerializeField] private float speed = 400f;
    [SerializeField] private float leftX = -600f;
    [SerializeField] private float rightX = 600f;
    [SerializeField] private float centerMargin = 80f;

    private bool movingRight = true;
    private Coroutine loop;

    public void Initialize(BarMinigame game)
    {
        barminigame = game;

        if (loop != null) 
        { 
            StopCoroutine(loop);
            loop = null;
        }


        ResetPositions();
    }
    public void StartLoop()
    {
        if(loop != null)
        {
            StopCoroutine(loop);
        }

        loop = StartCoroutine(MoveLoop());
    }

    public void StopLoop()
    {
        if(loop != null)
        {
            StopCoroutine(loop);
            loop = null;
        }
    }

    private IEnumerator MoveLoop()
    {
        yield return null;
        
        while (barminigame != null && !barminigame.finished)
        {
            if (!barminigame.isActive) 
            {
                yield return null;
                continue;
            }

            TickUI();
            barminigame.Tick();

            yield return null;
        }
    }

    public void TickUI()
    {
        MoveSyringes();
    }

    private void MoveSyringes()
    {
        float direction = movingRight ? 1 : -1;

        syringe.anchoredPosition += Vector2.right * direction * speed * Time.deltaTime;
        syringe2.anchoredPosition += Vector2.right * -direction * speed * Time.deltaTime;

        if (syringe.anchoredPosition.x >= rightX)
            movingRight = false;

        if (syringe.anchoredPosition.x <= leftX)
            movingRight = true;
    }

    public bool IsInCenter()
    {
        float centerX = (leftX +  rightX) * 0.5f;
        return Mathf.Abs(syringe.anchoredPosition.x - centerX) <= centerMargin;
    }

    public void ResetPositions()
    {
        syringe.anchoredPosition = new Vector2(leftX, syringe.anchoredPosition.y);
        syringe2.anchoredPosition = new Vector2(rightX, syringe2.anchoredPosition.y);
        movingRight = true;
    }
}
