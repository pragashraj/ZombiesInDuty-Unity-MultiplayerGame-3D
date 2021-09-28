using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        IDLE, WALKING, CHASING, ATTACKING
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        state = State.IDLE;
        startingPosition = transform.position;
        roamPosition = enemyAI.GetRoamingPosition(startingPosition);
    }

    private void Update()
    {
        HandleAI();
    }

    private void HandleAI()
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
            default: HandleIdle();
                return;
        }
    }

    private void HandleIdle()
    {
        animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
    }

    private void HandleWalk()
    {
        animator.SetFloat("Movement", 0.25f, 0.1f, Time.deltaTime);
        enemyAI.MoveTo(roamPosition);
        agent.speed = walkingSpeed;
        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = enemyAI.GetRoamingPosition(startingPosition);
        }
    }

    private void HandleChase()
    {
        animator.SetFloat("Movement", 0.5f, 0.1f, Time.deltaTime);
        transform.LookAt(player);
        targetPosition = player.position + offset;
        enemyAI.MoveTo(targetPosition);
        agent.speed = runningSpeed;
    }

    private void HandleAttack()
    {
        animator.SetFloat("Movement", 0.75f, 0.1f, Time.deltaTime);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.position) < 20f)
        {
            state = State.CHASING;
        } else
        {
            state = State.WALKING;
            animator.SetFloat("Movement", 0.25f, 0.1f, Time.deltaTime);
        }
    }

    private void IsTargetNear()
    {
        if (Vector3.Distance(transform.position, player.position) <= 2.5f)
        {
            state = State.ATTACKING;
            agent.isStopped = true;
        } else
        {
            agent.isStopped = false;
        }
    }

    private void StopChasing()
    {
        if (state == State.CHASING)
        {
            if (Vector3.Distance(transform.position, player.position) > 20f)
            {
                animator.SetFloat("Movement", 0.25f, 0.1f, Time.deltaTime);
                state = State.WALKING;
            }
        }
    }
}
