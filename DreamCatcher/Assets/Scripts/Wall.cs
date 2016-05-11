using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public bool isDestructable;
	public Lever lever;
	public int state;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestructable){
			if (state <= lever.getState()){
				Destroy(gameObject);
			}
		}
	}

	
}
