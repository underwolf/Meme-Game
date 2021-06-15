using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    [Header("UI")]
    public Text m_TextUI;

    [Header("Setup")]
    public string m_Filename = "credits";
    [Range(10.0f, 50.0f)]
    public float m_ScrollSpeed = 20.0f;

    private void Start()
    {
        var credit = LoadCredit();
        var builder = new System.Text.StringBuilder();
        foreach (var team in credit.teams)
        {
            builder.Append(WriteLine(team.title, credit.titleSize));
            foreach (var name in team.names)
            {
                builder.Append(WriteLine(name, credit.nameSize));
            }

            builder.Append(BreakLine(credit.titleSize));
        }

        UpdateUI(builder.ToString());    
    }

    private void Update()
    {
        m_TextUI.transform.Translate(Vector3.up * m_ScrollSpeed * Time.deltaTime);
    }

    private void UpdateUI(string text)
    {
        m_TextUI.text = text;
        Canvas.ForceUpdateCanvases();
    }

    private Credit LoadCredit()
    {
        var json = Resources.Load<TextAsset>(m_Filename);
        return JsonUtility.FromJson<Credit>(json.text);
    }

    private string BreakLine(int size)
    {
        return $"<size={size}>\n</size>";
    }

    private string WriteLine(string text, int size, bool bold = false)
    {
        string line = "";
        if (bold) line += "<b>";
        line += $"<size={size}>{text}</size>";
        if (bold) line += "</b>";
        line += BreakLine(size);
        return line;
    }
}

[System.Serializable]
public class Credit 
{
    public int titleSize;
    public int nameSize;
    public Team[] teams;
}

[System.Serializable]
public class Team 
{
    public string title;
    public string[] names;
}