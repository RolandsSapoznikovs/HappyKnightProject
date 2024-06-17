/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isDead = false;
    private EnemyPhysics enemyPhysics;
    private EnemyMovement enemymovement;
    private RangedEnemyMovement rangedenemymovement;
    private Collider2D enemyCollider;

    void Start()
    {
        // Store the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        enemymovement = GetComponent<EnemyMovement>();
        rangedenemymovement = GetComponent<RangedEnemyMovement>();
        enemyPhysics = GetComponent<EnemyPhysics>();
        enemyCollider = GetComponent<Collider2D>();
    }

    public void Respawn()
    {
        // Reset position, rotation, and state
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(true);
        isDead = false;

        // Enable the EnemyPhysics script
        if (enemyPhysics != null)
        {
            enemymovement.enabled = true;
            rangedenemymovement.enabled = true;
            enemyPhysics.enabled = true;
            enemymovement.ResumePatrol();
            rangedenemymovement.ResumePatrol();
            enemyPhysics.ResetEnemy();
        }

        // Enable the collider
        if (enemyCollider != null)
        {
            enemyCollider.enabled = true;
        }
    }

    public void MarkAsDead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return isDead;
    }
} */