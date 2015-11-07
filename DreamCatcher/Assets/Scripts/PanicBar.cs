using UnityEngine;
using System.Collections;

public class PanicBar : MonoBehaviour {

	public int fullfill = 100;
	public int currentfill = 0;
	public int delay = 1;
	public int courage;
	public bool scared = false;
	public int scaredTimer= delay;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		scaredTimer -= Time.fixedDeltaTime;

		if (scared && scaredTimer <= 0){
			currentfill -= courage;
		}
		if (scared && scaredTimer <= -2 * delay){
			currentfill -= courage;
		}
		if (currentfill <= 0){
			currentfill = 0;
			scaredTimer = delay
			scared = false;
		}

	}

	void FillBar(int amount) {
		currentfill += amount;
		scared = true;
	}

}
