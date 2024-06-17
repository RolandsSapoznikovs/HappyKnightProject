using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float ProjectileSpeed;
    public int ProjectileDamage;
    public int ProjectileKnockback = 0;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * ProjectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 ProjectileKnockbackDirection = Vector2.left;
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Zero_>().TakeDamage(ProjectileDamage, ProjectileKnockback, ProjectileKnockbackDirection);
            Destroy(gameObject);
        }
    }
}
