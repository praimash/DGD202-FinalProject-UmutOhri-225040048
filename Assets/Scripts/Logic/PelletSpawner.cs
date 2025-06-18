using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PelletSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject _pelletPrefab;

    [Range(1,20)]
    [field: SerializeField] public int NumberToSpawn;
    
    [SerializeField] private Vector2 _arenaSize;
    private Vector2 _arenaExtents;
    
    private Vector2[] _pelletPositions;

    private float _detectionRadius = 1f;

    private bool _spawningEnabled = false;
    [SerializeField] private float spawnInterval = 2f; // 2 saniyede bir yeni top

    private void Start()
    {
        _arenaExtents = _arenaSize * 0.5f;

        SpawnPellets(); // Baþlangýç toplarý
        _spawningEnabled = true;
        StartCoroutine(SpawnPelletsPeriodically()); // Oyun boyunca sürekli yeni top spawnla
    }
    private System.Collections.IEnumerator SpawnPelletsPeriodically()
    {
        while (_spawningEnabled)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPelletAtRandomPosition(); // 2 saniyede bir yeni top
        }
    }
    private void SpawnPelletAtRandomPosition()
    {
        int attempts = 0;
        while (attempts < 100)
        {
            attempts++;
            float xPos = Random.Range(-_arenaExtents.x, _arenaExtents.x);
            float zPos = Random.Range(-_arenaExtents.y, _arenaExtents.y);
            Vector2 newPos = new Vector2(xPos, zPos);

            if (NearAnotherPellet(newPos)) continue;

            // Yeni pozisyonu kaydetme
            Array.Resize(ref _pelletPositions, _pelletPositions.Length + 1);
            _pelletPositions[_pelletPositions.Length - 1] = newPos;

            SpawnPellet(newPos);
            Debug.Log($"Yeni top spawnlandý: {newPos}");
            return;
        }

        Debug.LogWarning("Uygun pozisyon bulunamadý!");
    }

    
    public void SpawnPellets()
    {
        Debug.Log("SpawnPellets started");
        _pelletPositions = new Vector2[NumberToSpawn];

        for (int i = 0; i < NumberToSpawn; i++)
        {
            Debug.Log($"Trying to spawn pellet {i}");
            int attempts = 0;
            while (_pelletPositions[i] == Vector2.zero)
            {
                attempts++;
                if (attempts > 100)
                {
                    Debug.LogWarning("Could not find valid spawn position!");
                    break;
                }

                float xPos = Random.Range(-_arenaExtents.x, _arenaExtents.x);
                float zPos = Random.Range(-_arenaExtents.y, _arenaExtents.y);

                Vector2 pelletPosition = new Vector2(xPos, zPos);

                if (NearAnotherPellet(pelletPosition)) continue;

                _pelletPositions[i] = pelletPosition;

                SpawnPellet(pelletPosition);
                Debug.Log($"Spawned pellet at {pelletPosition}");
            }
        }
        Debug.Log($"Spawned {NumberToSpawn} pellets");
    }


    public int GetPelletCount()
{
    
    return GameObject.FindGameObjectsWithTag("Pellet").Length;
}
    private void SpawnPellet(Vector2 position)
    {
        Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
        GameObject pellet = Instantiate(_pelletPrefab, worldPosition, Quaternion.identity);
    }
    
    private bool NearAnotherPellet(Vector2 pelletPosition)
    {
        for (int i = 0; i < _pelletPositions.Length; i++)
        {
            if ((pelletPosition - _pelletPositions[i]).magnitude < _detectionRadius) return true;
        }
        
        return false;
    }
}
