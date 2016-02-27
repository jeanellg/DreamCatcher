using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	public Rigidbody2D rb;
	public KeyCode On = KeyCode.N;
	public string floatTag;
	public bool canFloat;
	public float rate = 200f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(On) && canFloat){
			rb.gravityScale = 0f;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0, rate));
		}
		else{
			rb.gravityScale = 1f;
		}
	}
	 void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == floatTag)
        {
            canFloat = true;
        }
    }
    void OnTriggerExit2D(Collider2D col){
    	if (col.gameObject.tag == floatTag){
    	canFloat = false;
    	}
    }
}
