using UnityEngine;
using System.Collections;

public class PanicBar : MonoBehaviour {

	public float fullfill = 100f;
	public float currentfill = 0f;
	public float delay = 1f;
	public float courage = 1f;
	public bool scared = false;
	public float scaredTimer;


	// Use this for initialization
	void Start () {
		scaredTimer = delay;
	}
	
	// Update is called once per frame
	void Update () {
		scaredTimer -= Time.deltaTime;

		if (scared && scaredTimer <= 0f){
			currentfill -= courage* Time.deltaTime;
		}
		if (scared && scaredTimer <= -2f * delay){
			currentfill -= courage * Time.deltaTime;
		}
		if (currentfill <= 0f){
			currentfill = 0f;
			scaredTimer = delay;
			scared = false;
		}

	}

	void FillBar(int amount) {
		currentfill += amount;
		scared = true;
	}

}
