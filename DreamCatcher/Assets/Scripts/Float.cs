using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	public GameObject player;
	public KeyCode On = KeyCode.N;
	public float rate = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(On)){
			player.transform.position += new Vector3(0,rate*Time.deltaTime,0);
		}
	}
}
