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
            var controller = GetComponent<ChickController>();
            controller.ui.gameObject.SetActive(false);
            gameObject.SetActive(false);

            Invoke("Respawn", 5f);
            
            
            //m_ChickLocalcs.ChickLive = false; POSSIBLY DEFUNCT
            //AnimateCharacter("dead");
        }
    }

    void Respawn()
    {
        // set position to a spawn point
        transform.position = ChickenSpawnerManager.Instance.GetRandomSpawnLocation();
        SetHealth(maxHealth);
        gameObject.SetActive(true);
        var controller = GetComponent<ChickController>();
        controller.ui.gameObject.SetActive(true);
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
