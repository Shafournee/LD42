using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    [SerializeField] GameObject titleImage;

	// Use this for initialization
	void Start () {
		if(GameManager.instance.gameObject != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        // Load the first level
        SceneManager.LoadScene("Level1");
    }

    public void Credits()
    {
        titleImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(960f, -540f);
    }

    public void BackToTitle()
    {
        titleImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(960f, 540f);
    }

    public void LevelSelect()
    {
        titleImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960f, 540f);
    }
}
