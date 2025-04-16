using UnityEngine;
using TMPro; // Dùng cho TMP_Text

public class CoinManager : MonoBehaviour
{
    // Singleton
    public static CoinManager Instance { get; private set; }

    [Header("Coin Settings")]
    public int coinCount = 0;
    public int coinTarget = 30; // 👈 Điều kiện thắng là 30 coin

    [Header("UI Reference")]
    public TMP_Text coinText;

    private void Awake()
    {
        // Đảm bảo chỉ có một instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateUI(); // Cập nhật UI khi bắt đầu
    }

    public void AddCoin(int value = 1) // 👈 Giá trị mặc định là 1 coin nếu không truyền vào
    {
        coinCount += value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coin: {coinCount}/{coinTarget}";
        }
    }

    public bool HasEnoughCoins()
    {
        return coinCount >= coinTarget;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
