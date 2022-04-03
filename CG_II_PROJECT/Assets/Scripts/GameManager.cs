using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int maxHazardToSpawn = 3;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    [SerializeField]
    private Image pauseMenu;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject hazardPrefab;

    [SerializeField]
    private GameObject mainVCam;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject zoomVCam;

    private int highScore;
    private int score;
    private float timer;
    private bool gameOver;
    private Coroutine hazardsCoroutine;
    private static GameManager instance;
    private const string HighScorePreferenceKey = "High Score";
    public static GameManager Instance => instance;
    public int HighScore => highScore;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt(HighScorePreferenceKey);
    }

    private void OnEnable()
    {
        player.SetActive(true);

                
        mainVCam.SetActive(true);
        zoomVCam.SetActive(false);

        gameOver = false;
        scoreText.text = "0";
        score = 0;
        timer = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazards());
    }

    private void Update() 
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0) 
            {
                Resume();
            }
            if (Time.timeScale == 1) 
            {
                Pause();
            }
        }

        if (gameOver) return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();
            timer = 0;
        }
    }

    private void Pause()
    {
        LeanTween.value(1, 0, 0.5f)
            .setOnUpdate(SetTimeScale)
            .setIgnoreTimeScale(true);
        
        pauseMenu.gameObject.SetActive(true);
    }

    private void Resume()
    {
        LeanTween.value(0, 1, 0.5f)
            .setOnUpdate(SetTimeScale)
            .setIgnoreTimeScale(true);

        pauseMenu.gameObject.SetActive(false);
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f * value;
    }

    private IEnumerator SpawnHazards()
    {
        var hazardToSpawn = Random.Range(1, maxHazardToSpawn);

        for (int i = 0; i < hazardToSpawn; i++) 
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(0f, 2f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        yield return new WaitForSeconds(1f);

        yield return SpawnHazards();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);
        gameOver = true;

        if (Time.timeScale < 1)
        {
            Resume();
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScorePreferenceKey, highScore);
        }

        mainVCam.SetActive(false);
        zoomVCam.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

     public void Enable()
    {
        gameObject.SetActive(true);
    }

}
