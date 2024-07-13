using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FreeTime : MonoBehaviour
{
    public PlayableDirector freeTime_In;
    public PlayableDirector freeTime_Out;
    static bool hasIn;
    static bool hasOut;
    public void StartFreeTime_In()
    {
        freeTime_In.Play();
        DaySystemManager.Instance.PartUI("自由时间");
        hasIn = true;
        Debug.Log("3");
    }

    public void StartFreeTime_Out()
    {
        freeTime_Out.Play();
        hasOut = true;
        Debug.Log("4");
    }

    public static void ResetStates()
    {
        hasIn = false;
        hasOut = false;
    }

    private void Update()
    {
        if (!hasIn&&DaySystemManager.Instance.transition && !DaySystemManager.Instance.isDayStart && DaySystemManager.Instance.isFreeTime && !DaySystemManager.Instance.isHealingTime && !DaySystemManager.Instance.isSleepingTime)
        {
            if (freeTime_In != null)
                StartFreeTime_In();
        }
        if (!hasOut&&!DaySystemManager.Instance.transition && !DaySystemManager.Instance.isDayStart && DaySystemManager.Instance.isFreeTime && !DaySystemManager.Instance.isHealingTime && !DaySystemManager.Instance.isSleepingTime)
        {
            if (freeTime_Out != null)
                StartFreeTime_Out();
            DaySystemManager.Instance.isFreeTime = false;
            DaySystemManager.Instance.transition = true;
        }
    }
}
