using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private float OriginalSpeed;
    public Transform target;
    public float stoppingDistance;
    public float attackCooldown; // Time between attacks
    private float lastAttackTime; // Time of the last attack
    public Transform EnemyAttack;
    public LayerMask targetLayer;
    public float radius;
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;
    public bool PlayerDetected;
    public Transform ViewRange;
    public GameObject PointA;
    public GameObject PointB;
    public Animator animator;
    private Transform currentPoint;
    private Rigidbody2D rb;
    public float patrolspeed;
    public float EnemyAttackRadius;
    public int EnemyAttackDamage;
    public int Enemyknockback;
    public EnemyPhysics Physics;
    public bool Attacking;
    public float waitTimeAtPatrolPoints = 2f; // Time to wait at patrol points
    private bool isPatrolling = false;

    // Variable to track the previous direction
    private Vector2 previousDirection;

    void Start()
    {
        if (PointA == null || PointB == null)
        {
            Debug.LogError("PointA and/or PointB is not assigned in the Inspector.");
            return;
        }

        currentPoint = PointB.transform;

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is not found on the GameObject.");
            return;
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator is not found on the GameObject.");
            return;
        }

        OriginalSpeed = speed;
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (Physics.died == true)
        {
            this.enabled = false;
        }

        var collider = Physics2D.OverlapCircle(ViewRange.position, radius, targetLayer);
        PlayerDetected = collider != null;

        if (PlayerDetected)
        {
            StopCoroutine(Patrol()); // Stop patrolling when the player is detected
            HandlePlayerDetection();
        }
        else if (!Attacking)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }

    private void HandlePlayerDetection()
    {
        float distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f));

        if (distanceToTarget > stoppingDistance)
        {
            // Calculate the direction to the target
            Vector2 direction = new Vector2(target.position.x - transform.position.x, 0f).normalized;

            // Check if the direction has changed
            if (direction != previousDirection)
            {
                // Flip the sprite's scale along the x-axis
                FlipSprite(direction.x > 0f);
                previousDirection = direction;
            }

            // Move towards the target
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else
        {
            // Stop moving when the buffered destination is reached
            rb.velocity = Vector2.zero;
            previousDirection = Vector2.zero;

            // Check if enough time has passed since the last attack
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                // Perform the attack
                Attack();
                // Update the last attack time
                lastAttackTime = Time.time;
            }
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            if (!PlayerDetected && !Attacking)
            {
                Vector2 point = currentPoint.position - transform.position;
                animator.SetFloat("Speed", Mathf.Abs(patrolspeed));

                rb.velocity = new Vector2((currentPoint == PointB.transform ? patrolspeed : -patrolspeed), 0);

                if (Vector2.Distance(transform.position, currentPoint.position) < 1f)
                {
                    rb.velocity = Vector2.zero; // Stop moving
                    animator.SetFloat("Speed", 0);

                    // Wait for the specified time
                    yield return new WaitForSeconds(waitTimeAtPatrolPoints);

                    // Flip direction and move to the other point
                    if (currentPoint == PointB.transform)
                    {
                        currentPoint = PointA.transform;
                    }
                    else
                    {
                        currentPoint = PointB.transform;
                    }

                    // Flip sprite after changing points
                    FlipSprite(currentPoint == PointB.transform);
                }
            }

            yield return null;
        }
    }

    /* public void ResumePatrol()
    {
        if (!isPatrolling)
        {
            isPatrolling = true;
            StartCoroutine(Patrol());
        }
    } */

    // Function to flip the sprite's scale
    private void FlipSprite(bool isFacingRight)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = isFacingRight ? Mathf.Abs(newScale.x) : -Mathf.Abs(newScale.x);
        transform.localScale = newScale;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(ViewRange.position, radius);
            Gizmos.DrawSphere(EnemyAttack.position, EnemyAttackRadius);
        }
    }

    // Function to perform the attack
    private void Attack()
    {
        Attacking = true;
        speed = 0;
        animator.SetFloat("Speed", 0);
        animator.SetTrigger("Attacking");
    }

    private void DoAttackDamage()
    {
        Collider2D[] PlayerInRange = Physics2D.OverlapCircleAll(EnemyAttack.position, EnemyAttackRadius, targetLayer);
        Vector2 EnemyKnockbackDirection;

        foreach (Collider2D Player in PlayerInRange)
        {
            if (transform.localScale.x > 0)
            {
                EnemyKnockbackDirection = Vector2.right;
            }
            else
            {
                EnemyKnockbackDirection = Vector2.left;
            }

            Debug.Log("Player hit");
            Player.GetComponent<Zero_>().TakeDamage(EnemyAttackDamage, Enemyknockback, EnemyKnockbackDirection);
        }

        Attacking = false; // End attacking state
        speed = OriginalSpeed;
        StartCoroutine(Patrol()); // Resume patrolling after attacking
    }
}