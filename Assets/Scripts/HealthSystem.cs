using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    //Public members
    public float maxHealth;
    public GameObject m_DeathEffect;
    //Private members
    private float curHealth;
    [SerializeField]
    private ChickLocal m_ChickLocalcs;
	// Use this for initialization
	void Start ()
    {
        curHealth = maxHealth;
        m_ChickLocalcs = GetComponent<ChickLocal>();
        //m_DeathEffect = GameObject.Find("DeathEffects");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (curHealth <= 0.0f) //Code for Chicken Death
        {
            Debug.Log("You Died.");
            //m_ChickLocalcs.ExplosionAudio.Play();
            //m_ChickLocalcs.ExplosionEffect.Play();
            Instantiate(m_DeathEffect);
            m_DeathEffect.transform.position = m_ChickLocalcs.transform.position;
            //Invoke("Death", 0.1f);
            var controller = GetComponent<ChickController>();
            controller.ui.gameObject.SetActive(false);
            gameObject.SetActive(false);
            //if (m_ChickLocalcs != null)
            Invoke("Respawn", 5f);
            
            //AnimateCharacter("dead");
        }
    }

    void Respawn()
    {
        // set position to a spawn point
        transform.position = ChickenSpawnerManager.Instance.GetRandomSpawnLocation();
        SetHealth(maxHealth);
        if (m_ChickLocalcs != null)
            m_ChickLocalcs.serverCommunication.UpdateHealth(m_ChickLocalcs.serverID, curHealth);
        gameObject.SetActive(true);
        var controller = GetComponent<ChickController>();
        controller.ui.gameObject.SetActive(true);
    }

    public void Death()
    {
        
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
