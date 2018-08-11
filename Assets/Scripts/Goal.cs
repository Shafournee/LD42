using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

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
            // When player enters the goal load the next level
            StartCoroutine(EndLevelCutscene(collider.gameObject));

        }
    }

    IEnumerator EndLevelCutscene(GameObject player)
    {
        //Play Animation here
        player.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            yield return null;
        }

        GameManager.instance.LoadNextLevel();
    }
}
