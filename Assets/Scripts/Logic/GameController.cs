using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float gameDuration = 30f;

    [SerializeField] private PelletSpawner _pelletSpawner;
    [SerializeField] private PelletCollector _pelletCollector;
    private BombSpawner _bombSpawner;

    private float _timer;
    private bool _gameEnded = false;


    private void Awake()
    {
        _pelletSpawner = GetComponent<PelletSpawner>();
        if (_pelletSpawner == null)
        {
        
        }
    }
    private void Start()
    {
        StartGame();
    }
    private void Update()
    {
        if (_gameEnded) return;

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = 0f;
            EndGame();
        }
        int seconds = Mathf.FloorToInt(_timer % 60);
        int minutes = Mathf.FloorToInt(_timer / 60);
        _timerText.text = $"Time:{minutes:00}:{seconds:00}";
    }

    public void StartGame()
    {

        
        Time.timeScale = 1f;
        _gameEnded = false;     
        _timer = gameDuration;       
        _gameOverScreen.SetActive(false);
        _pelletSpawner.SpawnPellets();
        _bombSpawner.SpawnOneBomb(); 
        _bombSpawner.SpawnBombsInitial();
    }


  



public void EndGame()
    {
        _gameEnded = true;
        _gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
       
    }

    public void OnPlayAgain()
    {
        
        foreach (var pellet in GameObject.FindGameObjectsWithTag("Collectible"))
        {
            Destroy(pellet);
        }
        foreach (var bomb in GameObject.FindGameObjectsWithTag("Bomb"))
        {
            Destroy(bomb);
        }
        StartGame(); 
        
    }
}
