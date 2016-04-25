using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;
    void Update()
    {
        Vector3 rotation = new Vector3(xSpeed, ySpeed, zSpeed);
        transform.Rotate(rotation * Time.deltaTime);
    }
}
