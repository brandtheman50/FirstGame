using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;

    public Health healthBar;
    public int maxHealth = 100;
    public int currentHealth;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

   

    float turnSmoothVelocity;
    int attack = 0;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
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
        if(direction.magnitude >= 0.1f && !(Input.GetButton("Fire2")) && !(Input.GetMouseButton(0)))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
            
        }
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("Defend", true);
        }
        else
        {
            anim.SetBool("Defend", false);
        }
        
       
    }

    void Defend()
    {
        
    }

    
    

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
