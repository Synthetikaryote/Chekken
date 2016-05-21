using UnityEngine;
using System.Collections;

public class ChickLocal : ChickController
{
    //horizontal movement
    public float accelFactor = 20.0f;
    public float initSpeed = 15.0f;
    public float maxSpeed = 30.0f;

    //for roatation
    float tExample = 0.0f;

    //raycast distance
    private const float detectDist = 0.5f;

    //jumping
    private bool grounded;
    private bool hasAirJump;


    protected override void Start() 
    {
        base.Start();
        grounded = true;
        hasAirJump = true;
    }

    protected override void Update()
    {
        base.Update();
        //ground Detection
        Vector3 myPos = gameObject.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Vector3 modifier = new Vector3(myRenderer.bounds.size.x * 0.5f, 0.0f, 0.0f);
        Ray leftRay = new Ray(myPos + modifier, Vector3.down);
        Ray rightRay = new Ray(myPos - modifier, Vector3.down);

        Debug.DrawRay(leftRay.origin, leftRay.direction, Color.red);
        Debug.DrawRay(rightRay.origin, leftRay.direction, Color.red);

        if (Physics.Raycast(leftRay, detectDist) || Physics.Raycast(rightRay, detectDist))
        {
            grounded = true;
            hasAirJump = true;
        }
        else
        {
            grounded = false;
        }


        //Horizontal Movement
        if (Input.GetButton("Horizontal"))
        {
            float newSpeed;
            //this tests if the the player is still pressing the button in the same direction. 
            if (Input.GetAxisRaw("Horizontal") * myBody.velocity.x > 0)
            {
                newSpeed = Mathf.Max(initSpeed, Mathf.Abs(myBody.velocity.x));
            }
            else
            {
                newSpeed = initSpeed;
                tExample = 0.0f;
            }

            newSpeed = Mathf.Min(maxSpeed, newSpeed + (accelFactor * Time.deltaTime));
            myBody.velocity = new Vector3(newSpeed * Input.GetAxisRaw("Horizontal"), myBody.velocity.y, 0.0f);

            //rotation
            tExample += Time.deltaTime;
            radLerpValue = Mathf.Lerp(0.0f, Mathf.PI * 0.15f, tExample * rotationSpeedMultipier) * Input.GetAxisRaw("Horizontal");
            this.transform.rotation = new Quaternion(this.transform.rotation.x, 1.0f, 0.0f, radLerpValue);

            AnimateCharacter("move");

        }
        else
        {
            myBody.velocity = new Vector3(0.0f, myBody.velocity.y, 0.0f);
            //this.transform.rotation = new Quaternion(0, 1, 0, radLerpValue);
            radLerpValue = 0.0f;
        }

        //jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                JumpEffect.Play();
                JumpAudio.Play();
                grounded = false;
                myBody.AddForce(0.0f, jumpHeight, 0.0f);
                AnimateCharacter("jump");
            }
            else if (hasAirJump)
            {
                JumpEffect.Play();
                JumpAudio.Play();
                hasAirJump = false;
                myBody.velocity = Vector3.zero;
                myBody.AddForce(0.0f, doubJumpHeight, 0.0f);
                AnimateCharacter("jump");
            }
        }


        //Chicken Dead, Chicken Stop moving, Explosion, Disappear, Dead Audio 
        if (Input.GetKeyDown(KeyCode.F1))
        {

            ExplosionEffect.Play();
            ExplosionAudio.Play();
            IsAlive = true;
            this.transform.FindChild("Chick").gameObject.SetActive(false);
            this.GetComponent<BoxCollider>().enabled = false;
            AnimateCharacter("dead");
            //anim.SetTrigger(die);
        }
        if (IsAlive)
        {
            if (!ExplosionEffect.IsAlive())
            {
                //ChickenSpawnerManager.Instance.ChickenIsDead();
                Destroy(this.gameObject);
            }
        }

    }
}
