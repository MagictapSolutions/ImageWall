using UnityEngine;

public class MoveAround : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 direction;

    void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // If the image object goes out of bounds, reverse the direction
        if (transform.position.x > 5f || transform.position.x < -5f)
            direction.x = -direction.x;

        if (transform.position.y > 5f || transform.position.y < -5f)
            direction.y = -direction.y;
    }
}
