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
		RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up*.13f * moveY/Mathf.Abs(moveY), Vector2.up * moveY/Mathf.Abs(moveY), .02f);
		//Debug.DrawRay(transform.position + Vector3.up * moveY/Mathf.Abs(moveY)*.13f, Vector2.up * moveY/Mathf.Abs(moveY) * .2f,  Color.red);
		
		if (count == movePeriod){count = 0; moveX = -moveX; moveY= -moveY;}
		if(!hit){
			if ( !isLevered) {
				this.transform.position += new Vector3(moveX/movePeriod, moveY/movePeriod, 0);
				count += 1;
			}
			if (isLevered && lever.getState() == moveOnState){
				this.transform.Translate(new Vector3(moveX/movePeriod, moveY/movePeriod, 0));
				count += 1;
			}
		}
		else{count+=1;}
	}
}
