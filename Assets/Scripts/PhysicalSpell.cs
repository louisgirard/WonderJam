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

        if (collision.tag.Equals("Player") || collision.tag.Equals("Spell")) { return; }

        // Damage
        if (collision.tag.Equals("Enemy"))
        {
            Health health = collision.transform.GetComponent<Health>();
            health.TakeDamage(power);
        }
    }
}
