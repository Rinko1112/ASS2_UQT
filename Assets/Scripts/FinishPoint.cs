using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public GameObject winPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CoinManager.Instance.HasEnoughCoins())
        {
            winPanel.SetActive(true); // Show UI Win
            Time.timeScale = 0f; // Dừng game
        }
    }
}
