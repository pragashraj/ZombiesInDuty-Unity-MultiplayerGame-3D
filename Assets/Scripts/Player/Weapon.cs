using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string name;

    public GameObject weaponObject;

    public float damage;

    public string shootSound;

    public string reloadSound;

    public int totalBullet;

    public int totalAmmo;

    public int currentAmmo;

}
