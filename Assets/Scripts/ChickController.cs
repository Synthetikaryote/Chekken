﻿using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour
{

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

   ////JARED CODE////
    private MenuManager menuMan;

    //player name
    private TextMesh playerName;
   // //END////

    public ParticleSystem Effect;

<<<<<<< HEAD
    ////JARED CODE////
    void Awake()
    {
        menuMan = FindObjectOfType<MenuManager>();
        playerName = GetComponentInChildren<TextMesh>();
    }
    ////END////
=======
    //Ability Cooldown
    public float mCooldown;

    public void AddVelocity(Vector3 vel)
    {
        //additionalVelocity = vel;
    }
>>>>>>> origin/master

    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myRenderer = gameObject.GetComponent<Collider>();
        grounded = true;
        hasAirJump = true;

        ////JARED CODE////
        if (!menuMan)
        {
            Debug.LogError("[ChickController.cs] Cannot find the menu manager!");
        }
        if (!playerName)
        {
            Debug.LogError("[ChickController.cs] Cannot find the name text!");
        }

        playerName.text = menuMan.playerName;
        ////END////
    }


	void Update ()
    {
<<<<<<< HEAD
        
        print("Player name: " + playerName.text);
=======
        //Cooldown Timer: It takes a cooldown value from an Ability script, and subtracts it until it reaches zero. The ability script cannot fire off again until it reaches zero.
        #region Cooldown
        if (mCooldown > 0.0f)
        {
            mCooldown = mCooldown - 1.0f;
        }
        //Debug.Log(mCooldown);
        #endregion
>>>>>>> origin/master

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
            myBody.velocity = new Vector3( newSpeed * Input.GetAxisRaw( "Horizontal" ), myBody.velocity.y, 0.0f );        
        }
        else
        {
            myBody.velocity = new Vector3( 0.0f, myBody.velocity.y, 0.0f );
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
