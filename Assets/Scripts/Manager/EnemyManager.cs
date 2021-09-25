using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] positions;

    void Start()
    {
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

    private void SetPrefabsInPosition(Vector3 pos, GameObject prefab, int i)
    {
        int rand = Random.Range(0, 10);
        GameObject instantiatedObject = Instantiate(prefab, pos, Quaternion.Euler(new Vector3(0, i * 10 * rand, 0)));
        instantiatedObject.name = prefab.gameObject.name + i.ToString();
    }
}
