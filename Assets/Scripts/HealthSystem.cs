using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    //Public members
    public float maxHealth;

    //Private members
    private float curHealth;

	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {    }

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
