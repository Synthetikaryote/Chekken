using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; protected set; }

    public GameObject chickenSelectMenu;
    string playerName;
    int myChickenID;
    int mySkillID;
    void Awake()
    {
        Instance = this;
        if (chickenSelectMenu == null)
        {
            Debug.Log("[Menu Manager] no UI_CharacteMenu");
            Debug.Break();
        }
    }
    void Start()
    {
        ResetVariable();
    }
    void ResetVariable()
    {
        myChickenID = -1;
        mySkillID = -1;
        playerName = "";
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
        }
        if (!failed)
        {
            //check if chicken and id exist
            if (ChickenSpawnerManager.Instance.SpawnChicken(myChickenID, mySkillID))
            {
                //call name setter here
                Debug.Log("[Menu Manager] name is : " + playerName);
                ResetVariable();
                chickenSelectMenu.SetActive(false);
            }
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
}
 