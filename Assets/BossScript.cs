using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public EnemyPhysics Physics;
    public Animator animator;
    private Rigidbody2D rb;
    public Color gizmo1Color = Color.green;
    public Color gizmo2Color = Color.yellow;
    public Color gizmo3Color = Color.red;
    public bool showGizmos = true;
    public Vector2 RangedSize = new Vector2(4, 4);
    public Vector2 AttackSize = new Vector2(2, 2);
    public Vector2 VineSize = new Vector2(5, 1);
    public Transform RangedRange;
    public Transform AttackRange;
    public Transform VineRange;
    public LayerMask targetLayer;
    public int EnemyAttackDamage;
    public int Enemyknockback;
    public float attackCooldown; // Time between attacks
    public GameObject bullet;
    public Transform bulletPos;
    public Transform target;
    public GameObject Vine;
    public Transform VinePos;
    public float VineSpawnDelay = 0.5f;

    private float timer;
    private float lastAttackTime; // Time of the last attack

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Physics.died)
        {
            this.enabled = false;
            return;
        }

        timer += Time.deltaTime;

        if (timer > 3)
        {
            timer = 0;
            CheckForPlayers();
        }
    }

    private void CheckForPlayers()
    {
        float angle = transform.rotation.eulerAngles.z;

        var Rangedcollider = Physics2D.OverlapBox(RangedRange.position, RangedSize, angle, targetLayer);
        var AttackCollider = Physics2D.OverlapBox(AttackRange.position, AttackSize, angle, targetLayer);
        var VineCollider = Physics2D.OverlapBox(VineRange.position, VineSize, angle, targetLayer);

        bool RangedPlayerDetected = Rangedcollider != null;
        bool AttackPlayerDetected = AttackCollider != null;
        bool VinePlayerDetected = VineCollider != null;

        if (VinePlayerDetected && Time.time - lastAttackTime >= attackCooldown)
        {
            VineAttack();
            lastAttackTime = Time.time;
        }
        else if (AttackPlayerDetected)
        {
            Attack();
        }
        else if (RangedPlayerDetected)
        {
            RangedAttack();
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmo1Color;
            Gizmos.DrawWireCube(RangedRange.position, RangedSize);
            Gizmos.color = gizmo2Color;
            Gizmos.DrawWireCube(AttackRange.position, AttackSize);
            Gizmos.color = gizmo3Color;
            Gizmos.DrawWireCube(VineRange.position, VineSize);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");       
    }

    private void DoAttackDamage()
    {
        float angle = transform.rotation.eulerAngles.z;
        Collider2D[] PlayerInAttackRange = Physics2D.OverlapBoxAll(AttackRange.position, AttackSize, angle, targetLayer);
        Vector2 EnemyKnockbackDirection;

        foreach (Collider2D Player in PlayerInAttackRange)
        {
            if (transform.localScale.x > 0)
            {
                EnemyKnockbackDirection = Vector2.right;
            }
            else
            {
                EnemyKnockbackDirection = Vector2.left;
            }
            Debug.Log("Player hit with Attack");
            Player.GetComponent<Zero_>().TakeDamage(EnemyAttackDamage, Enemyknockback, EnemyKnockbackDirection);
        }
    }

    private void RangedAttack()
    {
        animator.SetTrigger("Ranged");
        // Bullet will be spawned via animation event
    }

    private void VineAttack()
    {
        animator.SetTrigger("Vine");
    }

    private IEnumerator SpawnVinesWithDelay()
    {
        Vector3 currentPosition = VinePos.position;

        for (int i = 0; i < 9; i++) 
        {
            // Instantiate the Vine at the current position
            Instantiate(Vine, currentPosition, Quaternion.identity);

            // Increment the position for the next Vine
            currentPosition += new Vector3(-4, 0, 0);

            yield return new WaitForSeconds(VineSpawnDelay);
        }
    }

    private void StartSpawningVines()
    {
        StartCoroutine(SpawnVinesWithDelay());
    }

    // Function called by animation event to spawn the bullet
    private void SpawnBullet()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}