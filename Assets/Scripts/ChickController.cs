using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour {

    //horizontal movement
    public float accelFactor;
    public float initSpeed;
    public float maxSpeed;

    //jumping
    public float jumpHeight;
    public float doubJumpHeight;

    private bool grounded;
    private bool hasAirJump;


    private Rigidbody myBody;
    

    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        grounded = true;
        hasAirJump = true;
    }


	void Update ()
    {
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
            if( newSpeed == maxSpeed )
                Debug.Log( "MaxSpeed Reached!" );
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
                grounded = false;
                myBody.AddForce(0.0f, jumpHeight, 0.0f);
            }
            else if (hasAirJump)
            {
                hasAirJump = false;
                myBody.velocity = Vector3.zero;
                myBody.AddForce(0.0f,doubJumpHeight, 0.0f);
            }
        }


    }

    void OnCollisionEnter()
    {
        grounded = true;
        hasAirJump = true;
    }

}
