using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    public GameObject winUI; // Gắn WinText vào đây trong inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CoinManager.Instance.HasEnoughCoins())
        {
            Debug.Log("YOU WIN!");
            winUI.SetActive(true);
            // Optional: Stop player movement
            Time.timeScale = 0;
        }
    }
}
