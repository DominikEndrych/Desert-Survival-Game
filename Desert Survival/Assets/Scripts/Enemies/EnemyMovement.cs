using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
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

        if(!playerInSightRange && !playerInAttackRange) { Patrol(); }
        if(playerInSightRange && !playerInAttackRange) { FollowPlayer(); }
        if(playerInSightRange && playerInAttackRange) { AttackPlayer(); }
    }

    private void CheckPlayerRange()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    }

    private void Patrol()
    {
        if (idleActive) { return; }

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
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);
        }

        Vector3 walkDistance = transform.position - walkPoint;

        if(walkDistance.magnitude < 1f)
        {
            nextWalkPointSet = false;
        }
    }

    private void FindNextWalkPoint()
    {
        float z = Random.Range(-walkPointRange, walkPointRange);
        float x = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            nextWalkPointSet = true;
        }
    }

    private IEnumerator Idle()
    {
        idleActive = true;
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(7f);
        animator.SetBool("Idle", false);
        idleActive = false;
    }

    private void FollowPlayer()
    {
        agent.speed = runningSpeed;
        //animator.SetTrigger("Run");
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Run", true);
        nextWalkPointSet = false;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);

        if (!alreadyAttacked)
        {
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
    }
}
