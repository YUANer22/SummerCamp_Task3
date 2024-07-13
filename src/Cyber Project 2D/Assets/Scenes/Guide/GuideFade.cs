using Cinemachine;
using DG.Tweening;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideFade : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public MMTweenType tweenType;
    public GameObject UP;
    public GameObject DOWN;

    float virtualCameraSize = 6f;
    void Start()
    {
        Invoke("StartFadeOut", 1f);
    }
    public void StartFadeOut()
    {
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        virtualCamera.m_Lens.OrthographicSize = 6f;
        Invoke("ToRegular", 8f);
        GuideManager.Instance.WakeUp();
    }

    void ToRegular()
    {
        float duration = 2f; // �仯�����ʱ�䣬����2��
        float targetValue = 7.5f; // Ŀ��ֵ
        UP.transform.DOLocalMoveY(400, duration);
        DOWN.transform.DOLocalMoveY(-400, duration);
        DOTween.To(() => virtualCameraSize, x => virtualCameraSize = x, targetValue, duration).OnUpdate(() => {
            virtualCamera.m_Lens.OrthographicSize = virtualCameraSize;
        });
        Invoke("CanMove", 2.5f);
    }

    //------------------------------------------
    public void StartFadeIn()
    {
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;
        ToCinema();
    }
    void ToCinema()
    {
        float duration = 2f; // �仯�����ʱ�䣬����2��
        float targetValue = 6f; // Ŀ��ֵ
        UP.transform.DOLocalMoveY(250, duration);
        DOWN.transform.DOLocalMoveY(-250, duration);
        DOTween.To(() => virtualCameraSize, x => virtualCameraSize = x, targetValue, duration).OnUpdate(() => {
            virtualCamera.m_Lens.OrthographicSize = virtualCameraSize;
        });
        Invoke("FadeIn", 2f);
    }
    void FadeIn()
    {
        MMFadeInEvent.Trigger(2f, tweenType, 0);
        Invoke("LoadScene", 2f);
    }
    void LoadScene()
    {
        MMSceneLoadingManager.LoadScene("InventoryTest");
    }

    void CanMove()
    {
        GameObject.FindWithTag("Player").GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;
        GuideManager.Instance.HorizontalMoveFadeIn();
    }
    #region ����ģʽ
    private static GuideFade _instance;
    public static GuideFade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Guide-Fade").GetComponent<GuideFade>();
            }
            return _instance;
        }
    }
    #endregion
}
