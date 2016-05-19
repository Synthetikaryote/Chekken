using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChatMenu : MonoBehaviour
{
    bool isInitialized = false;
    int chatCount;
    private Text UIText;
    private Image UITextBackground;
    private InputField UIInputField;
    
    //for resetting later?
    private List<string> textData;

    public void Intialize(Text text, Image img, InputField input)
    {
        UIText = text;
        Debug.Assert(UIText != null, "[chat menu] failed to get text object");
        UITextBackground = img;
        Debug.Assert(UIText != null, "[chat menu] failed to get Image object");
        UIInputField = input;
        Debug.Assert(UIText != null, "[chat menu] failed to get input field object");
        isInitialized = true;
        textData = new List<string>();
        chatCount = 0;
    }

    public void MenuToggle(bool active)
    {
        UIText.gameObject.SetActive(active);
        UITextBackground.gameObject.SetActive(active);
        UIInputField.gameObject.SetActive(active);
    }

    public void AddChat(string name, string chat)
    {
        string chatData = "<b>" + name + "</b>" + " : " + chat + "\n";
        textData.Add(chat);
        UpdateTextDisplay();
    }
    void UpdateTextDisplay()
    {
        while (chatCount < textData.Count)
        {
            UIText.text += textData[chatCount];
            chatCount++;
        }
    }
}
