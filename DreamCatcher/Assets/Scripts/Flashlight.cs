using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {


	public bool isActivated;
	public bool isEquipt;
	private int maxCharge;
	public int charge;
	public SpriteRenderer Light;
	private KeyCode On = KeyCode.B;

	// Use this for initialization
	void Start () {
		isEquipt = false;
		Light.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(On)){
			this.isActivated = !this.isActivated;
		}


		if (isEquipt) {
			Light.enabled = isActivated;
		}
	}

	void updateEquipt(){
		this.isEquipt = !this.isEquipt;
	}

	bool getActive(){
		return isEquipt && isActivated;
	}
}
