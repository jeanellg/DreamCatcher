using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    //I want to make this into an abstract class
    public const float viewDistance = 5.0f;
    public const float defaultSpeed = 3.0f;
    public const float alertSpeed = 6.0f;


    private GameObject enemy;
    private Vector2 direction;
    private string current_state;
    private bool facing_object;
    private float current_speed;

    // Use this for initialization
    void Start () {

        //set default direction to right
        direction = Vector2.right;
        enemy = transform.gameObject;
        //set default state to patrol
        current_state = "patrol";
        facing_object = false;
        current_speed = defaultSpeed;
    }

    void stateUpdate(Vector3 centerPoint)
    {
        //if an enemy detects a player, it goes on alert state
        if (detectPlayer(centerPoint))
            current_state = "alert";
        else
        {
            //timer until the enemy goes back to the patrol state
        }
    }

    bool playerWithinRange(Vector3 centerPoint)
    {
        RaycastHit2D[] detect = Physics2D.RaycastAll(centerPoint, direction, 0.5f);
        for (int i = 0; i < detect.Length; i++)
        {
            string s = detect[i].collider.tag;

            if (s == "Player")
            {
                return true;
            }

        }
        return false;
    }

    bool detectPlayer(Vector3 centerPoint)
    {
        


        RaycastHit2D[] detect = Physics2D.RaycastAll(centerPoint, direction, viewDistance);
        //bool temp_facing_object = false;
        for (int i = 0; i < detect.Length; i++)
        {
            string s = detect[i].collider.tag;

            if (s == "Player")
            {
                return true;
            }
            else if (s== "Wall")
            {
                facing_object = true;
            }
        }
        //if player is detected, enemy will continue to follow the player even if it sees a wall
        //facing_object = temp_facing_object;
        return false;

    }

    void updatePatrol(Vector3 centerPoint)
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
        if (change_direction || facing_object)
            //direction = Vector2.left;
            direction = -direction;
        current_speed = defaultSpeed;
    }

    void updateAlert(Vector3 centerPoint)
    {

        Vector3 playerPoint = GameObject.Find("Player").transform.position;
        Vector3 new_vector = playerPoint - centerPoint;
        direction = new Vector2 (Mathf.Abs(new_vector.x) / new_vector.x,0);
        if (playerWithinRange(centerPoint))
            current_state = "stop";
        else
            current_speed = alertSpeed;
    }

    void FixedUpdate()
    {
        Vector3 centerPoint = enemy.transform.position;
        stateUpdate(centerPoint);
        if (current_state == "patrol")
        {
            updatePatrol(centerPoint);
        }
        else if (current_state == "alert")
        {
            updateAlert(centerPoint);
            

        }
        else if (current_state == "stop")
        {
            current_speed = 0;
        }

        else
        {
            print("State: " + current_state + " is an unknown state.");
            //do something
        }
    
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2((direction.x * current_speed), enemy.GetComponent<Rigidbody2D>().velocity.y);
    }

}
