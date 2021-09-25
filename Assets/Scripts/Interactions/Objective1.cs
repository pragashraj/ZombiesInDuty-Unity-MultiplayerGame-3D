using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Objective1 : MonoBehaviour
{
    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private Sprite triggerImage;

    private List<Material> materials;
    private bool onStay;

    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }


    void Start()
    {
        materials = battery.GetComponent<Renderer>().materials.ToList();
        ChangeEmissionColor(Color.red);
    }

    
    void Update()
    {
        if (onStay)
        {
            HandleSwitchOnPress();
        }
    }


    private void ChangeEmissionColor(Color color)
    {
        materials[1].SetColor("_EmissionColor", color);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerUI.GetComponent<Image>().sprite = triggerImage;

        bool objective1 = gameManager.Objective1Completed;

        if (other.gameObject.tag == "Player" && !objective1)
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

    private void HandleSwitchOnPress()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioManager.Play("SwitchOn");
            ChangeEmissionColor(Color.blue);
            triggerUI.SetActive(false);
            gameManager.Objective1Completed = true;
            gameManager.HandleCompletionUI("Objective 1 completed");
        }
    }
}
