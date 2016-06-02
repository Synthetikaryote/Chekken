using UnityEngine;
using System.Collections;

public class AnimatUV : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    public bool IsRun = false;

    public float offset;

    public Material mat;

	// Use this for initialization
	void Start ()
    {
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        offset = Time.time * scrollSpeed % 1;

        if(IsRun == true)
        {
            mat.mainTextureOffset = new Vector2(0, offset);
        }

    }
}
