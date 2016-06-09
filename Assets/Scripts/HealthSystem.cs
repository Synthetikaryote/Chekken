using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    //Public members
    public float maxHealth;

    //Private members
    private float curHealth;
    [SerializeField]
    private ChickLocal m_ChickLocalcs;
	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
        m_ChickLocalcs = GetComponent<ChickLocal>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (curHealth <= 0.0f) //Code for Chicken Death
        {
            Debug.Log("You Died.");
            this.gameObject.SetActive(false);
            
            
            
            //m_ChickLocalcs.ChickLive = false; POSSIBLY DEFUNCT
            //AnimateCharacter("dead");
        }
    }

    

    //Health funcs
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
    }
    public float GetCurHealth()
    {
        return curHealth;
    }
    public void SetHealth(float newHealth)
    {
        curHealth = newHealth;
    }
}
