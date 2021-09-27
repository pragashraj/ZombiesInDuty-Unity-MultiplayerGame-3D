using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private AudioManager audioManager;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioManager.Play("Pickup");
            gameObject.transform.position = new Vector3(0, -15, 0);

            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.IncreaseHealthValue(20f);

            StartCoroutine(RemoveObject());
        }
    }

    IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
