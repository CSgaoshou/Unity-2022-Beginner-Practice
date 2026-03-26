using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            other.transform.position = targetPosition;
        }
    }
}