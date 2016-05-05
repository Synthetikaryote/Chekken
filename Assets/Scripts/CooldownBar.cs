using UnityEngine;
using System.Collections;

public class CooldownBar : MonoBehaviour
{
    //Public members
    public Color barColour;

    public Vector3 offset;

    //Private members
    private Renderer barRenderer;
    private ChickController chickControl;
    private TextMesh cdTimer;

    private float curCD;
    private float maxCD;

    void Awake()
    {
        barRenderer = GetComponent<Renderer>();
        chickControl = GetComponentInParent<ChickUI>().target.GetComponent<ChickController>();
        cdTimer = FindObjectOfType<TextMesh>();
    }

	// Use this for initialization
	void Start ()
    {
        if (!barRenderer)
        {
            Debug.LogError("[CooldownBar.cs] Cannot find the cooldown bar's renderer!");
        }
        if (!chickControl)
        {
            Debug.LogError("[CooldownBar.cs] Cannot find the chick's controller!");
        }
        if(!cdTimer)
        {
            Debug.LogError("[CooldownBar.cs] Cannot find the cooldown text!");
        }

        maxCD = 1.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        curCD = chickControl.mCooldown;

        if(curCD > maxCD)
        {
            maxCD = curCD;
        }
        
        barRenderer.material.SetColor("_Color", barColour);

        print("Max CD: " + maxCD.ToString());
      
        if (( curCD >= 0.0f))
        {
            cdTimer.gameObject.SetActive(true);
            transform.localScale = new Vector3(Mathf.Abs((curCD / maxCD) - 1.0f), 1.0f, Mathf.Abs((curCD / maxCD) - 1.0f));
            if(curCD >= 1.0f)
            {
                cdTimer.text = curCD.ToString("F0");
            }
        }
        else
        {
            cdTimer.gameObject.SetActive(false);
        }
    }
}
