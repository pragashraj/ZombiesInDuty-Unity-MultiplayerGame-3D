using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Objective3 : MonoBehaviour
{
    [SerializeField] private GameObject conversationpanel;
    [SerializeField] private Text speaker;
    [SerializeField] private Text message;

    private GameManager gameManager;
    private FirstPersonController firstPersonController;

    private List<Conversation> conversations = new List<Conversation>();

    private string[] messages = new string[] {
        "Hello captain, finally you arrived!",
        "Can you please tell me whats happening here?",
        "Our specialist and crew worked hard to produce a medical solution...",
        "For what?",
        "For covid ofcourse ",
        "and....",
        "Unfortunately we failed and when we test it against human...",
        "Wait you test it against human?",
        "Ya, thats why those people effected and turned into zombies!",
        "What the hell..., what we need to do now?",
        "Simple. lets find Dr.Cooper and create anti-solution.",
        "Where is he now?",
        "I think he struct in Medic lab, we lost our connection actually!",
        "What we need to do now ?",
        "First you need to Regenerate the whole power source",
        "Ok we're going...."
    };

    private void Awake()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            Conversation conversation = new Conversation();
            conversation.speaker = i % 2 == 0 ? "Worker" : "Captain";
            conversation.message = messages[i];
            conversations.Add(conversation);
        }
    }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        SetConversation(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool objective2 = gameManager.Objective2Completed;
        bool objective3 = gameManager.Objective3Completed;

        if (other.gameObject.tag == "Player")
        {
            if (objective2 && !objective3)
            {
                firstPersonController = other.gameObject.GetComponent<FirstPersonController>();
                conversationpanel.SetActive(true);
                firstPersonController.enabled = false;
                StartCoroutine(HandleMessages());
            }

            if (objective3)
            {
                SetConversationValue("Worker", "Go then....");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            conversationpanel.SetActive(false);
        }
    }

    private void SetConversationValue(string spk, string msg)
    {
        speaker.text = spk + " : ";
        message.text = msg;
    }

    private void SetConversation(int i)
    {
        if (i < conversations.Count)
        {
            Conversation conversation = conversations[i];
            if (conversation != null)
            {
                SetConversationValue(conversation.speaker, conversation.message);
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

        conversationpanel.SetActive(false);
        HandleConversationComplete();
    }

    private void HandleConversationComplete()
    {
        gameManager.Objective3Completed = true;
        firstPersonController.enabled = true;
        gameManager.HandleCompletionUI("Objective 3 completed");
    }
}
