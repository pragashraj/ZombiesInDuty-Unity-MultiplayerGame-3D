using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject objectCompletion;
    [SerializeField] private Text completionMessage;
    [SerializeField] private Animation coverPanel;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private Transform mapCam;
    [SerializeField] private float scale;

    private AudioManager audioManager;

    private bool isMapOpen;

    private bool objective1Completed;
    private bool objective2Completed;
    private bool objective3Completed;
    private bool objective4Completed;
    private bool objective5Completed;
    private bool objective6Completed;
    private bool objective7Completed;
    private bool objective8Completed;

    public bool Objective1Completed { get => objective1Completed; set => objective1Completed = value; }
    public bool Objective2Completed { get => objective2Completed; set => objective2Completed = value; }
    public bool Objective3Completed { get => objective3Completed; set => objective3Completed = value; }
    public bool Objective4Completed { get => objective4Completed; set => objective4Completed = value; }
    public bool Objective5Completed { get => objective5Completed; set => objective5Completed = value; }
    public bool Objective6Completed { get => objective6Completed; set => objective6Completed = value; }
    public bool Objective7Completed { get => objective7Completed; set => objective7Completed = value; }
    public bool Objective8Completed { get => objective8Completed; set => objective8Completed = value; }


    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = !isMapOpen;
            if (isMapOpen)
            {
                PlayAudio("InventoryOpen");
            }
            else
            {
                PlayAudio("InventoryClose");
            }
        }

        mapUI.SetActive(isMapOpen);

        HandleMapScroll();
    }


    private void HandleMapScroll()
    {
        if (isMapOpen)
        {
            float scroll = Input.mouseScrollDelta.y;
            mapCam.Translate(0, scroll * scale, 0, Space.Self);
        }
    }

    private void PlayAudio(string name)
    {
        audioManager.Play(name);
    }

    public void HandleCompletionUI(string message)
    {
        objectCompletion.SetActive(true);
        completionMessage.text = message;
        coverPanel.Play();
        audioManager.Play("Complete");
        StartCoroutine(CloseCompletionUI());
    }

    public void CompletionMessageUI(string message)
    {
        objectCompletion.SetActive(true);
        completionMessage.text = message;
        coverPanel.Play();
        StartCoroutine(CloseCompletionUI());
    }


    IEnumerator CloseCompletionUI()
    {
        yield return new WaitForSeconds(3.5f);
        objectCompletion.SetActive(false);
    }
}
