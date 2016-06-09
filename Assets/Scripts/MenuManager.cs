using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; protected set; }

    public GameObject chickenSelectMenu;
    public InputField inputMenu;
    public string playerNameTemp;
    string chatDisplayName;
    int myChickenID;
    int mySkillID;
    public bool chatMenuOn { private set; get; }
    private ServerCommunication servCom;
    ChatMenu chatMenu;

    public Text UIText;
    public GameObject UIChatGO;
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
        servCom = gameObject.GetComponent<ServerCommunication>();
        chatMenu.Intialize(UIText, UIInputField, UIChatGO, servCom);
        chatMenu.MenuToggle(chatMenuOn);
    }
    void ResetVariable()
    {
        myChickenID = -1;
        mySkillID = -1;
        playerNameTemp = "";
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
        if (playerNameTemp == "")
        {
            Debug.LogError("[Menu Manager]Name field is empty");
            failed = true;
        }
        if (!failed)
        {

            //check if chicken and id exist
            Debug.Log(playerNameTemp);
            chatDisplayName = playerNameTemp;
            var chicken = ChickenSpawnerManager.Instance.SpawnChicken(myChickenID, mySkillID, chatDisplayName);
            if (chicken != null)
            {
                //call name setter here
                chickenSelectMenu.SetActive(false);
                GetComponent<MultiplayerManager>().GameStart(chicken, playerNameTemp);
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

        if (Input.GetKeyDown(KeyCode.Return) && !chickenSelectMenu.activeInHierarchy)
        {
            if (!chatMenu.HasText())
            {
                chatMenuOn = !chatMenuOn;
                chatMenu.MenuToggle(chatMenuOn);
            }
            else
            {
                chatMenu.UpdateField(chatDisplayName);
            }
        }
    }
    //menu setter
    public void SetName(string name)
    {
        playerNameTemp  = name;
    }
    public void SetChickenID(int chickID)
    {
        myChickenID = chickID;
    }
    public void SetSkillID(int skillID)
    {
        mySkillID = skillID;
    }
}
 