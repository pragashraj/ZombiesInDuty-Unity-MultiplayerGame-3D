using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Officer : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject conversationpanel;
    [SerializeField] private Text speaker;
    [SerializeField] private Text message;

    private AudioManager audioManager;
    private FirstPersonController firstPersonController;
    private EnemyManager enemyManager;

    private List<Conversation> conversations = new List<Conversation>();
    private bool actionCompleted;

    private string[] messages = new string[] { 
        "Happy to have you and your crew here captain!",
        "Whats just happened here?",
        "I dont know captain, they are behaving upnormal",
        "What do you mean by upnoraml?",
        "I'm not sure kind a animal behaviour...",
        "Oh man.... open the door"
    };

    private void Awake()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            Conversation conversation = new Conversation();
            conversation.speaker = i % 2 == 0 ? "Security" : "Captain";
            conversation.message = messages[i];
            conversations.Add(conversation);
        }
    }

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.enabled = false;
        SetConversation(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !actionCompleted)
        {
            firstPersonController = other.gameObject.GetComponent<FirstPersonController>();
            conversationpanel.SetActive(true);
            firstPersonController.enabled = false;
            StartCoroutine(HandleMessages());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            conversationpanel.SetActive(false);
        }
    }

    private void HandleConversationComplete()
    {
        firstPersonController.enabled = true;
        door.transform.GetChild(0).GetComponentInChildren<Animation>().Play("LeftOpen");
        door.transform.GetChild(1).GetComponentInChildren<Animation>().Play("RightOpen");
        audioManager.Play("GO");
        enemyManager.enabled = true;
    }

    private void SetConversation(int i)
    {
        if (i < conversations.Count)
        {
            Conversation conversation = conversations[i];
            if (conversation != null)
            {
                speaker.text = conversation.speaker + " : ";
                message.text = conversation.message;
            }
        }
    }

    IEnumerator HandleMessages()
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            SetConversation(i);
            yield return new WaitForSeconds(4f);
        }

        actionCompleted = true;
        conversationpanel.SetActive(false);
        HandleConversationComplete();
    }
}
