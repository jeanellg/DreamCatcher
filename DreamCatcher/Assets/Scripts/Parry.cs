using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour {
	public Player player;
	public KeyCode on = KeyCode.I;
	public float iTime =1f;
	public float iTimer;
	public bool canParry = true;
	public float cooldown = 4f;
	public float cdTimer;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (canParry && Input.GetKeyDown(on)){
			player.setInvinceable(true);
			canParry = false;
			cdTimer = cooldown;
			iTimer = iTime;
		}
		else if (!canParry){
			cdTimer -= Time.deltaTime;
		}
		if ( cdTimer <= 0 ){
			cdTimer = 0;
			canParry = true;
		}
		if( player.getInvinceable()){
			iTimer -= Time.deltaTime;
		}
		if(iTimer <=0){
			iTimer = 0;
			player.setInvinceable(false);
		}
	}
}
