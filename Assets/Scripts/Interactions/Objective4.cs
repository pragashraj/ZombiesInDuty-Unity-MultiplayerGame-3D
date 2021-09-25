using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Objective4 : MonoBehaviour
{
    [SerializeField] private GameObject[] batteries;
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private Sprite triggerImage;

    private GameManager gameManager;
    private AudioManager audioManager;

    private bool onStay;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        ChangeEmissionColor(Color.red);
    }

    
    void Update()
    {
        if (onStay)
        {
            HandleSwitchOnPress();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerUI.GetComponent<Image>().sprite = triggerImage;

        bool objective3 = gameManager.Objective3Completed;
        bool objective4 = gameManager.Objective4Completed;

        if (other.gameObject.tag == "Player" && objective3 && !objective4)
        {
            onStay = true;
            triggerUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onStay = false;
            triggerUI.SetActive(false);
        }
    }

    private void ChangeEmissionColor(Color color)
    {
        for (int i = 0; i < batteries.Length; i++)
        {
            List<Material> materials = batteries[i].GetComponent<Renderer>().materials.ToList();
            materials[1].SetColor("_EmissionColor", color);
        }
    }

    private void HandleSwitchOnPress()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioManager.Play("SwitchOn");
            ChangeEmissionColor(Color.cyan);
            triggerUI.SetActive(false);
            gameManager.Objective4Completed = true;
            gameManager.HandleCompletionUI("Objective 4 completed");
        }
    }
}
