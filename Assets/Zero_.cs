using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zero_ : MonoBehaviour
{
    private Vector3 initialPosition;
    public Animator animator;
    private Rigidbody2D rb;
    public int combo;
    public bool Attack = false;
    public LayerMask enemyLayers;
    public LayerMask FountainLayer;
    public float FountainRange;
    public Transform FountainCollider;
    public float attackRange;
    public Transform AttackCollider;
    public int AttackDamage = 10;
    public int PlayerHealth = 100;
    public int PlayerMana;
    public int PlayerCurrentHealth;
    public int PlayerCurrentMana;
    public bool PlayerDied;
    /* public GameController Controller; */
    public FlashScript flashscript;
    public HealthBar healthBar;
    public ManaBar manabar;
    public bool Blocking;
    public bool SpecialAttack;
    public GameObject Projectile;
    public Transform ProjectilePos;
    public GameObject HammerProjectile;
    public Transform HammerProjectilePos;
    public GameObject SpearProjectile;
    public Transform SpearProjectilePos;
    public bool SwordShield = true;
    public bool Hammer;
    public bool Spear;
    public float specialAttackCooldown = 5f; // Cooldown duration in seconds
    private bool canUseSpecial = true; // Tracks if the special attack can be used
    public float dashDistance = 3f; // Distance to dash
    public float dashSpeed = 20f;
    private bool isDashing = false;
    public float dashCooldown = 2f;
    private bool isDashOnCooldown = false;
    public bool PlayerDead = false;
    public int WeaponKnockback = 20;
    public bool UseFountain;



    void Start()
    {
        SwordShield = true;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        PlayerDied = false;
        Attack = false;
        animator = GetComponent<Animator>();
        PlayerCurrentHealth = PlayerHealth;
        PlayerCurrentMana = PlayerMana;
        healthBar.SetMaxHealth(PlayerHealth);
        manabar.SetMaxMana(PlayerMana);
    }
   
    void Update()
    {
        Combos_();
        PlayerBlock();
        PlayerSpecial();
        WeaponSwitch();
        PlayerHealing();
        healthBar.SetHealth(PlayerCurrentHealth);
        manabar.SetMana(PlayerCurrentMana);
    }

    public void WeaponSwitch()
    {
        if(Input.GetKeyDown("1") && !Attack && !Blocking && !SpecialAttack)
        {
            SwordShield = true;
            Spear = false;
            Hammer = false;
            WeaponKnockback = 0;    
            AttackDamage = 10;
        }
        if(Input.GetKeyDown("2") && !Attack && !Blocking && !SpecialAttack)
        {
            Hammer = true;
            SwordShield = false;
            Spear = false;
            WeaponKnockback = 0;
            AttackDamage = 20;
        }
        if(Input.GetKeyDown("3") && !Attack && !Blocking && !SpecialAttack)
        {
            Spear = true;
            Hammer = false;
            SwordShield = false;
            WeaponKnockback = 0;
            AttackDamage = 5;
        }
        animator.SetBool("SwordShield", SwordShield);
        animator.SetBool("Hammer", Hammer);
        animator.SetBool("Spear", Spear);
    }

    public void PlayerHealing()
    {
        if(Input.GetKeyDown("e"))
        {
            Collider2D[] FountainInRange = Physics2D.OverlapCircleAll(FountainCollider.position, FountainRange, FountainLayer);

            foreach(Collider2D Fountain in FountainInRange)
            {
                UseFountain = true;
                PlayerCurrentHealth = 100;
                Debug.Log("Fountain used");
            }
        }
        else
        {
            UseFountain = false;
        }   
    }

    public void Start_Combo()
    {
        Attack = false;
        if(combo<3)
        {
            combo++;
        }
    }

    public void Finish_Ani()
    {
        Attack = false;
        combo = 0;
        
    }

    public void Combos_()
    {
        if(Input.GetKeyDown("k") && !Attack && !Blocking)
        {
            Attack = true;
            animator.SetTrigger(""+combo);
        }
    }

    public void PlayerDoDamage()
    {
        Collider2D[] EnemiesInRange = Physics2D.OverlapCircleAll(AttackCollider.position, attackRange, enemyLayers);
        Vector2 WeaponKnockbackDirection;

            foreach(Collider2D enemy in EnemiesInRange)
            {
                if (transform.localScale.x > 0)
                {
                    WeaponKnockbackDirection = Vector2.right;
                }
                else
                {
                    WeaponKnockbackDirection = Vector2.left;
                }

                enemy.GetComponent<EnemyPhysics>().TakeDamage(AttackDamage, WeaponKnockback, WeaponKnockbackDirection);
                if(PlayerCurrentMana == 100)
                {
                    PlayerCurrentMana += 0;
                }
                else
                {
                    PlayerCurrentMana += 3;
                }
                
            }
    }

    public void PlayerBlock()
    {
        // Check if the "j" key is pressed and the player is not attacking
        if (Input.GetKey("j") && !Attack && !SpecialAttack && SwordShield)
        {
            Blocking = true;
        }
        else if (Input.GetKey("j") && !Attack && !SpecialAttack && Spear && !isDashing && !isDashOnCooldown)
        {
            Blocking = false;
            StartCoroutine(DashBackward());
        }
        else if (Input.GetKey("j") && !Attack && !SpecialAttack && Hammer && !isDashing && !isDashOnCooldown)
        {
            Blocking = false;
            StartCoroutine(DashForward());
        }
        else
        {
            Blocking = false;
        }

        // Update the animator with the blocking state
        animator.SetBool("Blocking", Blocking);
    }

    private IEnumerator DashForward()
    {
        isDashing = true;

        animator.SetTrigger("Dash");

        // Calculate the dash direction (backwards based on facing direction)
        float facingDirection = transform.localScale.x > 0 ? 1 : -1;
        Vector2 dashDirection = new Vector2(facingDirection, 0) * dashDistance;

        // Store the starting position
        Vector2 startPosition = rb.position;

        // Calculate the target position
        Vector2 targetPosition = startPosition + dashDirection;

        // Perform the dash over time
        float elapsedTime = 0f;
        while (elapsedTime < dashDistance / dashSpeed)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, (elapsedTime * dashSpeed) / dashDistance));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set correctly
        rb.MovePosition(targetPosition);

        isDashing = false;

        // Start the cooldown coroutine
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashBackward()
    {
        isDashing = true;

        animator.SetTrigger("Dash");

        // Calculate the dash direction (backwards based on facing direction)
        float facingDirection = transform.localScale.x > 0 ? -1 : 1;
        Vector2 dashDirection = new Vector2(facingDirection, 0) * dashDistance;

        // Store the starting position
        Vector2 startPosition = rb.position;

        // Calculate the target position
        Vector2 targetPosition = startPosition + dashDirection;

        // Perform the dash over time
        float elapsedTime = 0f;
        while (elapsedTime < dashDistance / dashSpeed)
        {
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, (elapsedTime * dashSpeed) / dashDistance));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set correctly
        rb.MovePosition(targetPosition);

        isDashing = false;

        // Start the cooldown coroutine
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        isDashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashOnCooldown = false;
    }


    public void PlayerSpecial()
    {
        if (Input.GetKeyDown(KeyCode.L) && canUseSpecial && !Attack && !SpecialAttack && !Blocking && PlayerCurrentMana > 0)
        {
            SpecialAttack = true;
            PlayerCurrentMana -= 20;
            animator.SetTrigger("Special");
            canUseSpecial = false;

            StartCoroutine(SpecialAttackRoutine());
            StartCoroutine(SpecialAttackCooldownRoutine());
        }
    }

    private IEnumerator SpecialAttackRoutine()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SpecialAttack = false;
    }

    private IEnumerator SpecialAttackCooldownRoutine()
    {
        yield return new WaitForSeconds(specialAttackCooldown);
        canUseSpecial = true;
    }

    private void SpecialProjectileSword()
    {
        Instantiate(Projectile, ProjectilePos.position, Quaternion.identity);
    }

    private void SpecialProjectileHammer()
    {
        Instantiate(HammerProjectile, HammerProjectilePos.position, Quaternion.identity);
    }

    private void SpecialProjectileSpear()
    {
        Instantiate(SpearProjectile, SpearProjectilePos.position, Quaternion.identity);
    }



    void OnDrawGizmosSelected()
    {
        if(AttackCollider == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackCollider.position, attackRange);
        Gizmos.DrawWireSphere(FountainCollider.position, FountainRange);
    }

    public void TakeDamage(int damage, int knockback, Vector2 knockbackDirection)
    {
        if (Blocking)
        {
            PlayerCurrentHealth -= damage / 4;
            flashscript.Flash();
            healthBar.SetHealth(PlayerCurrentHealth);
        }
        else
        {
            PlayerCurrentHealth -= damage;
            flashscript.Flash();
            healthBar.SetHealth(PlayerCurrentHealth);

            // Apply knockback
            Vector2 knockbackForce = knockbackDirection.normalized * knockback;
            rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        }

        if (PlayerCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Died");
        /* Controller.Respawn(); */
        Respawn();
    }

    public void Respawn()
    {
        if (SceneController.instance != null)
        {
            SceneController.instance.HandlePlayerDeath();
            Debug.Log("Player died, coins updated.");
            // Implement additional death handling logic here (e.g., restarting the level)
        }
        else
        {
            Debug.LogWarning("SceneController instance is null in PlayerController");
        }
        PlayerCurrentHealth = PlayerHealth;
        PlayerCurrentMana = PlayerMana;
        transform.position = initialPosition;
        healthBar.SetHealth(PlayerCurrentHealth);
        manabar.SetMana(PlayerCurrentMana);

        gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Respawn();
        }

        if(collision.CompareTag("Spikes"))
        {
            PlayerCurrentHealth -= 10;
            if(PlayerCurrentHealth <= 0)
            {
                Die();
            }
        }
    }


}
