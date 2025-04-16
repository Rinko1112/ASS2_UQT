using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject winText;
    public GameObject backToMenuButton;
    public AudioSource winSound; // Âm thanh khi win

    private bool hasWon = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        if (other.CompareTag("Player") && CoinManager.Instance.HasEnoughCoins())
        {
            hasWon = true;
            winText.SetActive(true);
            backToMenuButton.SetActive(true);
            winSound.Play(); // Phát âm thanh
            Time.timeScale = 0f;
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Reset time
        SceneManager.LoadScene("Main Menu"); // Đảm bảo có scene tên "MainMenu"
    }
}
