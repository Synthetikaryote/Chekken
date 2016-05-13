using UnityEngine;
using System.Collections;

public class ScreenResizer : MonoBehaviour
{
    public float desiredWidth;
    public float desiredHeight;

    public float threshold = 0.5f;

    Camera camera;

	// Use this for initialization
	void Start ()
    {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float currentWidth = camera.orthographicSize * 2.0f * Screen.width / Screen.height;
        float currentHeight = camera.orthographicSize * 2.0f;
        if (currentWidth < desiredWidth)
        {
            camera.orthographicSize = desiredWidth * Screen.height / Screen.width * 0.5f;
        }
        else if(currentWidth - threshold > desiredWidth && currentHeight >  desiredHeight)
        {
            camera.orthographicSize = desiredHeight * 0.5f;
        }
	}
}
