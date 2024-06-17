using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    public Animator animator;
    public int EnemyHealth;
    private int EnemyCurrentHealth;
    private Rigidbody2D rb;
    public FlashScript flashscript;
    public bool died;
    public GameObject coinPrefab;
    public int numberOfCoinsToDrop = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyCurrentHealth = EnemyHealth;
        died = false;
    }

    public void TakeDamage(int damage, int knockback, Vector2 knockbackDirection)
    {
        if (died)
            return;

        EnemyCurrentHealth -= damage;

        if (flashscript != null)
        {
            flashscript.Flash();
        }
        
        Vector2 knockbackForce = knockbackDirection.normalized * knockback;
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);

        Debug.Log($"Enemy took {damage} damage. Current health: {EnemyCurrentHealth}. Knockback force: {knockbackForce}");

        if (EnemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void SpawnCoin()
    {
        for (int i = 0; i < numberOfCoinsToDrop; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        if (died)
            return;

        died = true;
        Debug.Log("Enemy died");

        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
            animator.SetBool("Dead", true);
        }
        else
        {
            Debug.LogWarning("Animator component is null in EnemyPhysics script.");
        }

        // Disable collider and script
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    /* public void ResetEnemy()
    {
        EnemyCurrentHealth = EnemyHealth;
        died = false;

        if (animator != null)
        {
            animator.SetBool("Dead", false);
        }

        GetComponent<Collider2D>().enabled = true;
        this.enabled = true;
    } */

    // Animation event function to be called when the death animation finishes
    /* public void OnDeathAnimationEnd()
    {
        GetComponent<EnemyRespawner>().MarkAsDead();
    } */

    void OnDrawGizmosSelected()
    {
        if (rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rb.position, 0.5f); // Example gizmo for debugging
        }
    }
}