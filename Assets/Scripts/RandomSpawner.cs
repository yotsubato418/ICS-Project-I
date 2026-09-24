using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 randomPosition = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));
            Instantiate(ItemPrefab, randomPosition, Quaternion.identity);
        }
    }
}
