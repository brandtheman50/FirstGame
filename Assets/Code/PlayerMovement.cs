using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    float nextAttackTime = 0f;
    public float attackRate = 2f;
    float turnSmoothVelocity;
    int attack = 0;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("Move", true);
        }

        else
        {
            anim.SetBool("Move", false);
        }
        if(direction.magnitude >= 0.1f && !(Input.GetButton("Fire2")))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
            
        }
        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Defend", true);
        }
        else
        {
            anim.SetBool("Defend", false);
        }
        
        if (Time.time >= nextAttackTime && !Input.GetButton("Fire2"))
        {

            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Defend()
    {
        
    }
    void Attack()
    {
        
        anim.SetTrigger("Attack");
        
    }
}
