using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public NewLever lever;
    public bool isOpen;
	

	// Use this for initialization
	void Start () {
		isOpen = lever.getState();
	}
	
	// Update is called once per frame
	void Update () {
        isOpen = lever.getState();

		if (isOpen)
        {
			GetComponent<SpriteRenderer>().color = Color.black;
			GetComponent<BoxCollider2D>().enabled = false;
            
		}
		else
        {
			GetComponent<SpriteRenderer>().color = Color.white;
			GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
