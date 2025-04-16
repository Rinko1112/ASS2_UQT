using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public float detectRange = 5f;
    public float moveSpeed = 2f;
    public int maxHealth = 3;

    [Header("Attack")]
    public float attackCooldown = 1f;
    public float attackRange = 1.2f;
    private bool isAttacking = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip deathSound;

    private int currentHealth;
    private Transform player;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectRange)
        {
            if (distance > attackRange)
            {
                animator.Play("Enemy_Run");
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                if (!isAttacking)
                    StartCoroutine(Attack());
            }

            // Xoay hướng theo player
            Vector3 scale = transform.localScale;
            scale.x = (player.position.x > transform.position.x) ? 1 : -1;
            transform.localScale = scale;
        }
        else
        {
            animator.Play("Enemy_Idle");
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        // Làm chậm animation để phù hợp âm thanh
        animator.speed = 0.7f;
        animator.Play("Enemy_Attack");

        // Phát âm thanh tấn công
        if (audioSource != null && attackSound != null)
            audioSource.PlayOneShot(attackSound);

        yield return new WaitForSeconds(0.4f); // Delay trước khi gây sát thương

        if (player != null && !isDead)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                PlayerController pc = player.GetComponent<PlayerController>();
                pc?.TakeDamage();
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        animator.speed = 1f;
        isAttacking = false;
    }

    public void TakeDamage()
    {
        if (isDead) return;

        currentHealth--;
        animator.Play("Enemy_Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.Play("Enemy_Death");

        // Phát âm thanh chết
        if (audioSource != null && deathSound != null)
            audioSource.PlayOneShot(deathSound);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Destroy(gameObject, 1f);
    }
}
