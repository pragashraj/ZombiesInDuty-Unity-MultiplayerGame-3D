using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bool objective10 = gameManager.Objective10Completed;

        if (other.gameObject.tag == "Player" && objective10)
        {
            gameManager.HandleCompletionUI("Objective 10 completed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}
