using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        WALKING, CHASING, ATTACKING
    }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runningSpeed = 6f;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 targetPosition;
    private State state;

    private EnemyAI enemyAI;
    private Animator animator;
    private Transform player;

    private void Awake()
    {
        enemyAI = gameObject.GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("PlayerX").transform;
    }

    private void Start()
    {
        state = State.WALKING;
        startingPosition = transform.position;
        roamPosition = enemyAI.GetRoamingPosition(startingPosition);
    }

    private void Update()
    {
        SwitchMovements();
        FindTarget();
        IsTargetNear();
        StopChasing();
    }

    private void SwitchMovements()
    {
        switch (state)
        {
            case State.WALKING: HandleWalk();
                break;
            case State.CHASING: HandleChase();
                break;
            case State.ATTACKING: HandleAttack();
                break;
            default: return;
        }
    }

    private void HandleWalk()
    {
        animator.SetBool("Walk", true);
        enemyAI.MoveTo(roamPosition);
        agent.speed = walkingSpeed;
        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = enemyAI.GetRoamingPosition(startingPosition);
        }
    }

    private void HandleChase()
    {
        animator.SetBool("Run", true);
        transform.LookAt(player);
        targetPosition = player.position + offset;
        enemyAI.MoveTo(targetPosition);
        agent.speed = runningSpeed;
    }

    private void HandleAttack()
    {
        animator.SetBool("Attack", true);
        transform.LookAt(player);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.position) < 20f)
        {
            state = State.CHASING;
        } else
        {
            state = State.WALKING;
            animator.SetBool("Run", false);
        }
    }

    private void IsTargetNear()
    {
        if (Vector3.Distance(transform.position, player.position) <= 2.5f)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            state = State.ATTACKING;
            agent.isStopped = true;
        } else
        {
            animator.SetBool("Attack", false);
            agent.isStopped = false;
        }
    }

    private void StopChasing()
    {
        if (state == State.CHASING)
        {
            if (Vector3.Distance(transform.position, player.position) > 20f)
            {
                animator.SetBool("Run", false);
                state = State.WALKING;
            }
        }
    }
}
