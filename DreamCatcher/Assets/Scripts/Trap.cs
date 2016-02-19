using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	public bool isActive;
	public Rigidbody2D dart;
	public int numberOfDarts =1;
	public Lever lever;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (isActive && numberOfDarts>0 && lever.getState() ==1){
		Rigidbody2D dartInstance = Instantiate (dart, transform.position, Quaternion.identity) as Rigidbody2D;
		dartInstance.velocity = new Vector2(-10,0);
		numberOfDarts--;
	}
	}
}
