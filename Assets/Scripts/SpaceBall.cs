using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Change gravity scale based on if the player enters or leaves a space ball

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            collider.GetComponent<Rigidbody2D>().gravityScale = .5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            collider.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
