using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour {

    public float acceleration;
    public float maxSpeed;
    public float jumpHeight;
    public float doubJumpHeight;

    private Rigidbody myBody;
    private bool grounded;
    private bool hasAirJump;

    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        grounded = true;
        hasAirJump = true;
    }


	void FixedUpdate ()
    {
        //Horizontal Movement
        if (Input.GetButton("Horizontal"))
        {
            if (myBody.velocity.x < maxSpeed && myBody.velocity.x > maxSpeed * -1)
                myBody.AddForce(acceleration * Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            else
                myBody.velocity.Set(maxSpeed * Input.GetAxis("Horizontal"), myBody.velocity.y, 0.0f);
        }

        //jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                grounded = false;
                myBody.AddForce(0.0f, jumpHeight, 0.0f);
                Debug.Log("jumped!");
            }
            else if (hasAirJump)
            {
                hasAirJump = false;
                myBody.velocity = Vector3.zero;
                myBody.AddForce(0.0f,doubJumpHeight, 0.0f);
                Debug.Log("Air jumped!");
            }
        }


    }

    void OnCollisionEnter()
    {
        grounded = true;
        hasAirJump = true;
        Debug.Log("Grounded!");
    }

}
