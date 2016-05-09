using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class NewPlayer : MonoBehaviour {

    float jumpHeight = 3;
    float timeToJumpApex = .4f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float moveSpeed = 6;
    float cooldownMax = 3.0f;
    float cooldown = 0;
    float flashDistance = 5.0f;
    int health = 5;

    bool isCooldown;
    bool jumped;
    float gravity;
    float jumpVelocity;
    Vector2 velocity;
    float velocityXSmoothing;
    NewLever lever;


    Controller2D controller;


    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();
        jumped = false;
        isCooldown = false;

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        
	}

    
    void OnTriggerEnter2D(Collider2D target)
    {
        //set the current lever to the colliding lever
        
        if (target.tag == "lever")
            lever = target.GetComponent<NewLever>();
           
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if a player leaves the lever, set the currrent lever to null
        if (other.tag == "lever")
            lever = null;
    }


    // Update is called once per frame
    void Update () {

        //ceiling and floor detection
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            jumped = false;
        }

        //grab user input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //if a player is on a lever, and Up key is pressed, activate the lever.
        if (lever != null && Input.GetKeyDown(KeyCode.W))
        {
            lever.pull();
        }

        //if a player in on ground and Space is pressed, jump.
        if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            jumped = true;
        }

        //if a player is in air after jumping once, allow the player to double jump
        if (Input.GetKeyDown(KeyCode.Space) && !controller.collisions.below && jumped)
        {
            velocity.y = jumpVelocity;
            jumped = false;
        }

        //if teleport button is pressed while pressing direction, a player flashes certain distance to that direction.
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCooldown)
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                //velocity.y = 0;
                isCooldown = true;
                controller.Move(new Vector2(-flashDistance, 0));
            }

            else if (Input.GetKey(KeyCode.D))
            {
                //velocity.y = 0;
                isCooldown = true;
                controller.Move(new Vector2(flashDistance, 0));
            }
            
            
        }

        //cooldown counter for flash
        if (isCooldown)
            cooldown += Time.deltaTime;

        //if cooldown complete, allow a player to flash once again
        if (cooldown >= cooldownMax)
        {
            cooldown = 0;
            isCooldown = false;
        }

        //apply all movement values into physics.
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? accelerationTimeGrounded :accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);
	}
}
