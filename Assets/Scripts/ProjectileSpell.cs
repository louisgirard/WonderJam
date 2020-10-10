using UnityEngine;

public class ProjectileSpell : Spell
{
    [SerializeField] float speed = 100f;
    [SerializeField] ParticleSystem hitParticles;
    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 orientation)
    {
        rigidbody.AddForce(orientation * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") || collision.tag.Equals("Spell")) { return; }

        // Explosion
        ParticleSystem explosion = Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(explosion.gameObject, hitParticles.main.duration);

        // Damage
        if (collision.tag.Equals("Enemy"))
        {
            Health health = collision.transform.GetComponent<Health>();
            health.TakeDamage(power);
        }

        Destroy(gameObject);
    }
}
