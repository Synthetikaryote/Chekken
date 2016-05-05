using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    //Public members
    public Color fullHealthColour;
    public Color lowHealthColour;

    //Private member
    private HealthSystem chickHealth;
    private Renderer barRenderer;
    private Transform chickTransform;

    void Awake()
    {
        //chickHealth = GetComponentInParent<HealthSystem>();
        chickHealth = GetComponentInParent<ChickUI>().target.GetComponent<HealthSystem>();
        barRenderer = GetComponent<Renderer>();
        chickTransform = GetComponentInParent<Transform>();
    }

	// Use this for initialization
	void Start ()
    {
	    if (!chickHealth)
        {
            Debug.LogError("[HealthBar.cs] Cannot find the chick's health!");
        }
        if (!barRenderer)
        {
            Debug.LogError("[HealthBar.cs] Cannot find the health bar's renderer!");
        }
        if (!chickTransform)
        {
            Debug.LogError("[HealthBar.cs] Cannot find the chick's transform!");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {       
        transform.localScale = new Vector3(chickHealth.GetCurHealth() / 166.7f, 0.1f, 0.000001f);

        Color barColour = Color.Lerp(lowHealthColour, fullHealthColour, (chickHealth.GetCurHealth() / chickHealth.maxHealth));
        barRenderer.material.SetColor("_Color", barColour);
	}
}
