using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject fpsCam;
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float radius = 5f;
    [SerializeField] private GameObject bloodEffect;

    private float range = 100f;
    private int currentActiveIndex = 0;
    private bool reloading;
    private string weaponType;
    private bool weaponDisabled;

    private Weapon weapon;
    private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        if (weapons.Length > 0)
        {
            weapon = weapons[0];
            animator = weapon.weaponObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (weaponDisabled)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].weaponObject.SetActive(false);
            }
        } 
        else
        {
            HandleShoot();
            HandleWeaponSwitch();
            HandleWeaponActive();
            HandleReloading();
        }
    }

    private void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentActiveIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentActiveIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentActiveIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentActiveIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentActiveIndex = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentActiveIndex = 5;
        }
    }

    private void HandleWeaponActive()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == currentActiveIndex)
            {
                weapon = weapons[i];
                weapon.weaponObject.SetActive(true);
                animator = weapon.weaponObject.GetComponent<Animator>();

                if (weapon.name == "Grenade" || weapon.name == "Knife")
                {
                    weaponType = "Other";
                } 
                else
                {
                    weaponType = "Gun";
                }
            }
            else
            {
                weapons[i].weaponObject.SetActive(false);
            }
        }
    }

    private void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weapon.name == "Grenade")
            {
                StartCoroutine(HandleGranede());
            } 
            else if (weapon.name == "Knife")
            {
                HandleKnife();
            }
            else
            {
                StartCoroutine(HandleGunShot());
            }
        }
    }

    IEnumerator HandleGunShot()
    {
        if (!reloading)
        {
            RaycastHit hit;
            Transform cam = fpsCam.transform;

            animator.SetTrigger("Shoot");
            PlayAudio(weapon.shootSound);

            if (Physics.Raycast(cam.position, cam.transform.forward, out hit, range))
            {
                Transform target = hit.transform;
                if (target.tag == "Enemy")
                {
                    EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                    enemyHealth.ReduceHealth(weapon.damage);
                    GameObject blood = Instantiate(bloodEffect, target.transform.position, target.transform.rotation);
                    PlayAudio("Roar");

                    yield return new WaitForSeconds(3f);
                    Destroy(blood);
                }
            }
        }
    }

    private void HandleKnife()
    {
        animator.SetTrigger("Hit");
    }

    IEnumerator HandleGranede()
    {
        animator.SetTrigger("Throw");

        yield return new WaitForSeconds(1f);
        Transform cam = fpsCam.transform;
        GameObject grenede = Instantiate(grenadePrefab, cam.position, cam.rotation);
        Rigidbody rb = grenede.GetComponent<Rigidbody>();
        rb.AddForce(cam.forward * throwForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(3f);
        GameObject explosion = Instantiate(explosionEffect, grenede.transform.position, grenede.transform. rotation);
        PlayAudio("Explosion");

        Collider[] colliders =  Physics.OverlapSphere(grenede.transform.position, radius);
        foreach(Collider nearByObject in colliders)
        {
            if (nearByObject.gameObject.tag == "Enemy")
            {
                nearByObject.GetComponent<EnemyHealth>().ReduceHealth(100f);
            }
        }

        yield return new WaitForSeconds(1f);
        grenede.transform.position = new Vector3(0, -15, 0);
        Destroy(explosion);
        Destroy(grenede);
    }

    private void HandleReloading()
    {
        if (weaponType == "Gun" && Input.GetKeyDown(KeyCode.R))
        {
            reloading = true;
            PlayAudio(weapon.reloadSound);
            animator.SetTrigger("Reload");
            StartCoroutine(EndReload());
        }
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    IEnumerator EndReload()
    {
        yield return new WaitForSeconds(2f);
        reloading = false;
    }

    public void DisableWeapon()
    {
        weaponDisabled = true;
    }

    public void EnableWeapon()
    {
        weaponDisabled = false;
    }
}
