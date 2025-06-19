using UnityEngine;

public class Pellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            
            PelletCollector.Instance.PelletCollected();
            Destroy(gameObject);
        }
    }
}
