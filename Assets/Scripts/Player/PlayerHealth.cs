using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image content;
    [SerializeField] private float lerpSpeed;

    private MenuManager menuManager;
    private PlayerWeaponController weaponController;
    private FirstPersonController firstPersonController;

    private float health = 100;
    private bool isDead;

    public float Health { get => health; set => health = value; }

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        weaponController = gameObject.GetComponent<PlayerWeaponController>();
        firstPersonController = gameObject.GetComponent<FirstPersonController>();
    }

    void Update()
    {
        float amount = Map(Health, 100, 1);
        content.fillAmount = Mathf.Lerp(content.fillAmount, amount, Time.deltaTime * lerpSpeed);

        if (health <= 0 && !isDead)
        {
            isDead = true;
            menuManager.HandleGameEndMenuActive(true);
            weaponController.enabled = false;
            firstPersonController.enabled = false;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }

    private float Map(float value, float max, float min)
    {
        return value * min / max;
    }

    public void IncreaseHealthValue(float count)
    {
        if (count != 100)
        {
            float healthTemp = health + count;
            if (healthTemp > 100)
            {
                health = 100;
            }
            else
            {
                health = healthTemp;
            }
        }
    }

    public void DecreaseHealthValue(float count)
    {
        if (count != 0)
        {
            float healthTemp = health - count;
            if (healthTemp < 0)
            {
                health = 0;
            }
            else
            {
                health = healthTemp;
            }
        }
    }
}
