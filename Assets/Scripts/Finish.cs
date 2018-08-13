using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    [SerializeField] List<Sprite> sunSprites;
    [SerializeField] List<Sprite> explosionSprites;

    // Use this for initialization
    void Start () {
        StartCoroutine(SunAnimation());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Player>() != null)
        {
            GameManager.instance.GetComponent<GameManager>().EndingCredits();
            transform.GetChild(0).position = collider.transform.position;
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ExplosionAnimation());
            collider.gameObject.SetActive(false);
        }
    }

    IEnumerator SunAnimation()
    {
        while(true)
        {
            for (int i = 0; i < sunSprites.Count; i++)
            {
                yield return new WaitForSeconds(.05f);
                GetComponent<SpriteRenderer>().sprite = sunSprites[i];
            }
            
        }
    }

    IEnumerator ExplosionAnimation()
    {
        for (int i = 0; i < explosionSprites.Count; i++)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = explosionSprites[i];
            yield return new WaitForSeconds(.07f);
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
