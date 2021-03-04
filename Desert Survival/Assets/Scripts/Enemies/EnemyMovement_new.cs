using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement_new : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public Transform player;
    public LayerMask groundLayer, playerLayer;

    public Vector3 walkPoint;
    private bool nextWalkPointSet;
    public float walkPointRange;
    public bool idleActive;

    public float walkingSpeed;
    public float runningSpeed;

    public float timeBetweenAttack;
    private bool alreadyAttacked;
    public Transform attackPoint;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPlayerRange();

        if (!playerInSightRange && !playerInAttackRange) { Patrol(); }
        if (playerInSightRange && !playerInAttackRange) { FollowPlayer(); }
        if (playerInSightRange && playerInAttackRange) { AttackPlayer(); }
    }

    private void CheckPlayerRange()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    }

    private void Patrol()
    {
        if (idleActive) { return; }

        animator.SetBool("Move", true);

        agent.speed = walkingSpeed;

        if (!nextWalkPointSet)
        {
            bool idle = (Random.Range(0, 2) == 0);

            if (idle)
            {
                agent.SetDestination(transform.position);
                StartCoroutine(Idle());
            }
            else
            {
                FindNextWalkPoint();
            }
        }
        else
        {
            agent.SetDestination(walkPoint);
            //animator.SetTrigger("Walk");
            /*
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);
            */
            animator.SetFloat("Blend", 0.5f);
        }

        Vector3 walkDistance = transform.position - walkPoint;

        if (walkDistance.magnitude < 1f)
        {
            nextWalkPointSet = false;
        }
    }

    public Vector3 refPoint;

    private void FindNextWalkPoint()
    {
        float z = Random.Range(-walkPointRange, walkPointRange);
        float x = Random.Range(-walkPointRange, walkPointRange);

        refPoint = new Vector3(transform.position.x + x, 5f, transform.position.z + z);

        RaycastHit groundHit;

        if (Physics.Raycast(refPoint, -Vector3.up, out groundHit))
        {
            if (groundHit.transform.CompareTag("Ground"))
            {
                Debug.Log("Hit ground");
                walkPoint = groundHit.point;
                nextWalkPointSet = true;
            }
        }
        else
        {
            Debug.Log("Hit nothing");
        }
        /*
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            nextWalkPointSet = true;
        }
        */
    }

    private IEnumerator Idle()
    {
        idleActive = true;
        animator.SetFloat("Blend", 0);
        yield return new WaitForSeconds(7f);
        idleActive = false;
    }

    private void FollowPlayer()
    {
        idleActive = false;
        animator.SetBool("Move", true);
        agent.speed = runningSpeed;
        //animator.SetTrigger("Run");
        animator.SetFloat("Blend", 0.9f);
        nextWalkPointSet = false;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        idleActive = false;
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetBool("Move", false);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            alreadyAttacked = true;
            bool playerHit = Physics.CheckSphere(attackPoint.position, attackRange, playerLayer);

            if (playerHit)
            {
                Invoke(nameof(HitPlayer), 0.6f);
            }

            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void HitPlayer()
    {
        float damage = GetComponent<Enemy>().damage;
        player.gameObject.GetComponent<PlayerStats>().ChangeValues(0, 0, -damage);
    }


    private void OnDrawGizmosSelected()
    {
        //sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        //attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //walk point position
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(walkPoint, 0.5f);
        //attack point position
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, 0.8f);
        Gizmos.DrawWireSphere(refPoint, 1);
    }
}
