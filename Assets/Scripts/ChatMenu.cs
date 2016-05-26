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
    private ServerCommunication serverCom;
    int limit = 25;
    private Text uIText;
    private Image uITextBackground;
    private InputField uIInputField;
    private GameObject uIMask;
    float windowHeight;
    //for resetting later?
    private List<string> textData;

    public void Intialize(Text text, Image img, InputField input, GameObject mask)
    {
        serverCom = gameObject.GetComponent<ServerCommunication>();
        uIText = text;
        Debug.Assert(uIText != null, "[chat menu] failed to get text object");
        windowHeight = text.rectTransform.rect.height;
        uIMask = mask;
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
        uIMask.SetActive(active);
    }

    public void UpdateField(string name)
    {
        if (HasText())
        {
            string chatData = "<b>" + name + "</b>" + " : " + uIInputField.text + "\n";
            textData.Add(chatData);

            serverCom.SendChat(chatData);
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
            Vector3 posPrev = uIText.transform.position;
            if (textLength > limit)
            {
                resizedWindowMultiplier = Mathf.CeilToInt(textLength / limit);
                Vector2 sizeDelta = uIText.rectTransform.sizeDelta;
                Debug.Log("<color=yellow>" + "size delta : " + sizeDelta + "</color>");
                float height = uIText.rectTransform.rect.height;
                height += (windowHeight * resizedWindowMultiplier);
                Debug.Log("<color=yellow>" + "new height : " + height + "</color>"); 
                uIText.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, height);
                uIText.transform.position = posPrev;
            }
            Debug.Log("resize by : " + resizedWindowMultiplier);
            uIText.text += textData[chatCount];
            chatCount++;
        }
    }
}
