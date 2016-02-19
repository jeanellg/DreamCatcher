using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

	public int numberOfStates;
	//BaseState is always 0
	public int state = 0;
	public bool isTimed;
	public float timer;
	public float timeRemaining;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){
		if (isTimed){
			if (state != 0){
				timeRemaining -= Time.deltaTime;
			}
			if (timeRemaining <= 0){
				state = 0;
			}
		}
	}
	void updateState() {
		state += 1;
		timeRemaining = timer;
		if (state > numberOfStates){
			state = 0;
		}
	}

	public int getState(){
		return state;
	}
	public void pull() {
		updateState();
	}
}
