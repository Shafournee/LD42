﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    public bool walkingCoroutineRunning;

    bool grounded;
    SpriteRenderer SpriteRenderer;

    [SerializeField] List<Sprite> sprites;

	// Use this for initialization
	void Start () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Wait());
	}
	
	// Update is called once per frame
	void Update () {

        grounded = GetComponent<Player>().PlayerCanJump();

        if (GetComponent<Player>().direction == dir.right)
        {
            SpriteRenderer.flipX = true;
        }
        else if (GetComponent<Player>().direction == dir.left)
        {
            SpriteRenderer.flipX = false;
        }
        

        if (!grounded)
        {
            StartCoroutine(Jumping());
        }
        else if (GetComponent<Player>().direction == dir.none)
        {
            SpriteRenderer.sprite = sprites[0];
        }
        else if(grounded)
        {
            if(!walkingCoroutineRunning)
            {
                walkingCoroutineRunning = true;
                StartCoroutine(Walking());
            }

        }
        
	}

    IEnumerator Walking()
    {
        while(true)
        {
            for(int i = 1; i < 6; i++)
            {
                if(!grounded || GetComponent<Player>().direction == dir.none)
                {
                    walkingCoroutineRunning = false;
                    yield break;
                }

                SpriteRenderer.sprite = sprites[i];
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    IEnumerator Jumping()
    {
        while(!grounded)
        {
            for (int i = 6; i < 9; i++)
            {
                if (grounded)
                    yield break;
                SpriteRenderer.sprite = sprites[i];
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    IEnumerator Wait()
    {
        yield return null;
        SpriteRenderer.flipX = true;
    }
}
