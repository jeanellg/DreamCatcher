using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    //I want to make this into an abstract class
    public float viewDistance;
    public float defaultSpeed;


    private GameObject enemy;
    private Vector2 direction;

    // Use this for initialization
    void Start () {
        viewDistance = 5.0f;
        defaultSpeed = 3.0f;

        //set default direction to right
        direction = Vector2.right;
        enemy = transform.gameObject;
        
    }

    void FixedUpdate()
    {
        Vector3 centerPoint = enemy.transform.position;
        RaycastHit2D[] detect = Physics2D.RaycastAll(centerPoint, direction, viewDistance);

        for (int i = 0; i < detect.Length; i++)
        {
            string s = detect[i].collider.tag;

            if (s == "player")
            {
                //do something

            }
            else
            {
                //do other things
            }


        }

        Vector2 lookDown = new Vector2(direction.x, -1);
        RaycastHit2D[] fall = Physics2D.RaycastAll(centerPoint, lookDown, 2.0f);
        bool change_direction = true;
        for (int i = 0; i < fall.Length; i++)
        {
            string f = fall[i].collider.tag;
            if (f == "ground")
                change_direction = false;

        }
        if (change_direction)
            //direction = Vector2.left;
            direction = -direction;

        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2((direction.x * defaultSpeed), enemy.GetComponent<Rigidbody2D>().velocity.y);
    }

}
