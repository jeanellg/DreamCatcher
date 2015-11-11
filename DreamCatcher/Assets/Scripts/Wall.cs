using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public bool isDestructable;
	public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestructable){
			if (health <= 0){
				Destroy(gameObject);
			}
		}
	}

	void takeDamage(int amount){
		health -= amount;
	}
}
