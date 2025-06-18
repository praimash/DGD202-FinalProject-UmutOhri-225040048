using UnityEngine;
using UnityEngine.UI;
using TMPro; // Bu satýr çok önemli!



public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            score++;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bomb"))
        {
            score -= 3;  // 3 puan eksilt
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}