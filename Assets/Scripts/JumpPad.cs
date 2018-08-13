using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    [SerializeField] float force;

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
            collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collider.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force));
        }
    }
}
