using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{
    MMF_Player feedback;
    bool hasUsed = false;
    public static bool use = false;
    public TestItem testItem;
    private void Start()
    {
        feedback = GetComponent<MMF_Player>();       
    }

    private void Update()
    {
        ShowUI();
    }
    void ShowUI()
    {
        if (use&&!hasUsed)
        {
            feedback.PlayFeedbacks();
            hasUsed = true;
        }
    }
}
