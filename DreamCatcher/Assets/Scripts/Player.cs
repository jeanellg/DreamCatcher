using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public string groundTag;
    public int numberOfHops;
    public float blinkDistance;
    public float blinkCooldown;

    //default key binds
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.Space;
    private KeyCode blink = KeyCode.LeftShift;


    private bool grounded = false;
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

	// Use this for initialization
	void Start ()
    {
        jumpVector = new Vector2(0, jumpForce);
        rb = this.GetComponent<Rigidbody2D>();

        //initializing cooldowns
        cooldowns["blink"] = new Cooldown(blinkCooldown, 0);

	}
	
	// Update is called once per frame
	void Update ()
    {
        TickCooldowns();
        Blink();
        //possibly reduce movement rate while airborne
        Move();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(jump) && grounded)
        {
            rb.AddForce(jumpVector);
            grounded = false;
            print("normal");
        }
        else if (!grounded && Input.GetKeyDown(jump) && hopsRemaining > 0)
        {
            rb.AddForce(jumpVector);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            hopsRemaining--;
            print("double");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == groundTag)
        {
            grounded = true;
            hopsRemaining = numberOfHops;
            print("grounded");
        }
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
        //get direction
        Vector2 delta;
        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            delta = new Vector2(-blinkDistance, 0);
        }
        else if (Input.GetKey(right) && !Input.GetKey(left))
        {
            delta = new Vector2(blinkDistance, 0);
        }
        else
        {
            delta = Vector2.zero;
        }
        //calculate validity
        //execute blink
        this.transform.position += (Vector3) delta;
        resetCD("blink");
    }

    void Move()
    {
        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            this.transform.position += new Vector3(-this.speed * Time.deltaTime, 0, 0);
        }
        else if (!Input.GetKey(left) && Input.GetKey(right))
        {
            this.transform.position += new Vector3(this.speed * Time.deltaTime, 0, 0);
        }
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
