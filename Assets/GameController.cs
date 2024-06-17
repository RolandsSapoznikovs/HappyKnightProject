/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Zero_ Player; // Reference to the player
    private Vector3 playerInitialPosition;
    public List<EnemyRespawner> enemies = new List<EnemyRespawner>();
    
     void Start()
    {
        // Store the initial position of the player
        playerInitialPosition = Player.transform.position;

        // Find all enemies with the EnemyRespawner component
        EnemyRespawner[] enemyRespawners = FindObjectsOfType<EnemyRespawner>();
        foreach (EnemyRespawner enemy in enemyRespawners)
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        // Reset the player's position and health
        Player.Respawn();

        // Respawn all enemies
        foreach (EnemyRespawner enemy in enemies)
        {
            if (enemy.IsDead())
            {
                enemy.Respawn();
            }
        }
    }
}
 */