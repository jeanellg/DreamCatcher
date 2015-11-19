using UnityEngine;
using System.Collections;

public class Invisible : MonoBehaviour {

    public SpriteRenderer sr;
    public bool visible;

    void Start () {

    }

    void Update() {
        if (visible == true)
        {
            sr.material.color = new Color(sr.material.color.b, sr.material.color.g, sr.material.color.r, 225);

        }
        if (visible == false)
        {
            sr.material.color = new Color(sr.material.color.b, sr.material.color.g, sr.material.color.r, 0);
        }
	}
}
