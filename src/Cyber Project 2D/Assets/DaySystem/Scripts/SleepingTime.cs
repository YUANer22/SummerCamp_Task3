using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SleepingTime : MonoBehaviour
{
    public PlayableDirector sleepingTime_In;
    public PlayableDirector sleepingTime_Out;
    static bool hasIn;
    static bool hasOut;
    public void StartSleepingTime_In()
    {
        sleepingTime_In.Play();
        DaySystemManager.Instance.PartUI("Ë¯¾õÊ±¼ä");
        hasIn = true;
        Debug.Log("7");
    }

    public void StartSleepingTime_Out()
    {
        sleepingTime_Out.Play();
        hasOut = true;
        Debug.Log("8");
    }

    public static void ResetStates()
    {
        hasIn = false;
        hasOut = false;
    }

    private void Update()
    {
        if (!hasIn && DaySystemManager.Instance.transition && !DaySystemManager.Instance.isDayStart && !DaySystemManager.Instance.isFreeTime && !DaySystemManager.Instance.isHealingTime && DaySystemManager.Instance.isSleepingTime)
        {
            if (sleepingTime_In != null)
                StartSleepingTime_In();
        }
        if (!hasOut&&!DaySystemManager.Instance.transition && !DaySystemManager.Instance.isDayStart && !DaySystemManager.Instance.isFreeTime && !DaySystemManager.Instance.isHealingTime && DaySystemManager.Instance.isSleepingTime)
        {
            if (sleepingTime_Out != null)
                StartSleepingTime_Out();
            DaySystemManager.Instance.dayCount++;
            DaySystemManager.Instance.isSleepingTime = false;
            DaySystemManager.Instance.transition = true;
        }
    }
}
