using UnityEngine;

public class FishSwim : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float leftLimit = -10f;
    [SerializeField] float rightLimit = 10f;

    int direction;

    void Start()
    {
        if (Random.value < 0.5f)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        FlipToDirection();
    }

    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * direction * speed * Time.deltaTime;

        if (transform.position.x > rightLimit || transform.position.x < leftLimit)
        {
            direction *= -1;
            FlipToDirection();
        }
    }

    void FlipToDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -direction;
        transform.localScale = scale;
    }
}