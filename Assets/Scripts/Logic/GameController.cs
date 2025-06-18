using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private float gameDuration = 5f;

    [SerializeField] private PelletSpawner _pelletSpawner;
    [SerializeField] private PelletCollector _pelletCollector;

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
            EndGame();
        }
    }

    public void StartGame()
    {

        Debug.Log("StartGame called, spawning pellets...");
        Time.timeScale = 1f;
        _gameEnded = false;     
        _timer = gameDuration;       
        _gameOverScreen.SetActive(false);     
        _pelletSpawner.SpawnPellets(); 
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
