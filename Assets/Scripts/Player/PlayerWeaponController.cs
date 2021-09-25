using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject fpsCam;

    private float damage = 20f;
    private float range = 100f;

    void Start()
    {
        
    }

    
    void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Transform cam = fpsCam.transform;
            if (Physics.Raycast(cam.position, cam.transform.forward, out hit, range))
            {
                Transform target = hit.transform;
                if (target.tag == "Enemy")
                {
                    EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                    enemyHealth.ReduceHealth(damage);
                }
            }
        }
    }
}
