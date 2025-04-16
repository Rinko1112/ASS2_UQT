using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool playerNearby = false;
    private bool isOpened = false;

    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerNearby && !isOpened && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Open");
            audioSource.Play(); // 🔊 Phát âm thanh mở rương
            CoinManager.Instance.AddCoin(3); // +3 coin
            isOpened = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
