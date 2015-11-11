using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

	public int numberOfStates;
	//BaseState is always 0
	public int state = 0;
	public bool isTimed;
	public float timer;
	private float timeRemaining;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (state != 0){
			if (isTimed){
				timeRemaining = timer;
				timeRemaining -= Time.deltaTime;
			}
			if (timeRemaining <= 0){
				timeRemaining = timer;
				state = 0;
			}
		}
	}

	void updateState() {
		state += 1;
		if (state > numberOfStates){
			state -= numberOfStates;
		}
	}

	int getState(){
		return state;
	}
}
