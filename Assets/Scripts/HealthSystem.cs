using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    //Public members
    public float maxHealth;

    //Private members
    private float curHealth;

    private ServerCommunication serverCommunication;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
        serverCommunication = FindObjectOfType<ServerCommunication>();
    }
	
	// Update is called once per frame
	void Update ()
    {    }

    //Health funcs
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (damage != 0f)
        {
            var chickController = GetComponent<ChickController>();
            serverCommunication.UpdateHealth(chickController.serverID, curHealth);
        }
    }
    public float GetCurHealth()
    {
        return curHealth;
    }
}
