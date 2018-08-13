using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : MonoBehaviour {

    public bool specialMeteor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            Destroy(collider.gameObject);

        }
        if(specialMeteor && collider.gameObject.layer != 2 )
        {
            Destroy(gameObject);
        }
    }
}
