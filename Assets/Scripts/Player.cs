using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dir { left, right, none };

public class Player : MonoBehaviour {

    Rigidbody2D rigidBody;

    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode space = KeyCode.Space;
    KeyCode down = KeyCode.S;

    KeyCode p = KeyCode.P;

    public dir direction;

    //Player speed on ground
    float speed = 20f;
    //Player speed in air
    float airSpeed = 10f;
    //Variable for trackin speed, whether on ground or in air
    float trueSpeed;
    //Variable for capping player speed
    float speedCap = 10f;
    //The jump speed of the player
    float jumpSpeed;
    //Modifiers for jump speed
    float spaceJumpSpeed = 400f;
    float earthJumpSpeed = 550f;

    public bool playerCanMove;

    public bool playerIsJumping;

    bool isInSpace;

    bool fastFall;

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

        //Check if the player moves from space to earth too quickly, if so modify the gravity appropriately
        if(jumpSpeed == spaceJumpSpeed && !isInSpace || jumpSpeed == earthJumpSpeed && isInSpace)
        {
            StartCoroutine(ModifyGravity());
        }

        if(Input.GetKey(down))
        {
            fastFall = true;
        }
        else
        {
            fastFall = false;
        }

        if(Input.GetKeyDown(p))
        {
            print(jumpSpeed);
            print(rigidBody.gravityScale);
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

        if(fastFall)
        {
            rigidBody.AddForce(new Vector2(0f, -20f));
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
        if(Input.GetKeyDown(space) && PlayerCanJump())
        {
            if(!playerIsJumping)
                StartCoroutine(Jumping());
        }
    }

    public void CheckJumpSpeed(bool inSpace)
    {
        isInSpace = inSpace;
        StartCoroutine(ChangeJumpSpeed(inSpace));
    }

    IEnumerator ChangeJumpSpeed(bool inSpace)
    {
        yield return new WaitForSeconds(.1f);
        if (inSpace)
        {
            // Space Jump Speed
            jumpSpeed = spaceJumpSpeed;
        }
        else
        {
            // Earth Jump Speed
            jumpSpeed = earthJumpSpeed;
        }
    }

    // If the player jumps but immediately exist the gravity they were in before, give them a modified gravity
    // For a bit, so that it doesn't feel like they get no height when going from space to earth, or too much going from
    // Earth to space
    IEnumerator ModifyGravity()
    {
        if (jumpSpeed == spaceJumpSpeed && !isInSpace)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.7f;
        }
        else if (jumpSpeed == earthJumpSpeed && isInSpace)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.4f;
        }
        for (int i = 0; i < 60; i++)
        {
            yield return null;
            if (PlayerCanJump())
            {
                if(isInSpace)
                {
                    GetComponent<Rigidbody2D>().gravityScale = .5f;
                }
                else
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1.8f;
                }
                yield break;
            }

        }
        if (isInSpace)
        {
            GetComponent<Rigidbody2D>().gravityScale = .5f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        }
    }

    IEnumerator Jumping()
    {
        playerIsJumping = true;
        yield return new WaitForFixedUpdate();
        rigidBody.AddForce(new Vector2(0f, jumpSpeed));
        yield return new WaitForFixedUpdate();
        playerIsJumping = false;
    }

    public bool PlayerCanJump()
    {
        // Raycast from left and right of player collision box, so that players can stand on edges and still are able to jump
        bool left;
        bool right;
        Vector2 rayLeft = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x - .01f, GetComponent<BoxCollider2D>().bounds.min.y - .01f);
        left =  Physics2D.Raycast(rayLeft, Vector2.down, .1f);
        Vector2 rayRight = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x + .01f, GetComponent<BoxCollider2D>().bounds.min.y - .01f);
        right = Physics2D.Raycast(rayRight, Vector2.down, .1f);
        if(left || right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
