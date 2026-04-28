using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MiniGame1 : MonoBehaviour
{
    [Header("InputManager")]
    [NonSerialized] private int player1Id = 1;
    [NonSerialized] private int player2Id = 2;

    [Header("Circles References")]
    [SerializeField] private RectTransform player1Circle;
    [SerializeField] private RectTransform player2Circle;

    [Header("Canvas a desactivar")]
    [SerializeField] private GameObject canvasToDisable;

    [Header("Size Settings")]
    [SerializeField] private Vector2 maxSize = new Vector2(250f, 250f);
    [SerializeField] private Vector2 minSize = new Vector2(120f, 120f);
    [SerializeField] private float shrinkSpeed = 200f;
    [SerializeField] private float timeWaiting = 0.2f;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.red;
    [SerializeField] private Color successColor = Color.green;

    private Image player1Image;
    private Image player2Image;

    private bool inGreen = false;
    private bool roundResolved = false;

    private int successCounter = 0;
    private int targetSuccess = 5;

    private int failCounter = 0;
    private int maxFails = 10;

    private bool p1PressedInGreen = false;
    private bool p2PressedInGreen = false;

    private bool finished = false;

    public event Action<bool> OnMiniGameFinished;

    void Start()
    {
        player1Image = player1Circle.GetComponent<Image>();
        player2Image = player2Circle.GetComponent<Image>();

        player1Circle.sizeDelta = maxSize;
        player2Circle.sizeDelta = maxSize;

        StartCoroutine(MainLoop());
    }

    void Update()
    {
        if (roundResolved || finished) return;

        bool p1Pressed = InputManager.Instance.GetGrabHold(player1Id);
        bool p2Pressed = InputManager.Instance.GetGrabHold(player2Id);

        if (inGreen)
        {
            if (p1Pressed) p1PressedInGreen = true;
            if (p2Pressed) p2PressedInGreen = true;

            if (p1PressedInGreen && p2PressedInGreen)
            {
                successCounter++;
                roundResolved = true;

                if (successCounter >= targetSuccess)
                {
                    FinishGame(true);
                }
            }
        }
        else if (p1Pressed || p2Pressed)
        {
            successCounter = 0;
            failCounter++;

            if (failCounter >= maxFails)
            {
                FinishGame(false);
            }

            roundResolved = true;
        }
    }

    private IEnumerator MainLoop()
    {
        while (!finished)
        {
            while (player1Circle.sizeDelta.x > minSize.x)
            {
                Vector2 shrink = Vector2.one * shrinkSpeed * Time.deltaTime;

                player1Circle.sizeDelta -= shrink;
                player2Circle.sizeDelta -= shrink;

                yield return null;
            }

            player1Circle.sizeDelta = minSize;
            player2Circle.sizeDelta = minSize;

            player1Image.color = successColor;
            player2Image.color = successColor;

            inGreen = true;
            roundResolved = false;

            p1PressedInGreen = false;
            p2PressedInGreen = false;

            yield return new WaitForSeconds(timeWaiting);

            if (!roundResolved)
            {
                successCounter = 0;
                failCounter++;

                if (failCounter >= maxFails)
                {
                    FinishGame(false);
                }
            }

            player1Circle.sizeDelta = maxSize;
            player2Circle.sizeDelta = maxSize;

            player1Image.color = normalColor;
            player2Image.color = normalColor;

            inGreen = false;
        }
    }

    private void FinishGame(bool success)
    {
        if (finished) return;

        finished = true;

        StopAllCoroutines();

        OnMiniGameFinished?.Invoke(success);

        canvasToDisable.SetActive(false);
    }
}