using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
public class StartScreenManager : MonoBehaviour
{
    public InputField api_base;
    public InputField api_key;
    public Text errorText;
    public APISettings settings;
    public void ExitGame()
    {
        Application.Quit();
    }

    public Text show_base;
    public Text show_key;
    public Text response_time;
    private void Awake()
    {
    }
    private void Start()
    {
        api_base.text = PlayerPrefs.GetString("api_base");
        api_key.text = PlayerPrefs.GetString("api_key");
    }
    private bool SendRequest()
    {
        string url = api_base.text + "/models";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "Bearer " + api_key.text);
        request.Method = "GET";
        request.Timeout = 3000;

        try
        {
            float startTime = Time.realtimeSinceStartup;
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            float endTime = Time.realtimeSinceStartup;
            Debug.Log(((int)((endTime - startTime) * 1000)).ToString() + "ms");
            response_time.text = ((int)((endTime - startTime) * 1000)).ToString() + "ms";
            return webResponse.StatusCode == HttpStatusCode.OK;
        }
        catch
        {
            return false;
        }
    }
    public void SaveAPISettings()
    {
        if (api_key.text == "" && api_base.text == "") return;
        if (api_base.text == "") api_base.text = api_base.placeholder.GetComponent<Text>().text;
        if (SendRequest())
        {
            PlayerPrefs.SetString("api_base", api_base.text);
            PlayerPrefs.SetString("api_key", api_key.text);
        }
        else
        {
            errorText.text = "«Î«Û ß∞‹£¨«ÎºÏ≤ÈAPI…Ë÷√";
            errorText.gameObject.SetActive(true);
        }   
    }    
}
