using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int maxHazardToSpawn = 3;

    [SerializeField]
    private int maxCoinToSpawn = 1;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    [SerializeField]
    private Image pauseMenu;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject hazardPrefab;

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private GameObject mainVCam;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject zoomVCam;

    private int highScore;
    private int score;
    private bool gameOver;

    private Coroutine hazardsCoroutine;
    private Coroutine coinsCoroutine;

    private const string HighScorePreferenceKey = "High Score";
    public int HighScore => highScore;

    private static GameManager instance;
    public static GameManager Instance => instance;

    public AudioClip clipNextLevel;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt(HighScorePreferenceKey);
    }

    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();

        player.SetActive(true);
                
        mainVCam.SetActive(true);
        zoomVCam.SetActive(false);

        gameOver = false;
        scoreText.text = "0";
        score = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazards());
        coinsCoroutine = StartCoroutine(SpawnCoins());
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
    }

    public void SetScore()
    {
        score++;
        scoreText.text = score.ToString();

        if (score > 2)
        {
            AudioSource.PlayClipAtPoint(clipNextLevel, Camera.main.transform.position);
            Pause(); // TODO: Implements plataform appears logic
        }
    }

    private void Pause()
    {
        GetComponent<AudioSource>().Pause();

        LeanTween.value(1, 0, 0.5f)
            .setOnUpdate(SetTimeScale)
            .setIgnoreTimeScale(true);
        
        pauseMenu.gameObject.SetActive(true);
    }

    private void Resume()
    {
        GetComponent<AudioSource>().Play();

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

    private IEnumerator SpawnCoins()
    {

        var coinToSpawn = Random.Range(1, maxCoinToSpawn);

        for (int i = 0; i < coinToSpawn; i++)
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(0f, 2f);
            var coin = Instantiate(coinPrefab, new Vector3(x, 11, 0), Quaternion.identity);
            coin.GetComponent<Rigidbody>().drag = drag;
        }

        yield return new WaitForSeconds(3f);

        yield return SpawnCoins();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);
        StopCoroutine(coinsCoroutine);
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
