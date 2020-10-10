using UnityEngine;

public class PhysicalSpell : Spell
{
    [SerializeField] float distance = 0.5f;

    public float GetDistance()
    {
        return distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);

        if (collision.CompareTag("Player") || collision.CompareTag("Spell")) { return; }

        // Damage
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.transform.GetComponent<Health>();
            health.TakeDamage(power);
        }
    }
}
