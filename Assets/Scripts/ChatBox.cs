using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChatBox : MonoBehaviour
{
    [SerializeField] ContentSizeFitter contentSizeFitter;
    [SerializeField] Text showhideButtonText;
    [SerializeField] Transform messageParentPanel;
    [SerializeField] GameObject newMessagePrefab;
    bool showChat = false;
    string message = "";
    void Start()
    {
        ToggleChat();
    }
    public void ToggleChat()
    {
        showChat = !showChat;
        if (showChat)
        {
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            showhideButtonText.text = "hide chat";
        }
        else
        {
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
            showhideButtonText.text = "show chat";
        }
    }

    public void SetMessage(string message)
    {
        this.message = message;
    }

    public void ShowMessage()
    {
        if (message != "")
        {
            GameObject clone = Instantiate<GameObject>(newMessagePrefab);
            clone.transform.SetParent(messageParentPanel);
            clone.transform.SetSiblingIndex(messageParentPanel.childCount - 2);
            clone.GetComponent<Message>().ShowMessage(message);
        }
    }

}
