using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; protected set; }

    public GameObject chickenSelectMenu;
    public InputField inputMenu;
    public string playerName;
    int myChickenID;
    int mySkillID;
    public bool chatMenuOn { private set; get; }

    ChatMenu chatMenu;
    public Text UIText;
    public Image UITextBackground;
    public InputField UIInputField;
    void Awake()
    {
        Instance = this;
        chatMenu = new ChatMenu();
        if (chickenSelectMenu == null)
        {
            Debug.Log("[Menu Manager] no UI_CharacteMenu");
            Debug.Break();
        }
    }
    void Start()
    {
        ResetVariable();
        chatMenuOn = false;
        chatMenu.Intialize(UIText, UITextBackground, UIInputField);
        chatMenu.MenuToggle(chatMenuOn);
    }
    void ResetVariable()
    {
        myChickenID = -1;
        mySkillID = -1;
        playerName = "";
        inputMenu.text = "";
        
    }
    public void StartGame()
    {
        bool failed = false;
        //check for error
        if (myChickenID == -1)
        {
            Debug.LogError("[Menu Manager]No Chicken selected");
            failed = true;
        }
        if (mySkillID == -1)
        {
            Debug.LogError("[Menu Manager]No skill selected");
            failed = true;
        }
        if (playerName == "")
        {
            Debug.LogError("[Menu Manager]Name field is empty");
            failed = true;
        }
        if (!failed)
        {

            //check if chicken and id exist
            var chicken = ChickenSpawnerManager.Instance.SpawnChicken(myChickenID, mySkillID, playerName);
            if (chicken != null)
            {
                //call name setter here
                Debug.Log("[Menu Manager] name is : " + playerName);
                chickenSelectMenu.SetActive(false);
                GetComponent<MultiplayerManager>().GameStart(chicken, playerName);
                ResetVariable();
            }


        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)
            && chickenSelectMenu.activeSelf == false)
        {
            chickenSelectMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            chatMenuOn = !chatMenuOn;
            chatMenu.MenuToggle(chatMenuOn);
        }
    }
    //menu setter
    public void SetName(string name)
    {
        playerName = name;
    }
    public void SetChickenID(int chickID)
    {
        myChickenID = chickID;
    }
    public void SetSkillID(int skillID)
    {
        mySkillID = skillID;
    }

    public void SendChat(string chat)
    {
        if (chat.Length > 0)
        {
            chatMenu.AddChat(name, chat);
        }
    }
}
 