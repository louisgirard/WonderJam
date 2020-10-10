using UnityEngine;

public class ProjectileSpell : Spell
{
    [SerializeField] protected float speed = 100f;
    [SerializeField] protected ParticleSystem hitParticles;
    new protected Rigidbody2D rigidbody;
    protected Vector3 orientation;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetOrientation(Vector2 playerOrientation)
    {
        orientation = playerOrientation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Spell")) { return; }

        // Explosion
        ParticleSystem explosion = Instantiate(hitParticles, transform.position, Quaternion.identity);
        explosion.transform.localScale = transform.localScale;
        Destroy(explosion.gameObject, hitParticles.main.duration);

        // Damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth health = collision.transform.GetComponent<EnemyHealth>();
            health.TakeDamage(power);
        }

        Destroy(gameObject);
    }
}
