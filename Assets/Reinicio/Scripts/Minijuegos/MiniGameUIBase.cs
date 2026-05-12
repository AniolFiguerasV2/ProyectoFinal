using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniGameUIBase : MonoBehaviour
{

    public ButtonsMiniGame buttonsminigame;

    [Header("Circles")]
    [SerializeField] private RectTransform player1Circle;
    [SerializeField] private RectTransform player2Circle;

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


    public void Initialize(ButtonsMiniGame game)
    {
        buttonsminigame = game;
    }

    void Start()
    {
        player1Image = player1Circle.GetComponent<Image>();
        player2Image = player2Circle.GetComponent<Image>();
        
        ResetVisuals();

        StartCoroutine(MainLoop());
    }

    void Update()
    {
        if(buttonsminigame == null) return;

        if (buttonsminigame.finished) return;

        buttonsminigame.HandleInputs();
    }

    private IEnumerator MainLoop()
    {
        while (!buttonsminigame.finished)
        {
            yield return ShrinkPhase();

            GreenPhase();

            yield return new WaitForSeconds(timeWaiting);

            buttonsminigame.EndGreenPhase();

            ResetVisuals();
        }
    }

    private IEnumerator ShrinkPhase()
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
    }

    private void GreenPhase()
    {
        player1Image.color = successColor;
        player2Image.color = successColor;

        buttonsminigame.StartGreenPhase();
    }

    private void ResetVisuals()
    {
        player1Circle.sizeDelta = maxSize;
        player2Circle.sizeDelta = maxSize;

        player1Image.color = normalColor;
        player2Image.color = normalColor;
    }
}
