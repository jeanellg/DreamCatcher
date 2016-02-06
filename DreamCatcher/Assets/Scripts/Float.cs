using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	public GameObject player;
	public Rigidbody2D rb;
	public KeyCode On = KeyCode.N;
	public string floatTag;
	public bool canFloat;
	public float rate = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(On) && canFloat){
			rb.gravityScale = 0f;
			player.transform.position += new Vector3(0,rate*Time.deltaTime,0);
		}
		else{
			rb.gravityScale = 1f;
		}
	}
	 void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == floatTag)
        {
            canFloat = true;
        }
    }
}
