using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] SpriteRenderer startingBackground;
    [SerializeField] SpriteRenderer loopingBackgroundPrefab;
    [SerializeField] Camera targetCamera;
    [SerializeField] float spawnAheadScreens = 2f;

    float tileHeight;
    float lowestSpawnedY;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        tileHeight = loopingBackgroundPrefab.bounds.size.y;
        lowestSpawnedY = startingBackground.bounds.min.y;

        SpawnUntilCovered();
    }

    void Update()
    {
        SpawnUntilCovered();
    }

    void SpawnUntilCovered()
    {
        float viewBottom = targetCamera.transform.position.y - targetCamera.orthographicSize;
        float neededBottom = viewBottom - tileHeight * spawnAheadScreens;

        while (lowestSpawnedY > neededBottom)
        {
            SpriteRenderer tile = Instantiate(loopingBackgroundPrefab, transform);
            tile.transform.position = new Vector3(
                loopingBackgroundPrefab.transform.position.x,
                lowestSpawnedY - tileHeight / 2f,
                loopingBackgroundPrefab.transform.position.z
            );
            lowestSpawnedY -= tileHeight;
        }
    }
}