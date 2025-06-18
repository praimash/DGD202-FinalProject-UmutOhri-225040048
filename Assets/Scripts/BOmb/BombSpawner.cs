using System;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Vector2 _arenaSize = new Vector2(20, 20);

    [SerializeField] private int initialBombCount = 3;      // Oyun baþýnda spawnlanacak bomba sayýsý
    [SerializeField] private float bombSpawnInterval = 1f;  // Her kaç saniyede bir bomba spawnlanacak
    [SerializeField] private float _bombDetectionRadius = 1f; // Bombalarýn birbirine çok yakýn spawn olmamasý için mesafe

    private Vector2 _arenaExtents;
    private Vector2[] _bombPositions;

    private void Start()
    {
        _arenaExtents = _arenaSize * 0.5f;

        // Baþlangýçta belirlenen sayýda bomba spawnla
        SpawnBombsInitial();

        // Belirli aralýklarla sürekli bomba spawnlamayý baþlat
        InvokeRepeating(nameof(SpawnOneBomb), bombSpawnInterval, bombSpawnInterval);
    }

    public  void SpawnBombsInitial()
    {
        _bombPositions = new Vector2[initialBombCount];

        for (int i = 0; i < initialBombCount; i++)
        {
            int attempts = 0;
            while (_bombPositions[i] == Vector2.zero)
            {
                attempts++;
                if (attempts > 100)
                {
                    Debug.LogWarning("Valid bomba spawn pozisyonu bulunamadý!");
                    break;
                }

                float xPos = UnityEngine.Random.Range(-_arenaExtents.x, _arenaExtents.x);
                float zPos = UnityEngine.Random.Range(-_arenaExtents.y, _arenaExtents.y);

                Vector2 bombPosition = new Vector2(xPos, zPos);

                if (NearAnotherBomb(bombPosition)) continue;

                _bombPositions[i] = bombPosition;

                SpawnBomb(bombPosition);
            }
        }
    }

    public void SpawnOneBomb()
    {
        Vector2 bombPosition = Vector2.zero;
        int attempts = 0;

        while (bombPosition == Vector2.zero)
        {
            attempts++;
            if (attempts > 100)
            {
                Debug.LogWarning("Valid bomba spawn pozisyonu bulunamadý!");
                return;
            }

            float xPos = UnityEngine.Random.Range(-_arenaExtents.x, _arenaExtents.x);
            float zPos = UnityEngine.Random.Range(-_arenaExtents.y, _arenaExtents.y);

            Vector2 possiblePosition = new Vector2(xPos, zPos);

            if (NearAnotherBomb(possiblePosition)) continue;

            bombPosition = possiblePosition;
        }

        SpawnBomb(bombPosition);
    }

    public void SpawnBomb(Vector2 position)
    {
        Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
        Instantiate(_bombPrefab, worldPosition, Quaternion.identity);
    }

    private bool NearAnotherBomb(Vector2 position)
    {
        if (_bombPositions == null) return false;

        foreach (var existingPos in _bombPositions)
        {
            if ((existingPos - position).magnitude < _bombDetectionRadius)
                return true;
        }

        return false;
    }
}
