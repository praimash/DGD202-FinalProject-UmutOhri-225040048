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
            Debug.LogError("PelletSpawner component not found on GameController!");
        }
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

        Debug.Log("StartGame called, spawning pellets...");
        Time.timeScale = 1f;
        _gameEnded = false;     
        _timer = gameDuration;       
        _gameOverScreen.SetActive(false);
       // _pelletCollector.ResetCounter();
        _pelletSpawner.SpawnPellets();
        _bombSpawner.SpawnOneBomb(); 
         _bombSpawner.SpawnBombsInitial();
    }


    private void ClearOldPellets()
    {
        GameObject[] pellets = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject pellet in pellets)
        {
            Destroy(pellet);
        }
    }




public void EndGame()
    {
        _gameEnded = true;
        _gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Oyun bitti!");
    }

    // 🎮 Play Again butonundan çağrılır
    public void OnPlayAgain()
    {
        // Sahnede topları temizle
        foreach (var pellet in GameObject.FindGameObjectsWithTag("Collectible"))
        {
            Destroy(pellet);
        }
        Debug.Log("onplayu");    
        StartGame(); // ✅ Restart game & spawn new pellets
        
    }
}
