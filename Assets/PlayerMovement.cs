using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Zero_ combat;


    public float runSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    public int JumpCount = 0;
    public bool isGrounded;
    public bool CanDoubleJump;

    void start()
    {
        JumpCount = controller.m_JumpCount;
        isGrounded = controller.m_Grounded;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controller.m_Grounded;

        if (combat.Attack == true || combat.Blocking == true || combat.SpecialAttack == true)
        {
            runSpeed = 0f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        }
        else if (combat.Attack == false || combat.Blocking == false || combat.SpecialAttack == false)
        {
            runSpeed = 40f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                JumpCount++;

                if(isGrounded)
                {
                    Debug.Log("Player Grounded");
                    JumpCount = 0;
                    CanDoubleJump = true;
                }
                else if(JumpCount >= 1 && CanDoubleJump)
                {
                    animator.SetTrigger("IsSecondJump");
                    JumpCount = 0;
                    CanDoubleJump = false;
                }
            }
        }
        
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
