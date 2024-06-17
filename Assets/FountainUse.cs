using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainUse : MonoBehaviour
{

    public Zero_ combat;
    public bool FountainUsed;
    private Collider2D myCollider;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        FountainUsed = combat.UseFountain;
        myCollider = GetComponent<Collider2D>();
        animator.SetBool("Empty", false);
    }

    // Update is called once per frame
    void Update()
    {
        FountainUsed = combat.UseFountain;

        if(FountainUsed)
        {
            myCollider.enabled = !myCollider.enabled;
            animator.SetBool("Empty", true);
        }
    }
}
