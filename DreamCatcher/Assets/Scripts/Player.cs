using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public string groundTag;

    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.Space;
    private Vector2 jumpVector;
    private bool grounded = false;

	// Use this for initialization
	void Start ()
    {
        jumpVector = new Vector2(0, jumpForce);
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
            this.transform.GetComponent<Rigidbody2D>().AddForce(jumpVector);
            grounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == groundTag)
        {
            grounded = true;
        }
    }
}
