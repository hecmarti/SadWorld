using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroSequence : MonoBehaviour
{
    [SerializeField]
    private Transform logoPicture = default;

    [SerializeField]
    private GameObject background = default;

    [SerializeField]
    private SpriteRenderer logoLetters = default;

    [SerializeField]
    private SpriteRenderer pressAnyKey = default;

    [SerializeField]
    private CanvasGroup explanationCanvasGroup = default;

    [SerializeField]
    private TextMeshProUGUI explanationLabel = default;

    [SerializeField]
    private IntroNpcController introNpcController = default;

    [SerializeField]
    private AudioSource introMusic;

    private float logoPictureStartPositionY;

    private bool introFinished;
    private bool textExplanationStarted;

    readonly string explanationText = "In the year 2245 the Evil Coorp corporation drains the world's happiness for its own profit. The world is a gray place devoid of smiles where all people are unhappy.\n\n Until one day a little girl named Alice realizes that she is able to laugh and be happy and from that day on, everything changes.";

    private void Awake()
    {
        logoPictureStartPositionY = logoPicture.position.y;

        explanationCanvasGroup.alpha = 0;

        Color tmpLetters = logoLetters.material.color;
        tmpLetters.a = 0f;
        logoLetters.material.color = tmpLetters;

        Color tmpPressAnyKey = pressAnyKey.material.color;
        tmpPressAnyKey.a = 0f;
        pressAnyKey.material.color = tmpPressAnyKey;

        logoPicture.position = new Vector3(logoPicture.position.x, logoPicture.position.y + 10, logoPicture.position.z);
    }

    private void Start()
    {
        logoPicture.DOMoveY(logoPictureStartPositionY, 3).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            float alpha = 0;
            DOTween.To(() => alpha, x => alpha = x, 1, 2).SetDelay(0.5f)
                .OnUpdate(() =>
                {
                    Color tmpLetters = logoLetters.material.color;
                    tmpLetters.a = alpha;
                    logoLetters.material.color = tmpLetters;
                }).OnComplete(() =>
                {
                    Color tmpPressAnyKey = pressAnyKey.material.color;
                    tmpPressAnyKey.a = 1f;
                    pressAnyKey.material.color = tmpPressAnyKey;

                    introFinished = true;
                });
        });
    }

    private void Update()
    {
        if (!introFinished || textExplanationStarted)
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            textExplanationStarted = true;

            StartCoroutine(ShowTextExplanation());
        }
    }

    IEnumerator ShowTextExplanation()
    {
        introMusic.Play();
        logoPicture.gameObject.SetActive(false);
        logoLetters.gameObject.SetActive(false);
        pressAnyKey.gameObject.SetActive(false);
        background.SetActive(false);

        explanationCanvasGroup.DOFade(1, 1);

        yield return new WaitForSeconds(1);

        int currentIndex = 0;
        while (currentIndex < explanationText.Length)
        {
            currentIndex++;
            explanationLabel.text = explanationText.Substring(0, currentIndex);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(4);

        explanationCanvasGroup.DOFade(0, 1);

        yield return new WaitForSeconds(1);

        introNpcController.StartSequence();
    }
}
