﻿using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour
{
    //player name
    public string playerName;

    //horizontal movement
    public float accelFactor;
    public float initSpeed;
    public float maxSpeed;

    //jumping
    public float jumpHeight;
    public float doubJumpHeight;
    private const float detectDist = 0.5f;

    private bool grounded;
    private bool hasAirJump;

    private Rigidbody myBody;
    private Collider myRenderer;

    private Vector3 additionalVelocity;

    public ParticleSystem Effect;

    public void AddVelocity(Vector3 vel)
    {
        additionalVelocity = vel;
    }

    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myRenderer = gameObject.GetComponent<Collider>();
        grounded = true;
        hasAirJump = true;

        additionalVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        GetComponentInChildren<TextMesh>().text = playerName;
    }


	void Update ()
    {
        //ground Detection
        Vector3 myPos = gameObject.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Vector3 modifier = new Vector3(myRenderer.bounds.size.x * 0.5f, 0.0f, 0.0f);
        Ray leftRay = new Ray(myPos + modifier, Vector3.down);
        Ray rightRay = new Ray( myPos - modifier, Vector3.down );

        Debug.DrawRay(leftRay.origin, leftRay.direction, Color.red);
        Debug.DrawRay( rightRay.origin, leftRay.direction, Color.red );

        if( Physics.Raycast( leftRay, detectDist) || Physics.Raycast(rightRay, detectDist))
        {
            grounded = true;
            hasAirJump = true;
        }
        else
        {
            grounded = false;
        }

        //Horizontal Movement
        if( Input.GetButton( "Horizontal" ) )
        {
            float newSpeed;
            //this tests if the the player is still pressing the button in the same direction. 
            if( Input.GetAxisRaw( "Horizontal" ) * myBody.velocity.x > 0 )
                newSpeed = Mathf.Max( initSpeed, Mathf.Abs( myBody.velocity.x ) );
            else
                newSpeed = initSpeed;
            
            newSpeed = Mathf.Min( maxSpeed, newSpeed + (accelFactor * Time.deltaTime) );
            myBody.velocity = new Vector3( newSpeed * Input.GetAxisRaw( "Horizontal" ), myBody.velocity.y, 0.0f ) + additionalVelocity;        
        }
        else
        {
            myBody.velocity = new Vector3( 0.0f, myBody.velocity.y, 0.0f ) + additionalVelocity;
        }


        //jumping
        if (Input.GetButtonDown("Jump"))
        {

            if (grounded)
            {
                Effect.Play();
                grounded = false;
                myBody.AddForce(0.0f, jumpHeight, 0.0f);

            }
            else if (hasAirJump)
            {
                Effect.Play();
                hasAirJump = false;
                myBody.velocity = Vector3.zero;
                myBody.AddForce(0.0f,doubJumpHeight, 0.0f);

            }
        }
    }
}
