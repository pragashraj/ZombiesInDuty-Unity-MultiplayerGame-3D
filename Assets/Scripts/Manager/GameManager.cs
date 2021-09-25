using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject objectCompletion;
    [SerializeField] private Text completionMessage;
    [SerializeField] private Animation coverPanel;

    private bool objective1Completed;
    private bool objective2Completed;
    private bool objective3Completed;
    private bool objective4Completed = true;
    private bool objective5Completed;
    private bool objective6Completed;

    public bool Objective1Completed { get => objective1Completed; set => objective1Completed = value; }
    public bool Objective2Completed { get => objective2Completed; set => objective2Completed = value; }
    public bool Objective3Completed { get => objective3Completed; set => objective3Completed = value; }
    public bool Objective4Completed { get => objective4Completed; set => objective4Completed = value; }
    public bool Objective5Completed { get => objective5Completed; set => objective5Completed = value; }
    public bool Objective6Completed { get => objective6Completed; set => objective6Completed = value; }

    public void HandleCompletionUI(string message)
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
