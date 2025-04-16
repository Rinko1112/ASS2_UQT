using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public string nextSceneName = "Level2"; // 👈 Đặt tên scene tiếp theo tại đây

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager coinManager = CoinManager.Instance;

            if (coinManager != null && coinManager.GetCoinCount() >= 30)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("Chưa đủ coin để sang màn!");
            }
        }
    }
}
