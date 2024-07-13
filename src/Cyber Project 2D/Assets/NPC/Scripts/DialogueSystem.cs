using DG.Tweening;
using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public float fadeTime = 0.5f;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    TypewriterEffect typewriterEffect;
    GameObject player;
    private void Start()
    {
        canvasGroup=GameObject.Find("DialogueUI").GetComponent<CanvasGroup>();
        rectTransform = GameObject.Find("DialoguePanel").GetComponent<RectTransform>();
        typewriterEffect = GameObject.Find("DialogueText").GetComponent<TypewriterEffect>();
    }
    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        typewriterEffect.ReStartEffect();
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.OutExpo);
        canvasGroup.DOFade(1, fadeTime);
    }
    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1;
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;
        rectTransform.DOAnchorPos(new Vector2(0, -1000f), fadeTime, false).SetEase(Ease.InExpo);
        canvasGroup.DOFade(0, fadeTime);
    }
}
