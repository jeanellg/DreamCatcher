using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Lever lever;
	public bool isOpen;
	public int openOn = 1;

	// Use this for initialization
	void Start () {
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (lever.getState() == openOn){
			GetComponent<SpriteRenderer>().color = Color.black;
			GetComponent<BoxCollider2D>().enabled = false;
		}
		else{
			GetComponent<SpriteRenderer>().color = Color.white;
			GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
