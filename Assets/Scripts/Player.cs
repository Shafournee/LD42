using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dir { left, right, none };

public class Player : MonoBehaviour {

    Rigidbody2D rigidBody;

    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode space = KeyCode.Space;

    KeyCode p = KeyCode.P;

    public dir direction;

    float speed = 15f;
    float airSpeed = 5f;
    float trueSpeed;
    float speedCap = 10f;

    public bool playerCanMove;

    public bool playerIsJumping;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        // Get access to the rigidbody
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerCanMove)
        {
            Movement();
            Jump();
        }


	}

    private void FixedUpdate()
    {
        if(PlayerCanJump())
        {
            trueSpeed = speed;
        }
        else
        {
            trueSpeed = airSpeed;
        }
        // Adds speed to the player
        if ((direction == dir.left && rigidBody.velocity.x > 0 || direction == dir.right && rigidBody.velocity.x < 0) && PlayerCanJump())
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }
        else if (direction == dir.left)
        {
            rigidBody.AddForce(new Vector2(-trueSpeed, 0f));
        }
        else if (direction == dir.right)
        {
            rigidBody.AddForce(new Vector2(trueSpeed, 0f));
        }
        // If you aren't moving, or you're going from one direction to another, stop completely first
        else if (direction == dir.none)
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }

        // Cap the speed of the player
        if(rigidBody.velocity.x > speedCap)
        {
            rigidBody.velocity = new Vector2(speedCap, rigidBody.velocity.y);
        }
        else if (rigidBody.velocity.x < -speedCap)
        {
            rigidBody.velocity = new Vector2(-speedCap, rigidBody.velocity.y);
        }
    }

    void Movement()
    {
        if(Input.GetKey(left))
        {
            direction = dir.left;
        }
        else if (Input.GetKey(right))
        {
            direction = dir.right;
        }
        else
        {
            direction = dir.none;
        }
    }

    void Jump()
    {
        // If player can jump, and on space down, add the force
        if(Input.GetKey(space) && PlayerCanJump())
        {
            if(!playerIsJumping)
                StartCoroutine(Jumping());
        }
    }

    IEnumerator Jumping()
    {
        playerIsJumping = true;
        yield return new WaitForFixedUpdate();
        rigidBody.AddForce(new Vector2(0f, 400f));
        yield return new WaitForFixedUpdate();
        playerIsJumping = false;
    }

    public bool PlayerCanJump()
    {
        Vector2 origin = new Vector2(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f);
        return Physics2D.Raycast(origin, Vector2.down, .01f);
    }


}
