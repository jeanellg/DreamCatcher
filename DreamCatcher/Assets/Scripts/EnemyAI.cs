using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    //I want to make this into an abstract class
    public const float view_distance = 5.0f;
    public const float patrol_speed = 3.0f;
    public const float alert_speed = 6.0f;

    private string[] states = new string[3] {"idle","patrol","alert"};
    private string current_state;
    private float current_speed;
    private Vector2 direction;
    private GameObject enemy;
    

    // Use this for initialization
    void Start () {

        
        //set default direction to right
        direction = Vector2.right;
        enemy = transform.gameObject;
        current_state = states[1];
    }

    void updateState ()
    {

    }

    void updateSpeed ()
    {
        if (current_state == states[0])
            current_speed = 0;
        else if (current_state == states[1])
            current_speed = patrol_speed;
        else if (current_state == states[2])
            current_speed = alert_speed;
        else
            print("State: " + current_state + " is undefined");
    }

    void updateDirection (Vector3 centerPoint)
    {
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
    }

    void forwardDetection (Vector3 centerPoint)
    {
        RaycastHit2D[] detect = Physics2D.RaycastAll(centerPoint, direction, view_distance);

        for (int i = 0; i < detect.Length; i++)
        {
            string s = detect[i].collider.tag;

            if (s == "player")
            {
                //ignore regular patrol pattern

            }
            else if (s == "wall")
            {
                //do other things
            }


        }
    }

    void FixedUpdate()
    {
        Vector3 centerPoint = enemy.transform.position;
        updateState();
        updateSpeed();
        


        updateDirection(centerPoint);
        forwardDetection(centerPoint);


        

        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2((direction.x * current_speed), enemy.GetComponent<Rigidbody2D>().velocity.y);
    }

}
