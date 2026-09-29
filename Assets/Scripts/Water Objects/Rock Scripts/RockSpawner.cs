using UnityEngine.InputSystem;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    public float SpawnWeights = 15f;
    public float DepthWeight = 1f;

    public Transform hookTransform;

    public float spawnInterval = 2f;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -20f;
    public float maxY = -3f;
    public float depthLimit = 3f;
    public float waterSurfaceY = -3f;

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
            InvokeRepeating(nameof(SpawnRock), 0f, spawnInterval);
        }
    }
    void SpawnRock()
    {
        float cameraY = mainCamera.transform.position.y;
        float depth = startPosY - cameraY;
        float effectiveY = Mathf.Min(cameraY, waterSurfaceY);

        float adjustedDepth = Mathf.Max(0f, depth - depthLimit);
        float weight = SpawnWeights + DepthWeight * adjustedDepth;
        int spawnCount = (int)(weight / 10f);

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), effectiveY + Random.Range(minY, maxY), 0f);
            GameObject chosenPrefab = ItemPrefabs[Random.Range(0, ItemPrefabs.Length)];
            Instantiate(chosenPrefab, randomPosition, Quaternion.identity);
        }
    }
}