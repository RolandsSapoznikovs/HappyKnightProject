using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public int VineDamage;
    private int VineKnockback = 0;

    private float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 VineKnockbackDirection = Vector2.left;
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Zero_>().TakeDamage(VineDamage, VineKnockback, VineKnockbackDirection);
        }
    }
}
