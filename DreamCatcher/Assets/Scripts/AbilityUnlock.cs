using UnityEngine;
using System.Collections;

public class AbilityUnlock : MonoBehaviour {

public string ability;
private int ablint;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		switch (ability)
		{
			case "blink":{ablint = 0;break;}
			case "move":{ablint = 1;break;}
			case "jump":{ablint = 2;break;}
			case "portal":{ablint = 3;break;}
			case "float":{ablint = 4;break;}
			case "fill":{ablint = 5;break;}
		}
	}
	public int getAblint(){return ablint;}
	public void destroy(){Destroy(gameObject);}
}
