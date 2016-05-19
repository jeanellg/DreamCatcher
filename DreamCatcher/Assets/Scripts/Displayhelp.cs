using UnityEngine;
using System.Collections;

public class Displayhelp : MonoBehaviour {
	public string message = "";
	bool display= false;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			display = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag=="Player")
		{
			display = false;
		}
	}
	void OnGUI()
	{
		if(display)
		{
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 4, 200f, 200f), message);
		}
	}
}
