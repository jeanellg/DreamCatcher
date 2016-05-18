using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	public KeyCode On = KeyCode.N;
	public string floatTag;
	public bool canFloat;
	public float rate = 200f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
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
