using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Attack")]
    public Transform attackPointHolder;
    public Transform attackPoint;
    public float attackRange = 3.5f;
    public LayerMask enemyLayers;

    [Header("Health")]
    public int maxHP = 15;
    private int currentHP;
    private bool isHurting = false;
    private bool isDead = false;

    [Header("References")]
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip deathSound;
    public AudioClip runSound;
    public AudioClip hurtSound;
    public AudioClip loseSound;

    [Header("UI")]
    public GameObject losePanel;

    private bool isRunningSoundPlaying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;

        if (losePanel != null)
            losePanel.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Flip nhân vật và attack point
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (attackPointHolder != null) attackPointHolder.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (attackPointHolder != null) attackPointHolder.localScale = new Vector3(-1, 1, 1);
        }

        // Chạy âm thanh khi di chuyển
        if (movement.sqrMagnitude > 0.1f)
        {
            if (!isRunningSoundPlaying && runSound != null)
            {
                audioSource.clip = runSound;
                audioSource.loop = true;
                audioSource.Play();
                isRunningSoundPlaying = true;
            }
        }
        else
        {
            if (isRunningSoundPlaying)
            {
                audioSource.Stop();
                isRunningSoundPlaying = false;
            }
        }

        // Tấn công
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        if (isDead) return;

        animator.SetTrigger("Attack");

        if (audioSource != null && attackSound != null)
            audioSource.PlayOneShot(attackSound);

        if (attackPoint != null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>()?.TakeDamage();
            }
        }
    }

    public void TakeDamage()
    {
        if (isDead || isHurting) return;

        currentHP--;
        Debug.Log("Player took damage! Current HP: " + currentHP);
        StartCoroutine(Hurt());

        if (currentHP <= 0)
        {
            Die();
        }
    }

    IEnumerator Hurt()
    {
        isHurting = true;
        animator.Play("Player_Hurt");

        if (audioSource != null && hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        yield return new WaitForSeconds(0.4f);
        isHurting = false;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.Play("Player_Death");

        if (audioSource != null && deathSound != null)
            audioSource.PlayOneShot(deathSound);

        rb.linearVelocity = Vector2.zero;
        this.enabled = false;

        StartCoroutine(ShowLosePanelAfterDeath());
    }

    IEnumerator ShowLosePanelAfterDeath()
    {
        yield return new WaitForSeconds(1.2f); // Chờ animation chết kết thúc

        if (audioSource != null && loseSound != null)
            audioSource.PlayOneShot(loseSound);

        if (losePanel != null)
            losePanel.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Gọi từ UI Button
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Đặt đúng tên Scene Menu của bạn
    }
}
