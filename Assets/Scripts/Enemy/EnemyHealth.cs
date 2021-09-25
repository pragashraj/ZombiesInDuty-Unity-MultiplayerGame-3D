using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    [Range(0, 100)]
    private float health = 100;

    public float Health { get => health; set => health = value; }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (health == 0)
        {
            animator.SetTrigger("Death");
        }
    }

    public void ReduceHealth(float count)
    {
        health -= count;
    }
}
