using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Đổi tên theo đúng scene của bạn
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit(); // Không thấy tác dụng trong Editor, nhưng sẽ thoát khi build
    }
}
