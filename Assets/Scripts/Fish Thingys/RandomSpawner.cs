using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float spawnInterval = 2f;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -6f;
    public float maxY = 1f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnFish), 0f, spawnInterval);
    }
    void SpawnFish()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Instantiate(ItemPrefab, randomPosition, Quaternion.identity);
    }
}
