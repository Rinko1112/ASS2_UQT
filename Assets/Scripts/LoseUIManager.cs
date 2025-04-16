using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUIManager : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load lại level hiện tại
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Đảm bảo có scene tên "MainMenu"
    }
}
