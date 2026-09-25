using UnityEngine.InputSystem;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    public float[] SpawnWeights;
    public float[] DepthWeight;

    public Transform hookTransform;
    public float bottomLimit = -18f;
    bool stoppedAtBottom = false;

    public float spawnInterval = 3.3f;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -6f;
    public float maxY = 1f;
    public float depthLimit = 3f;

    Camera mainCamera;
    float startPosY;
    bool started = false;
    void Start()
    {
        mainCamera = Camera.main;
        startPosY = mainCamera.transform.position.y;
    }

    void Update()
    {
        if (!started && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            started = true;
            InvokeRepeating(nameof(SpawnFish), 0f, spawnInterval);
        }

        if (started && !stoppedAtBottom && hookTransform != null && hookTransform.position.y <= bottomLimit)
        {
            stoppedAtBottom = true;
            CancelInvoke(nameof(SpawnFish));
        }
    }
    void SpawnFish()
    {
        float cameraY = mainCamera.transform.position.y;
        float depth = startPosY - cameraY;
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), cameraY + Random.Range(minY, maxY), 0f);
        GameObject chosenPrefab = ItemPrefabs[GetWeightedIndex(depth)];
        Instantiate(chosenPrefab, randomPosition, Quaternion.identity);
    }

    int GetWeightedIndex(float depth)
    {
        float tWeight = 0f;
        float[] depthWeight = new float[SpawnWeights.Length];
        for (int i = 0; i < SpawnWeights.Length; i++)
        {
            float adjustedDepth = Mathf.Max(0f, depth - depthLimit);
            depthWeight[i] = SpawnWeights[i] + DepthWeight[i] * adjustedDepth;
            if (depthWeight[i] < 0f)
                depthWeight[i] = 0f;
            tWeight += depthWeight[i];
        }
        float roll = Random.Range(0f, tWeight);
        float cumulative = 0f;

        for (int i = 0; i < depthWeight.Length; i++)
        {
            cumulative += depthWeight[i];
            if (roll < cumulative)
                return i;
        }
        return depthWeight.Length - 1;
    }
}
