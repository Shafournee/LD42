using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBall : MonoBehaviour {

    GameObject levelManager;
    GameObject player;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Change gravity scale based on if the player enters or leaves a space ball

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            if(levelManager.GetComponent<LevelManager>().isSpaceLevel)
            {
                collider.GetComponent<Player>().CheckJumpSpeed(false);
                collider.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
            }
            else
            {
                collider.GetComponent<Player>().CheckJumpSpeed(true);
                collider.GetComponent<Rigidbody2D>().gravityScale = .5f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            if (levelManager.GetComponent<LevelManager>().isSpaceLevel)
            {
                collider.GetComponent<Player>().CheckJumpSpeed(true);
                collider.GetComponent<Rigidbody2D>().gravityScale = .8f;
            }
            else
            {
                collider.GetComponent<Player>().CheckJumpSpeed(false);
                collider.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
            }
        }
    }
}
