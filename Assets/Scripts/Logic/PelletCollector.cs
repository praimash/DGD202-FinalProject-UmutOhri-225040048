using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PelletCollector : MonoBehaviour
{
    
    public static PelletCollector Instance;
    private PelletSpawner _pelletSpawner;
    private GameController _gameController;
    private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _counter;
    
    
    private int _numberToCollect;
    private int _numberCollected;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;            
        }

        _gameController = GetComponent<GameController>();
        _pelletSpawner = GetComponent<PelletSpawner>();
        _audioSource = GetComponent<AudioSource>();
    }

    
   
    public void ResetCounter()
    {
        _numberCollected = 0;
        _counter.text = "0";
    }
  
    public void PelletCollected()
    {
        Debug.Log($"Toplanan: {_numberCollected} / Hedef: {_numberToCollect}");
        _audioSource.PlayOneShot(_audioSource.clip);
        _numberCollected++;
        _counter.text = _numberCollected.ToString();
          
        
       // if (_numberCollected >= _numberToCollect)
       // {
          //  Debug.Log("T�m toplar topland�, EndGame() �a�r�l�yor.");
        //    _gameController.EndGame();
       // }
    }
}
