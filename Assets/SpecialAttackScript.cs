using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackScript : MonoBehaviour
{
    public float ProjectileSpeed;
    public int ProjectileDamage;
    public float lifetime = 5f;
    public int SpecialKnockBack;

    private Rigidbody2D rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Determine the direction the player is facing
            Vector3 direction;
            if (player.transform.localScale.x < 0) // Player is facing left
            {
                direction = -player.transform.right;

                // Flip the projectile sprite vertically when facing left
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
            else // Player is facing right
            {
                direction = player.transform.right;
            }

            // Set the projectile's velocity
            rb.velocity = direction * ProjectileSpeed;

            // Rotate the projectile to face the direction it is moving
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }

        // Destroy the projectile after a certain time to prevent it from existing indefinitely
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 SpecialKnockBackDirection = Vector2.left;
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Assuming the enemy script has a method called TakeDamage
            other.GetComponent<EnemyPhysics>().TakeDamage(ProjectileDamage, SpecialKnockBack, SpecialKnockBackDirection);
        }
    }
}