using Cinemachine;
using DG.Tweening;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDaySystem : MonoBehaviour
{
    public int dayCount = 10;
    private static readonly int maxtimes = 5;
    private static int aitimes = maxtimes;
    public int AItimes
    {
        get { return aitimes; }
        set
        {
            aitimes = value;
            UI_AIDialog.Instance.EP.text = aitimes.ToString() + "/" + maxtimes.ToString();
        }
    }
    Text dayText;
    public MMF_Player dayUI;
    public CinemachineVirtualCamera virtualCamera;
    public MMTweenType tweenType;
    GameObject UP;
    GameObject DOWN;

    float virtualCameraSize=6f;

    GameObject CupFetch;
    GameObject SedtiveFetch;
    void Start()
    {
        dayText = GameObject.Find("DayCount").GetComponent<Text>();
        UP= GameObject.Find("UP");
        DOWN= GameObject.Find("DOWN");
        StartFadeOut();
        CupFetch = GameObject.Find("CupFetch");
        SedtiveFetch = GameObject.Find("SedtiveFetch");
    }
    private void Update()
    {
        dayText.text = "还剩" + dayCount.ToString() + "天";
        if (dayCount < 0)
        {
            dayCount = 0;
        }
        
    }
    public void StartFadeOut()
    {    
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        virtualCamera.m_Lens.OrthographicSize = 6f;
        FadeOut();
        Invoke("RespwanItems", 0.1f);
    }
  
    void FadeOut()
    {
        MMFadeOutEvent.Trigger(3f, tweenType, 0);
        Invoke("ShowDayUI", 3f);
    }
    void ShowDayUI()
    {
        RespwanItems();
        dayUI.PlayFeedbacks();
        Invoke("ToRegular", 5f);
    }
    void ToRegular()
    {
        float duration = 2f; // 变化所需的时间，例如2秒
        float targetValue = 7.5f; // 目标值
        UP.transform.DOLocalMoveY(400, duration);
        DOWN.transform.DOLocalMoveY(-400, duration);
        DOTween.To(() => virtualCameraSize, x => virtualCameraSize = x, targetValue, duration).OnUpdate(() => {
            virtualCamera.m_Lens.OrthographicSize = virtualCameraSize;
        });
        Invoke("CanMove", 2f);
    }

    //------------------------------------------
    public void StartFadeIn()
    {
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        ToCinema();
    }
    void ToCinema()
    {
        float duration = 2f; // 变化所需的时间，例如2秒
        float targetValue = 6f; // 目标值
        UP.transform.DOLocalMoveY(250, duration);
        DOWN.transform.DOLocalMoveY(-250, duration);
        DOTween.To(() => virtualCameraSize, x => virtualCameraSize = x, targetValue, duration).OnUpdate(() => {
            virtualCamera.m_Lens.OrthographicSize = virtualCameraSize;
        });
        Invoke("FadeIn", 2f);
    }
    void FadeIn()
    {
        AItimes = maxtimes;
        dayCount--;
        if(dayCount == 0)
        {
            CGManager.Instance.LoadNewScene(2);
        }
        MMFadeInEvent.Trigger(2f, tweenType, 0);
        Invoke("StartFadeOut", 4f);
    }

    void CanMove()
    {
        NarratorSystem.Instance.SendActionInfo("时间不多了，得赶紧逃离这里。");
        NarratorSystem.Instance.SendActionInfo("在医院转转看看有没有线索吧。");
        NarratorSystem.Instance.ShowInfo(2);
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;
    }
#region 单例模式
    private static NewDaySystem _instance;
    public static NewDaySystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("NewDaySystem").GetComponent<NewDaySystem>();
            }
            return _instance;
        }
    }
#endregion

    public void BackToStartScreen()
    {
        MMSceneLoadingManager.LoadScene("StartScreen");
    }

    public void RespwanItems()
    {
        ItemSystem.Instance.RespawnTargetItem("EmptyCup");
        CupFetch.SetActive(true);
        //如果天数为奇数，就刷新一次
        if (dayCount % 2 == 1)
        {
            ItemSystem.Instance.RespawnTargetItem("Sedtive");
            SedtiveFetch.SetActive(true);
        }
    }
}
