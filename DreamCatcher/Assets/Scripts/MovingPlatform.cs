using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {


	public Lever lever;
	public int moveX;
	public int moveY;
	public float movePeriod;
	public int moveOnState;
	private bool isLevered;
	private int count;

    
	// Use this for initialization
	void Start () {
		if (lever == null){
			isLevered = false;
		}
		else{
			isLevered = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if (count == movePeriod){count = 0; moveX = -moveX; moveY= -moveY;}
		if ( !isLevered) {
			this.transform.position += new Vector3(moveX/movePeriod, 0, 0);
			count += 1;
		}
		if (isLevered && lever.getState() == moveOnState){
			this.transform.Translate(new Vector3(moveX/movePeriod, moveY/movePeriod, 0));
			count += 1;
		}
	}
}
