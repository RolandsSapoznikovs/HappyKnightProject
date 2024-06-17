using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // You can add the coin value to the player's total coins here
            CoinController player = other.GetComponent<CoinController>();
            if (player != null)
            {
                player.AddCoins(coinValue);
            }
            Destroy(gameObject);
        }
    }
}
