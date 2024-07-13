using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.CorgiEngine;
using Unity.VisualScripting;

public class NarratorSystem : MonoBehaviour
{
    List<String> items = new List<String>();
    List<String> actions = new List<String>();
    List<String> dialogue = new List<String>();
    List<String> interactions = new List<String>();
    int currentItemNum = 0;
    int currentActionNum = 0;
    int currentDialogueNum = 0;
    int currentInteractionNum = 0;
    Text NarratorText;
    TypewriterEffect typewriterEffect;
    CanvasGroup NarratorUI;
    float timer = 0;
    public float displayTime = 5f;//旁白显示时间
    private void Start()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);
        NarratorText = GameObject.Find("NarratorText").GetComponent<Text>();
        typewriterEffect = GameObject.Find("NarratorText").GetComponent<TypewriterEffect>();
        NarratorUI = GameObject.Find("NarratorUI").GetComponent<CanvasGroup>();
        currentItemNum = 0;
        currentActionNum = 0;
        currentDialogueNum = 0;
        currentInteractionNum = 0;
    }
    private void Update()
    {
       
            if (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
            }
            else
            {
                NarratorUI.DOFade(0, 0.2f);
            }
           
    }

    #region 单例模式
    private static NarratorSystem instance;
    public static NarratorSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NarratorSystem>();
            }
            return instance;
        }
    }
    #endregion
    #region
    public void SendItemInfo(String itemName)//捡起物品、使用物品时调用
    {
        items.Add(itemName);
    }

    public void SendActionInfo(String actionName)//进出房间、与物体交互时调用
    {
        actions.Add(actionName);
    }

    public void SendDialogueInfo(String dialogueName)//对话后调用
    {
        dialogue.Add(dialogueName);
    }

    public void SendInteractionInfo(String interactionInfo)//与UI交互时调用
    {
        interactions.Add(interactionInfo);
    }
    #endregion
    public void ShowInfo(String info)//无关ID，直接显示info
    {
        StopAllCoroutines();
        NarratorUI.DOFade(1, 0.2f);
        NarratorText.text = info;
        typewriterEffect.ReStartEffect();
        timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 4;//显示时间
    }

    public void ShowInfo(int ID)//根据ID显示info
    {
        if (ID == 1)
        {
            if (items != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowItemsList());
            }
        }
        else if (ID == 2)
        {
            if (actions != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowActionsList());
            }
        }
        else if (ID == 3)
        {
            if (dialogue != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowDialogueList());
            }

        }
        else if (ID == 4)
        {
            if (interactions != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowInteractionsList());
            }
        }
    }
    IEnumerator ShowItemsList()
    {
        for(int i = currentItemNum; i < items.Count; i++)
        {
            NarratorUI.DOFade(1, 0.2f);
            NarratorText.text = items[i];
            typewriterEffect.ReStartEffect();       
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            currentItemNum = items.Count;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }

    }
    IEnumerator ShowActionsList()
    {
        for(int i = currentActionNum; i < actions.Count; i++)
        {
            NarratorUI.DOFade(1, 0.2f);
            NarratorText.text = actions[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            currentActionNum = actions.Count;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
       
    }
    IEnumerator ShowDialogueList()
    {
        for(int i = currentDialogueNum; i < dialogue.Count; i++)
        {
            NarratorUI.DOFade(1,0.2f);
            NarratorText.text = dialogue[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            currentDialogueNum = dialogue.Count;//这样写的好处是，只要触发一次旁白，就会无视之前的旁白，直接显示最新的旁白
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
        
    }
    IEnumerator ShowInteractionsList()
    {
        for(int i = currentInteractionNum; i < interactions.Count; i++)
        {
            NarratorUI.DOFade(1, 0.2f);
            NarratorText.text = interactions[i];
            typewriterEffect.ReStartEffect();
            timer = typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 5;
            currentInteractionNum = interactions.Count;
            yield return new WaitForSeconds(typewriterEffect.words.Length * typewriterEffect.charsPerSecond + 1);
        }
        
    }
}
