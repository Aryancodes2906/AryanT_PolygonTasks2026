using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    public float leftBound = -5f;
    public float rightBound = 5f;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        float posX = transform.position.x;

        if (posX < leftBound || posX > rightBound)
        {
            speed *= -1f;
        }
    }
}