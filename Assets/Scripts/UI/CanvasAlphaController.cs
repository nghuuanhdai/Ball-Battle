using System;
using System.Collections;
using UnityEngine;

public class CanvasAlphaController : MonoBehaviour
{
    [SerializeField] RectTransform container;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float duration = 0.5f;

    private float targetAlpha;
    private Coroutine updateCR;

    internal void ShowImmediately()
    {
        canvasGroup.interactable = true;
        container.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
    }

    internal void HideImmediately()
    {
        canvasGroup.interactable = false;
        container.gameObject.SetActive(false);
        canvasGroup.alpha = 0;
    }

    internal void Hide()
    {
        if(updateCR != null)
        {
            StopCoroutine(updateCR);
            updateCR = null;
        }
        targetAlpha = 0;
        updateCR = StartCoroutine(UpdateAlpha());
    }

    internal void Show()
    {
        if(updateCR != null)
        {
            StopCoroutine(updateCR);
            updateCR = null;
        }
        targetAlpha = 1;
        updateCR = StartCoroutine(UpdateAlpha());
    }

    private IEnumerator UpdateAlpha()
    {
        container.gameObject.SetActive(targetAlpha > 0);
        canvasGroup.interactable = false;
        while (canvasGroup.alpha != targetAlpha)
        {
            var sign = Mathf.Sign(targetAlpha - canvasGroup.alpha);
            canvasGroup.alpha += sign*1/duration;
            var newSign = Mathf.Sign(targetAlpha - canvasGroup.alpha);
            if(newSign != sign || canvasGroup.alpha == targetAlpha)
            {
                canvasGroup.alpha = targetAlpha;
                break;
            }
            yield return null;
        }

        canvasGroup.interactable = targetAlpha > 0;
        container.gameObject.SetActive(targetAlpha > 0);
    }
}