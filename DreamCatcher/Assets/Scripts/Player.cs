using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    //new stuff
    float jumpHeight = 3;
    float timeToJumpApex = .4f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float moveSpeed = 6;
    bool jumped;
    float gravity;
    float jumpVelocity;
    Vector2 velocity;
    float velocityXSmoothing;
    Controller2D controller;

    //public float speed;
    //public float jumpForce;
    public float panic;
    public string groundTag;
    public int numberOfHops;
    public float blinkDistance;
    public float blinkCooldown;
    public float portCooldown;
    public GameObject bluePort, orangePort;
    public float minPortDistance = .1f;
    public bool isInvincible = false;
    public bool canPull;
    public Lever lever;

    //default key binds
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.Space;
    private KeyCode blink = KeyCode.LeftShift;
    private KeyCode port = KeyCode.X; //tentative keybind, not sure what to put
    private KeyCode pull = KeyCode.W; //tentative ^^

    public bool grounded = false;

    //private bool jumpPressed = false;
    private int hopsRemaining = 0;
    private Rigidbody2D rb;
    private Vector2 jumpVector;
    private struct Cooldown
    {
        public float max, current;
        
        public Cooldown(float mx, float cur)
        {
            max = mx;
            current = cur;
        }
    }
    private Dictionary<string, Cooldown> cooldowns = new Dictionary<string, Cooldown>();
    private bool bluePortPlaced = false;
    private bool orangePortPlaced = false;
    private GameObject blueInstance, orangeInstance;
    private float maxPanic = 100f;

	void Start ()
    {
        controller = GetComponent<Controller2D>();
        jumped = false;
        canPull = false;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        //jumpVector = new Vector2(0, jumpForce);
        rb = this.GetComponent<Rigidbody2D>();

        //initializing cooldowns
        cooldowns["blink"] = new Cooldown(blinkCooldown, 0);
        cooldowns["port"] = new Cooldown(portCooldown, 0);

        controller.collisionMask =  ~(1 << LayerMask.NameToLayer("Player"));
	}
	
	void Update ()
    {
        TickCooldowns();
        Blink();
        Port();
        //possibly reduce movement rate while airborne
        Move();
        PullLever();
        //grounded = controller.collisions.below;
    }

    void FixedUpdate()
    {
    }

    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == groundTag)
        {
            grounded = true;
            hopsRemaining = numberOfHops;
            print("grounded");
        }
    }
    */

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "lever"){
            lever = other.GetComponent<Lever>();
            canPull = true;
        }
        if (other.tag== "ground"){
            // Return to level start
        }
    }


    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "lever"){
            canPull = false;
        }
    }

    void PullLever(){
        if (Input.GetKeyDown(pull) && canPull){
            lever.pull();
        }
    }


void Panic(int amount){
        if(!isInvincible){
            panic += amount;
        }
    }

    public void setInvinceable(bool Inv)
    {
        isInvincible = Inv;
    }

    public bool getInvinceable()
    {
        return isInvincible;
    }

    void Blink()
    {
        //check if blinking
        if (!Input.GetKey(blink))
        {
            return;
        }
        //check cd
        if (cooldowns["blink"].current != 0)
        {
            return;
        }
        //changing layer mask
        controller.collisionMask = (1 << LayerMask.NameToLayer("Unblinkable"));
        //blink in direction
        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            controller.Move(new Vector2(-blinkDistance, 0));
        }
        else if (Input.GetKey(right) && !Input.GetKey(left))
        {
            controller.Move(new Vector2(blinkDistance, 0));
        }
        //resetting collision
        controller.collisionMask = ~(1 << LayerMask.NameToLayer("Player"));
        resetCD("blink");
    }

    void Port()
    {
        if (!Input.GetKeyDown(port) || cooldowns["port"].current != 0)
        {
            return;
        }

        if (!bluePortPlaced)
        {
            blueInstance = (GameObject) Instantiate(bluePort, transform.position, Quaternion.identity);
            bluePortPlaced = true;
            resetCD("port");
        }
        else if (!orangePortPlaced)
        {
            if (Vector2.Distance(transform.position, blueInstance.transform.position) > minPortDistance)
            {
                orangeInstance = (GameObject) Instantiate(orangePort, transform.position, Quaternion.identity);
                orangePortPlaced = true;
                resetCD("port");
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, blueInstance.transform.position) < minPortDistance)
            {
                transform.position = orangeInstance.transform.position;
                resetCD("port");
            }
            else if (Vector2.Distance(transform.position, orangeInstance.transform.position) < minPortDistance)
            {
                transform.position = blueInstance.transform.position;
                resetCD("port");
            }
            else
            {
                Destroy(blueInstance);
                Destroy(orangeInstance);
                bluePortPlaced = false;
                orangePortPlaced = false;
                resetCD("port");
            }
        }
    }

    void Move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            jumped = false;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
       

        if (Input.GetKey(jump) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            jumped = true;
        }

        if (Input.GetKeyDown(jump) && !controller.collisions.below && jumped)
        {
            velocity.y = jumpVelocity;
            jumped = false;
        }

        controller.Move(velocity * Time.deltaTime);

        //old code
        /*
        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            this.transform.position += new Vector3(-this.speed * Time.deltaTime, 0, 0);
        }
        else if (!Input.GetKey(left) && Input.GetKey(right))
        {
            this.transform.position += new Vector3(this.speed * Time.deltaTime, 0, 0);
        }

        bool jumpPressed = Input.GetKeyDown(jump);
        if (jumpPressed && grounded)
        {
            rb.AddForce(jumpVector);
            grounded = false;
            print("normal");
        }
        else if (!grounded && jumpPressed && hopsRemaining > 0)
        {
            rb.AddForce(jumpVector);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            hopsRemaining--;
            print("double");
        } */
    }

    void TickCooldowns()
    {
        foreach (string key in new List<string>(cooldowns.Keys))
        {
            float val = cooldowns[key].current;
            val -= Time.deltaTime;
            if (val < 0)
            {
                val = 0;
            }
            cooldowns[key] = new Cooldown(cooldowns[key].max, val);
        }
    }

    void resetCD(string cd)
    {
        cooldowns[cd] = new Cooldown(cooldowns[cd].max, cooldowns[cd].max);
    }
}
