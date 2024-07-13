using DG.Tweening;
using MoreMountains.CorgiEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    public float fadeTime = 0.5f;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    private void Start()
    {
        canvasGroup= GameObject.Find("BedCanvas").GetComponent<CanvasGroup>();
        rectTransform = GameObject.Find("BedPanel").GetComponent<RectTransform>();
    }
    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.OutExpo);
        canvasGroup.DOFade(1, fadeTime);
    }
    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1;
        rectTransform.DOAnchorPos(new Vector2(0, -1000f), fadeTime, false).SetEase(Ease.InExpo);
        canvasGroup.DOFade(0, fadeTime);
    }
}
