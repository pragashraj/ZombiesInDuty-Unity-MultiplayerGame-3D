using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Objective7 : MonoBehaviour
{
    [SerializeField] private GameObject conversationpanel;
    [SerializeField] private Text speaker;
    [SerializeField] private Text message;

    private GameManager gameManager;
    private Animator animator;
    private GameObject player;

    private List<Conversation> conversations = new List<Conversation>();

    private string[] messages = new string[] {
        "Oh captain you came",
        "Where is Dr.Cooper?",
        "We're sorry, he effected too",
        "what?",
        "Ya, that's true",
        "So what's next?",
        "Please collect the file case and go",
        "And what about you all?",
        "Once you cleared out the area, we follow you!",
        "Ok get ready..."
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
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetConversation(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool objective6 = gameManager.Objective6Completed;
        bool objective7 = gameManager.Objective7Completed;

        if (other.gameObject.tag == "Player")
        {
            if (objective6 && !objective7)
            {
                conversationpanel.SetActive(true);
                animator.SetBool("Working", false);
                animator.SetBool("Talking", true);
                transform.LookAt(player.transform);
                StartCoroutine(HandleMessages());
            }

            if (objective7)
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
        gameManager.HandleCompletionUI("Objective 7 completed");
        animator.SetBool("Talking", false);
        animator.SetBool("Working", true);
    }
}
