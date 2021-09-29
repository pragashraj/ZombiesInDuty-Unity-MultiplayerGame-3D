using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    private float health = 100;

    private bool dead = false;

    public float Health { get => health; set => health = value; }
    public bool Dead { get => dead; set => dead = value; }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (health == 0 && !Dead)
        {
            Dead = true;
            animator.SetTrigger("Death");
            agent.isStopped = true;
            Vector3 pos = gameObject.transform.position;
            pos.y = 0.8f;
            gameObject.transform.position = pos;
            StartCoroutine(DeActivate());
        }
    }

    public void ReduceHealth(float count)
    {
        if (health > 0)
        {
            float healthTemp = health - count;
            if (healthTemp < 0)
            {
                health = 0;
                agent.isStopped = true;
            }
            else
            {
                health = healthTemp;
            }
        }
    }

    IEnumerator DeActivate()
    {
        yield return new WaitForSeconds(6f);
        gameObject.SetActive(false);
    }
}
