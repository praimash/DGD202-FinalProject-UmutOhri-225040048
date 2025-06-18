using UnityEngine;

public class Pellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Pellet çarpışması algılandı"); 
            PelletCollector.Instance.PelletCollected();
            Destroy(gameObject);
        }
    }
}
