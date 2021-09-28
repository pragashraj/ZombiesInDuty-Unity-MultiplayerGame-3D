using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Objective9 : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] positions;

    private GameManager gameManager;

    private bool objectiveInitiated;
    private List<GameObject> instantiatedEnemies;
    private List<GameObject> deadEnemies;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        instantiatedEnemies = new List<GameObject>();
        deadEnemies = new List<GameObject>();
    }

    
    void Update()
    {
        bool objective8 = gameManager.Objective8Completed;
        bool objective9 = gameManager.Objective9Completed;

        if (objective8 && !objective9 && !objectiveInitiated)
        {
            objectiveInitiated = true;
            int count = positions.Length;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Vector3 pos = positions[i];
                    int rand = Random.Range(0, enemyPrefabs.Length);
                    SetPrefabsInPosition(pos, enemyPrefabs[rand], i);
                }
            }
        }

        if (objective8 && !objective9)
        {
            CheckObjectiveCompletion();
        }
    }

    private void SetPrefabsInPosition(Vector3 pos, GameObject prefab, int i)
    {
        int rand = Random.Range(0, 10);
        GameObject instantiatedObject = Instantiate(prefab, pos, Quaternion.Euler(new Vector3(0, i * 10 * rand, 0)));
        instantiatedObject.name = prefab.gameObject.name + i.ToString();
        instantiatedEnemies.Add(instantiatedObject);
    }

    private void CheckObjectiveCompletion()
    {
        int count = instantiatedEnemies.Count;

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject enemy = instantiatedEnemies[i];
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

                if (enemyHealth.Dead)
                {
                    deadEnemies.Add(enemy);
                }
            }

            if (deadEnemies.Count == count)
            {
                gameManager.Objective9Completed = true;
                gameManager.HandleCompletionUI("Objective 9 completed");
                StartCoroutine(NextMessage());
            }
        }
    }


    IEnumerator NextMessage()
    {
        yield return new WaitForSeconds(4f);
        gameManager.CompletionMessageUI("Ok lets get out from here");
    }
}
