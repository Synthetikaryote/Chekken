using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChatMenu : MonoBehaviour
{
    bool isInitialized = false;
    int chatCount;

    //horizontal limit 24
    //vertical limit ? 18 + 7 = 25;
    /*
     Add more row if the word count more than 24
     */
    int limit = 21;
    private Text uIText;
    private Image uITextBackground;
    private InputField uIInputField;
    float windowHeight;
    //for resetting later?
    private List<string> textData;

    public void Intialize(Text text, Image img, InputField input)
    {
        uIText = text;
        Debug.Assert(uIText != null, "[chat menu] failed to get text object");
        windowHeight = text.rectTransform.rect.height;

        uITextBackground = img;
        Debug.Assert(uITextBackground != null, "[chat menu] failed to get Image object");
        uIInputField = input;
        Debug.Assert(uIInputField != null, "[chat menu] failed to get input field object");
        isInitialized = true;
        textData = new List<string>();
        chatCount = 0;
        uIText.text = "";
    }

    public void MenuToggle(bool active)
    {
        uIText.gameObject.SetActive(active);
        uITextBackground.gameObject.SetActive(active);
        uIInputField.gameObject.SetActive(active);
    }

    public void AddChat(string name, string chat)
    {
        string chatData = "<b>" + name + "</b>" + " : " + chat + "\n";
        textData.Add(chat);
        UpdateTextDisplay();
    }

    public void UpdateField(string name)
    {
        if (HasText())
        {
            string chatData = "<b>" + name + "</b>" + " : " + uIInputField.text + "\n";
            textData.Add(chatData);
            UpdateTextDisplay();
            uIInputField.text = "";
        }
    }
    public bool HasText()
    {
        return uIInputField.text.Length > 0;
    }

    void UpdateTextDisplay()
    {
        while (chatCount < textData.Count)
        {
            int resizedWindowMultiplier = 1;
            int textLength = textData[chatCount].Length;
            if (textLength > 24)
            {
                resizedWindowMultiplier = Mathf.CeilToInt(textLength / 24);
                Vector2 sizeDelta = uIText.rectTransform.sizeDelta;
                Debug.Log("<color=yellow>" + "size delta : " + sizeDelta + "</color>");
                float height = uIText.rectTransform.rect.height;
                height += (windowHeight * resizedWindowMultiplier);
                Debug.Log("<color=yellow>" + "new height : " + height + "</color>");
                Debug.Break();
                uIText.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, height);
                //uIText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                //uIText.rectTransform.rect.Set(pos.x, pos.y, width, height);
            }
            Debug.Log("resize by : " + resizedWindowMultiplier);
            uIText.text += textData[chatCount];
            chatCount++;
        }
    }
}
