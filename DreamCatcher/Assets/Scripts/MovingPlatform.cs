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
	public LayerMask mask;
	public float RayLength = .2f;

    
	// Use this for initialization
	void Start () {
		mask = ~(1 << LayerMask.NameToLayer("Platform") | 1 << LayerMask.NameToLayer ("Player"));
		if (lever == null){
			isLevered = false;
		}
		else{
			isLevered = true;
		}
	}
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up * moveY/Mathf.Abs(moveY), RayLength, mask);
		Debug.DrawRay(transform.position, Vector2.up * moveY/Mathf.Abs(moveY) *RayLength,  Color.red);
		
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
	}
}
