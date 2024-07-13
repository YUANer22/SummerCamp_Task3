using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Validator : MonoBehaviour
{
    public Text errorText;

    private string urlPattern = @"^(https?://)?([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$";
    private string apiKeyPattern = @"^sk-[a-zA-Z0-9]+$";

    private void Start()
    {
        transform.Find("API_base/InputField").GetComponent<InputField>().onValueChanged.AddListener(urlChanged);
        transform.Find("API_key/InputField").GetComponent<InputField>().onValueChanged.AddListener(keyChanged);
    }

    private void urlChanged(string value)
    {
        // 使用正则表达式进行匹配
        bool isMatch = value == "" || Regex.IsMatch(value, urlPattern);

        // 根据匹配结果更新错误提示
        errorText.text = "请输入正确的api_base";
        errorText.gameObject.SetActive(!isMatch);
    }

    private void keyChanged(string value)
    {
        // 使用正则表达式进行匹配
        bool isMatch = value == "" || Regex.IsMatch(value, apiKeyPattern);

        // 根据匹配结果更新错误提示
        errorText.text = "请输入正确的api_key";
        errorText.gameObject.SetActive(!isMatch);
    }
}