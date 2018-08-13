using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] List<Sprite> shipSprites;

    bool shipIsMoving;

    [SerializeField] GameObject ship;

    // Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;

    AudioSource audioSource;

    // Access to the player
    [SerializeField] GameObject player;

    [SerializeField] GameObject canvas;

    [SerializeField] List<AudioClip> music;

    GameObject levelManager;

    GameObject restartText;

    [SerializeField] List<string> scenes;

    int currentScene = 1;

    private void Awake()
    {

        Application.targetFrameRate = 60;
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this:
        else if (instance != this)
        {
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(StartingCutscene());
        audioSource = GetComponent<AudioSource>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");

    }
	
	// Update is called once per frame
	void Update () {
        MusicPlayer();
        if (player == null)
        {
            if(restartText != null)
                restartText.SetActive(true);

            // Handle restarting level here lmao
            if (Input.GetKey(KeyCode.R))
            {
                RestartLevel();
            }
        }
	}

    // This is the function for restarting the level
    void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
        StartCoroutine(Wait());

    }

    // This gets info from the player to tell the GM the next level was loaded
    // Because I'm an idiot and dont know how to do this correctly
    public void NewLevelLoaded()
    {
        StartCoroutine(Wait());
    }

    // We wait a frame here, because unity sucks and it takes a little while to spawn the player
    // so if we don't wait it's fucking null, and that's really dumb, so here's my stupid coroutine
    // instead that literally just waits one frame, and does nothing else.
    IEnumerator Wait()
    {
        yield return null;
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        restartText = canvas.transform.GetChild(0).gameObject;
        restartText.SetActive(false);
        player.GetComponent<Player>().CheckJumpSpeed(GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().isSpaceLevel);

        //If you're in a space level, set the default gravity scale to .5
        if (levelManager.GetComponent<LevelManager>().isSpaceLevel)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = .5f;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        }

    }

    // This is how you load the next level
    public void LoadNextLevel()
    {
        if(currentScene < scenes.Count)
        {
            currentScene++;
            SceneManager.LoadScene(currentScene);
            StartCoroutine(StartingCutscene());
        }
        else
        {
            print("you fucked up dude");
        }
    }

    // This plays the cutscene for when a level starts for the first time
    IEnumerator StartingCutscene()
    {
        StartCoroutine(Wait());
        yield return null;
        Vector3 playerPos = player.transform.position;
        playerPos = new Vector2(playerPos.x, playerPos.y + .7f);
        ship.transform.position = new Vector2(playerPos.x - 13f, playerPos.y + 2f);
        player.SetActive(false);
        shipIsMoving = true;
        int z = 16;
        while (shipIsMoving)
        {
            // Set ship sprite
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[z];

            z++;

            if(z > 19)
            {
                z = 16;
            }

            // Move ship to starting position
            float step = Time.deltaTime * 9f;
            yield return new WaitForFixedUpdate();
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, playerPos, step);
            // Check if ship is in starting pos, if so break out of loop
            if(ship.transform.position == playerPos)
            {
                shipIsMoving = false;
            }
        }

        // Deploy landing gear
        for(int i = 15; i > -1; i--)
        {
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[i];
            yield return new WaitForSeconds(.1f);
        }
        yield return null;

        player.SetActive(true);
        player.GetComponent<PlayerAnimator>().walkingCoroutineRunning = false;
        player.GetComponent<Player>().CheckJumpSpeed(GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().isSpaceLevel);
    }

    public void CallEndingCutscene(Vector2 goalPos)
    {
        StartCoroutine(EndingCutscene(goalPos));
    }

    public IEnumerator EndingCutscene(Vector2 goalPos)
    {
        // Calculate starting and ending positions for the animations
        goalPos = new Vector2(goalPos.x, goalPos.y);
        ship.transform.position = goalPos;
        player.SetActive(false);
        ship.SetActive(true);

        // Remove landing gear
        for (int i = 0; i < 15; i++)
        {
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[i];
            yield return new WaitForSeconds(.1f);
        }
        yield return null;


        Vector3 finalPos = new Vector3(goalPos.x + 13f, goalPos.y + 2f);

        shipIsMoving = true;
        int z = 16;
        while (shipIsMoving)
        {
            // Set ship sprite
            ship.GetComponent<SpriteRenderer>().sprite = shipSprites[z];

            z++;

            if (z > 19)
            {
                z = 16;
            }

            // Move ship to starting position
            float step = Time.deltaTime * 9f;
            yield return new WaitForFixedUpdate();
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, finalPos, step);
            // Check if ship is in starting pos, if so break out of loop
            if (ship.transform.position == finalPos)
            {
                shipIsMoving = false;
            }
        }
        LoadNextLevel();
    }

    public void EndingCredits()
    {
        StartCoroutine(PlayEndingCredits());
    }

    IEnumerator PlayEndingCredits()
    {

        GameObject credits = canvas.transform.GetChild(1).gameObject;
        yield return new WaitForSeconds(4f);
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        shipIsMoving = true;
        while (shipIsMoving)
        {
            float step = Time.deltaTime * 150f;
            yield return new WaitForFixedUpdate();
            credits.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(credits.GetComponent<RectTransform>().anchoredPosition, new Vector3(0f, 0f, 0f), step);
            if (credits.GetComponent<RectTransform>().anchoredPosition == new Vector2(0f, 0f))
            {
                shipIsMoving = false;
            }
        }

        shipIsMoving = true;
        while (shipIsMoving)
        {
            float step = Time.deltaTime * 150f;
            yield return new WaitForFixedUpdate();
            credits.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(credits.GetComponent<RectTransform>().anchoredPosition, new Vector3(0f, 2385f, 0f), step);
            if (credits.GetComponent<RectTransform>().anchoredPosition == new Vector2(0f, 2385f))
            {
                shipIsMoving = false;
            }
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TitleScreen");
    }

    void MusicPlayer()
    {
        if(!audioSource.isPlaying)
        {
            if (!levelManager.GetComponent<LevelManager>().isSpaceLevel)
            {
                audioSource.clip = music[Random.Range(0, 5)];
            }
            else
            {
                audioSource.clip = music[Random.Range(6, 10)];
            }
            audioSource.Play();
        }
    }
}
