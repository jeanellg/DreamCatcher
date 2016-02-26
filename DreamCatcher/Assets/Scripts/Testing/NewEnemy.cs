using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class NewEnemy : MonoBehaviour {

    float jumpHeight = 4;
    float timeToJumpApex = .4f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float moveSpeed = 6;

    bool jumped;
    Vector2 velocity;
    float gravity;

    float velocityXSmoothing;

    Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    }

    void updatePatrol(Vector3 centerPoint)
    {
        Vector2 lookDown = new Vector2(Mathf.Sign(moveSpeed), -1);
        RaycastHit2D[] fall = Physics2D.RaycastAll(centerPoint, lookDown, 2.0f);
        Debug.DrawRay(centerPoint, lookDown * 2.0f, Color.red);
        bool change_direction = true;
        for (int i = 0; i < fall.Length; i++)
        {
            string f = fall[i].collider.tag;
            if (f == "ground")
                change_direction = false;


        }
        if (change_direction || controller.collisions.left || controller.collisions.right)
            //direction = Vector2.left;
            moveSpeed = -moveSpeed;
        
    }

    // Update is called once per frame
    void Update () {
        Vector3 centerPoint = transform.position;
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            jumped = false;
        }

        

        updatePatrol(centerPoint);

        float targetVelocityX = moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
