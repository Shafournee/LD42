using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            if(collider.GetComponent<Rigidbody2D>().velocity.y > 0)
                collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 300f));
        }
    }
}
