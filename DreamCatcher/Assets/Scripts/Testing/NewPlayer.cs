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
    Lever lever;
    bool canPull;

    Controller2D controller;


    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();
        jumped = false;
        isCooldown = false;
        canPull = false;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "lever")
        {
            lever = other.GetComponent<Lever>();
            Destroy (lever);
            canPull = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "lever")
        {
            canPull = false;
        }
    }


    // Update is called once per frame
    void Update () {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            jumped = false;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (canPull && Input.GetKeyDown(KeyCode.W))
        {
            lever.pull();
        }

        if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            jumped = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !controller.collisions.below && jumped)
        {
            velocity.y = jumpVelocity;
            jumped = false;
        }

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

        if (isCooldown)
            cooldown += Time.deltaTime;

        if (cooldown >= cooldownMax)
        {
            cooldown = 0;
            isCooldown = false;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? accelerationTimeGrounded :accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity* Time.deltaTime);
	}
}
