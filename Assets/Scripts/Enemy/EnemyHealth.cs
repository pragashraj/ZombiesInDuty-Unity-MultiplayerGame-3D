using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private float health = 100;

    public float Health { get => health; set => health = value; }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (health == 0)
        {
            animator.SetTrigger("Death");
            agent.isStopped = true;
        }
    }

    public void ReduceHealth(float count)
    {
        if (health > 0)
        {
            health -= count;
        }
    }
}
