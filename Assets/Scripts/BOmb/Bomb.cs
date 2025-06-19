using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // ⛔ BU, bombayı yok eder
            PelletCollector.Instance.BombHit();
        }

    


    }
}