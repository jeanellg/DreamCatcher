using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public string groundTag;
    public int numberOfHops;

    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.Space;
    private bool grounded = false;
    private int hopsRemaining = 0;
    private Rigidbody2D rb;
    private Vector2 jumpVector;

	// Use this for initialization
	void Start ()
    {
        jumpVector = new Vector2(0, jumpForce);
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //possibly reduce movement rate while airborne
        if (Input.GetKey(left) && !Input.GetKey(right))
        {
            this.transform.position += new Vector3(-this.speed * Time.deltaTime, 0, 0);
        }
        else if (!Input.GetKey(left) && Input.GetKey(right))
        {
            this.transform.position += new Vector3(this.speed * Time.deltaTime, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(jump) && grounded)
        {
            rb.AddForce(jumpVector);
            grounded = false;
        }
        else if (!grounded && Input.GetKeyDown(jump) && hopsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(jumpVector);
            hopsRemaining--;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == groundTag)
        {
            grounded = true;
            hopsRemaining = numberOfHops;
        }
    }
}
