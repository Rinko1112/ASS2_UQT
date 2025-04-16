using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public Animator coinAnimator;
    public GameObject collectEffect;

    private bool isCollected = false;
    private AudioSource audioSource; // Thêm biến âm thanh

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Lấy AudioSource
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return; // Tránh thu thập nhiều lần
        if (other.CompareTag("Player"))
        {
            isCollected = true;

            // Gọi CoinManager
            CoinManager.Instance.AddCoin(coinValue);

            // Gọi animation
            if (coinAnimator != null)
            {
                coinAnimator.SetTrigger("Collect");
            }

            // Phát âm thanh
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Hiệu ứng particle (nếu có)
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            // Xoá object sau thời gian delay để âm thanh/animation kịp chạy
            Destroy(gameObject, 0.5f); // Giữ 0.5s để nghe âm thanh
        }
    }
}
