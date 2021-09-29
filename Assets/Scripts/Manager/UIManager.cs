using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponCurrent;
    [SerializeField] private GameObject[] weaponTotal;

    private PlayerWeaponController weaponController;

    void Start()
    {
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponController>();
    }

    
    void Update()
    {
        HandleUIValuesChange();
    }

    private void HandleUIValuesChange()
    {
        Weapon[] weapons = weaponController.GetWeapons();
        for (int i = 0; i < weapons.Length - 1; i++)
        {
            Weapon weapon = weapons[i];
            SetText(weaponCurrent[i], weapon.currentAmmo.ToString());
            SetText(weaponTotal[i], weapon.totalBullet.ToString());
        }
    }

    private void SetText(GameObject obj, string value)
    {
        obj.GetComponent<Text>().text = value;
    }
}
