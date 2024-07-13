using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaySystemManager : MonoBehaviour
{
    public MMF_Player dayStart;
    public MMF_Player freeTime;
    public MMF_Player healingTime;
    public MMF_Player sleepingTime;
    public MMF_Player dayUI;
    public MMF_Player delayPartUI;
    public MMF_Player partUI;

    Text dayText;
    Text partText;

    public bool isDayStart = false;
    public bool isFreeTime = false;
    public bool isHealingTime = false;
    public bool isSleepingTime = false;
    public bool transition = false;
    public int dayCount = 1;
    public MMTweenType tweenType;
    int count;

    private void Awake()
    {
        count = dayCount;
        dayText= GameObject.Find("DayCount").GetComponent<Text>();
        partText= GameObject.Find("PartUI").GetComponent<Text>();
    }
    public void ResetStates()
    {
        count = dayCount;
        DayStart.ResetStates();
        FreeTime.ResetStates();
        HealingTime.ResetStates();
        SleepingTime.ResetStates();
    }

    public void Fade()
    {
        MMFadeInEvent.Trigger(1f, tweenType, 0);
        dayUI.PlayFeedbacks();
        Invoke("Delay", 5f);
    }

    void Delay()
    {
        MMFadeOutEvent.Trigger(1f, tweenType, 0);
    }

    public void DelayPartUI(string text)
    {
        partText.text = "现在是" + text;
        delayPartUI.PlayFeedbacks();
    }
    public void PartUI(string text)
    {
        partText.text = "现在是" + text;
        partUI.PlayFeedbacks();
    }

    private void Update()
    {
        if (dayCount - count == 1)
        {
            ResetStates();
        }
        dayText.text = "第" + dayCount.ToString() + "天";
    }
    //完成一个单例模式
    private static DaySystemManager _instance;
    public static DaySystemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("DaySystem").GetComponent<DaySystemManager>();
            }
            return _instance;
        }
    }

    
}
