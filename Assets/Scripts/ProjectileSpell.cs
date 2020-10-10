using UnityEngine;

public class ProjectileSpell : Spell
{
    [SerializeField] float speed = 100f;
    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 orientation)
    {
        rigidbody.AddForce(orientation * speed);
    }
}
