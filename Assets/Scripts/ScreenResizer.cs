using UnityEngine;
using System.Collections;

public class ScreenResizer : MonoBehaviour
{
    public float desiredWidth;
    public float desiredHeight;

    public float threshold = 0.5f;

    Camera targetCamera;

	// Use this for initialization
	void Start ()
    {
        targetCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float currentWidth = targetCamera.orthographicSize * 2.0f * Screen.width / Screen.height;
        float currentHeight = targetCamera.orthographicSize * 2.0f;
        if (currentWidth < desiredWidth)
        {
            targetCamera.orthographicSize = desiredWidth * Screen.height / Screen.width * 0.5f;
        }
        else if(currentWidth - threshold > desiredWidth && currentHeight >  desiredHeight)
        {
            targetCamera.orthographicSize = desiredHeight * 0.5f;
        }
	}
}
