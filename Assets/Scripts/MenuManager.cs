using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; protected set; }

    public GameObject chickenSelectMenu;
    int myChickenID;
    int mySkillID;
    void Awake()
    {
        Instance = this;
        if (chickenSelectMenu == null)
        {
            Debug.Log("[Menu Manager] no UI_CharacteMenu");
        }
    }
    void Start()
    {
        myChickenID = -1;
        mySkillID = -1;
    }
    public void SetChickenID(int chickID)
    {
        myChickenID = chickID;
    }
    public void SetSkillID(int skillID)
    {
        mySkillID = skillID;
    }
    public void StartGame()
    {
        if (myChickenID != -1
            && mySkillID != -1)
        {
            //check if chicken and id exist
            if (ChickenSpawner.Instance.SpawnChicken(myChickenID, mySkillID))
            {
                chickenSelectMenu.SetActive(false);
            }
            
        }
        else
        {
            Debug.Log("No chicken / Skill selected");
        }

    }
}
 