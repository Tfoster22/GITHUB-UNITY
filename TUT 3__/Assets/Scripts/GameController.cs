using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public int hazardCount;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text ScoreText;
    public int score;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    float currentTime = 0f;
    float startingTime = 15f;
   


    [SerializeField] Text countdownText;

    private bool gameOver;
    private bool restart;
    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (spawnwaves());
        currentTime = startingTime;


    }


    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
            Debug.Log("WHY ME");
            musicSource.clip = musicClipTwo;
            musicSource.Play();

        }
    
}

    IEnumerator spawnwaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
;                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'B' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
        if (score >= 100)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
    }
    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 100)
        {
           
            winText.text = "You win! Game created by Taylor Foster!";
            gameOver = true;
            restart = true;
        }

    }

    public void GameOver()
    {
        gameOverText.text = "Game Over! Game created by Taylor Foster!";
        gameOver = true;
        musicSource.clip = musicClipTwo;
        musicSource.Play();
        
    }

       
}
