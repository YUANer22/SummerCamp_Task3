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
        // ʹ��������ʽ����ƥ��
        bool isMatch = value == "" || Regex.IsMatch(value, urlPattern);

        // ����ƥ�������´�����ʾ
        errorText.text = "��������ȷ��api_base";
        errorText.gameObject.SetActive(!isMatch);
    }

    private void keyChanged(string value)
    {
        // ʹ��������ʽ����ƥ��
        bool isMatch = value == "" || Regex.IsMatch(value, apiKeyPattern);

        // ����ƥ�������´�����ʾ
        errorText.text = "��������ȷ��api_key";
        errorText.gameObject.SetActive(!isMatch);
    }
}