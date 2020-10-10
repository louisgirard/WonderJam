using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Cast(Vector2 mousePosition)
    {
        rigidbody.AddForce(mousePosition * speed);
    }
}
