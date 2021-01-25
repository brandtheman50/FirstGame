using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    private PlayerMovement playerScript;

    float lookRadius = 5f;
    Transform target;
    NavMeshAgent agent;

    float currentHealth;
    float maxHealth = 10f;

    public Animator anim;
    
   

    public float attackRate = 2f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    float nextAttackTime = 0f;
    public LayerMask playerLayer;

    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        playerScript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Death();
            Destroy(gameObject);
        }
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {

            }
        }
        else
        {
            agent.SetDestination(transform.position);
        }
        if (Time.time >= nextAttackTime)
        {

            if (distance <= 1f)
            {
                anim.SetTrigger("Attack");
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

                foreach (Collider enemy in hitEnemies)
                {
                    enemy.GetComponent<PlayerMovement>().TakeDamage(5);

                }
                anim.SetTrigger("Return");
                nextAttackTime = Time.time + 5f / attackRate;
                
            }
        }
    }

    void Attack()
    {
        
        anim.SetTrigger("Attack");
        if (Time.time >= nextAttackTime)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<PlayerMovement>().TakeDamage(5);

            }
        }

    }
    

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        if (attackPoint == null)
            return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        

    }
    void Death()
    {
        anim.SetTrigger("Die");
        
        

    }
}
