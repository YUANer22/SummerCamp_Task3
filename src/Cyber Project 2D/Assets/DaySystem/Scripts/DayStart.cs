using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DayStart : MonoBehaviour
{
    public PlayableDirector dayStart_In;
    public PlayableDirector dayStart_Out;
    static bool hasIn;
    static bool hasOut;
    public MMTweenType tweenType;
    public void StartDayStart_In()
    {
        dayStart_In.Play();
        DaySystemManager.Instance.Fade();
        DaySystemManager.Instance.DelayPartUI("一天的开始");
        hasIn = true;
    }

    public void StartDayStart_Out()
    {
        dayStart_Out.Play();
        hasOut = true;
    }

    public static void ResetStates()
    {
        hasIn = false;
        hasOut = false;
    }

    private void Update()
    {
        if(!hasIn&&DaySystemManager.Instance.transition&&DaySystemManager.Instance.isDayStart&& !DaySystemManager.Instance.isFreeTime&& !DaySystemManager.Instance.isHealingTime&& !DaySystemManager.Instance.isSleepingTime)
        {
            if(dayStart_In!=null)
                StartDayStart_In();
        }
        if (!hasOut&&!DaySystemManager.Instance.transition && DaySystemManager.Instance.isDayStart && !DaySystemManager.Instance.isFreeTime && !DaySystemManager.Instance.isHealingTime && !DaySystemManager.Instance.isSleepingTime)
        {
            if (dayStart_Out!=null)
                StartDayStart_Out();
            DaySystemManager.Instance.isDayStart = false;
            DaySystemManager.Instance.transition = true;
        }
    }

    private void Start()
    {
        DaySystemManager.Instance.transition=true;
        DaySystemManager.Instance.isDayStart=true;
    }
}
