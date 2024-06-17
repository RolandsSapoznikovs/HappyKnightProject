using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    public Animator animator;
    public int comboCount = 0;
    public int maxComboCount = 2;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        // Check for input to trigger the combo
        if (Input.GetMouseButtonDown(0))
        {
            // Trigger the next attack in the combo
            ComboAttack();
        }
    }

    public void ComboAttack()
    {
        // Increment combo count
        comboCount++;

        // Trigger the corresponding animation based on the combo count
        animator.SetTrigger("Attack" + comboCount);

        // Reset combo count if it exceeds the number of attacks in your combo
        if (comboCount > maxComboCount)
        {
            comboCount = 0;
        }
    }
}