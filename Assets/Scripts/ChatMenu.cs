using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChatMenu
{
    int chatCount;
    bool updateChat;
    //horizontal limit 24
    //vertical limit ? 18 + 7 = 25;
    /*
     Add more row if the word count more than 24
     */
    private ServerCommunication serverCom;
    int limit = 25;
    private Text uIText;
    private GameObject uIParent;
    private InputField uIInputField;
    float windowHeight;
    private Vector2 scrollPos;
    private Vector2 uiSize;
    //for resetting later?
    private List<string> textData;

    public void Intialize(Text text, InputField input, GameObject parent, ServerCommunication sC)
    {
        updateChat = false;
        scrollPos = new Vector2();
        uiSize = parent.GetComponent<RectTransform>().sizeDelta;
        scrollPos = parent.transform.position;
        serverCom = sC;
        serverCom.onPlayerMessage += GetMessageFromServer;
        uIText = text;
        Debug.Assert(uIText != null, "[chat menu] failed to get text object");
        windowHeight = text.rectTransform.rect.height;
        uIParent = parent;
        Debug.Assert(uIParent != null, "[chat menu] failed to get parent");
        uIInputField = input;
        Debug.Assert(uIInputField != null, "[chat menu] failed to get input field object");
        textData = new List<string>();
        chatCount = 0;
        uIText.text = "";
    }

    public void MenuToggle(bool active)
    {
        uIParent.SetActive(active);
    }

    public void UpdateField(string name, string serverMsg = "", bool server = false)
    {
        if (server == true)
        {
            textData.Add(serverMsg);
            //updateChat = true;
            UpdateTextDisplay();
        }
        else if (HasText())
        {
            string chatData = "<b>" + name + "</b>" + " : " + uIInputField.text + "\n";
            textData.Add(chatData);
            serverCom.SendChat(chatData);
            //updateChat = true;
            UpdateTextDisplay();
            uIInputField.text = "";
        }
    }
    public bool HasText()
    {
        return uIInputField.text.Length > 0;
    }
    //void OnGUI()
    //{
    //    if (updateChat)
    //    {
    //        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(uiSize.x), GUILayout.Height(uiSize.y));
    //        while (chatCount < textData.Count)
    //        {
    //            uIText.text += textData[chatCount];
    //            chatCount++;
    //        }
    //        GUILayout.EndScrollView();
    //        updateChat = false;
    //    }
    //}
    void UpdateTextDisplay()
    {
        //scrollPos = GUILayout.BeginScrollView(scrollPos);
        while (chatCount < textData.Count)
        {
            int resizedWindowMultiplier = 1;
            int textLength = textData[chatCount].Length;
            Vector3 posPrev = uIText.transform.position;
            /*
             pseudo code:
             get width and length of each line?
             */
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
        //GUILayout.EndScrollView();
    }
    private void ResolveChatSize()
    {
        int wordCount = 0;
        foreach (string msg in textData)
        {
            wordCount += msg.Length;
        }
    }
    void GetMessageFromServer(ServerCommunication.Player play, string msg)
    {
        UpdateField(play.name, msg, true);
    }
}
