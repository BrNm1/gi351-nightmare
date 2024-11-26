using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Light2D light;
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float patrolRange = 10f;
    public float attackDamage = 20f;
    public LayerMask obstacleMask;
    
    private Vector2 patrolTarget;
    private bool isPlayerDetected = false;
    private bool isAttacking = false;

    private void Start()
    {
        SetRandomPatrolTarget();
    }

    private void Update()
    {
        bool isLightOn = light != null && light.enabled;

        if (isLightOn)
        {
            MoveAwayFromPlayer();
        }
        else
        {
            if (isPlayerDetected)
            {
                MoveTowardsPlayer();
            }
            else
            {
                Patrol();
            }
        }

        DetectPlayer();
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        if (!IsObstacleInDirection(direction))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            SetRandomPatrolTarget();
        }
    }

    private void MoveAwayFromPlayer()
    {
        Vector2 direction = (transform.position - player.position).normalized;
        if (!IsObstacleInDirection(direction))
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, moveSpeed * Time.deltaTime);
        }
        else
        {
            SetRandomPatrolTarget();
        }
    }

    private void Patrol()
    {
        Vector2 direction = (patrolTarget - (Vector2)transform.position).normalized;

        if (!IsObstacleInDirection(direction))
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolTarget, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolTarget) < 0.2)
            {
                SetRandomPatrolTarget();
            }
        }
        
        else
        {
            SetRandomPatrolTarget();
        }
    }

    private void SetRandomPatrolTarget()
    {
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomY = Random.Range(-patrolRange, patrolRange);
        patrolTarget = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }

    private bool IsObstacleInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, obstacleMask);
        
        Debug.DrawRay(transform.position, direction, Color.red);
        
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isAttacking)
        {
            StartCoroutine(AttackPlayer(other.gameObject));
        }
    }
    
    private IEnumerator AttackPlayer(GameObject player)
    {
        isAttacking = true;

        for (int i = 0; i < 3; i++)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.TakeDamage(attackDamage);

            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, patrolRange);
    }
}
