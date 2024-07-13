using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using MoreMountains.Tools;
public class CGManager : MonoBehaviour
{
    public static CGManager Instance;
    public List<DialogConf> cgs;
    public GameObject black;
    [System.Serializable]
    public class SceneLoadedEvent : UnityEvent<int> { } // 自定义事件类型

    public SceneLoadedEvent onSceneLoaded; // 自定义事件
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        SceneManager.sceneUnloaded += SceneUnloaded;
        onSceneLoaded.AddListener(EndCG);
    }
    private void SceneUnloaded(Scene scene)
    {
        UI_Dialog.dialogEnd.RemoveAllListeners();
    }
    private void Start()
    {
        DialogueManager.Instance.StartDialog(cgs[0]);
        UI_Dialog.dialogEnd.AddListener(() => { MMSceneLoadingManager.LoadScene("GuideScene"); });
    }
    public void EndCG(int id)
    {
        DialogueManager.Instance.StartDialog(cgs[id]);
        UI_Dialog.dialogEnd.AddListener(() => { MMSceneLoadingManager.LoadScene("StartScreen"); });
    }


    public void LoadNewScene(int end)
    {
        StartCoroutine(StartScene(end));
    }

    IEnumerator StartScene(int end)
    {
        AsyncOperation aync = SceneManager.LoadSceneAsync("CGScene");
        while (aync.isDone == false)
        {
            Debug.Log(aync.progress);
            yield return null;
        }
        Debug.Log("LoadOK");
        onSceneLoaded.Invoke(end);
    }
}
