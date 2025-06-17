using UnityEngine;
using UnityEngine.UI;
using TMPro; // Bu sat�r �ok �nemli!



public class PlayerCollector : MonoBehaviour
{
        [SerializeField] private TextMeshProUGUI scoreText;

         private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            score++;

            // UI g�ncelle
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }

            // Nesneyi yok et
            Destroy(other.gameObject);
        }
    }
}
