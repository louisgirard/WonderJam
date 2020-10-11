using UnityEngine;

public class PhysicalSpell : Spell
{
    [SerializeField] float distance = 0.5f;
    [SerializeField] ParticleSystem goodParticle = null;
    [SerializeField] ParticleSystem wrongParticle = null;

    new ParticleSystem particleSystem;

    public override bool Launch(float efficacy)
    {
        bool success;
        if (Random.Range(0f, 100f) <= efficacy)
        {
            particleSystem = goodParticle;
            success = true;
        }
        else
        {
            // Failure, GOES WRONG !!!
            particleSystem = wrongParticle;
            power *= -1f;
            success = false;
        }
        Instantiate(particleSystem, transform.position, Quaternion.identity, transform);

        Destroy(gameObject, particleSystem.main.duration);

        return success;
    }

    public float GetDistance()
    {
        return distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") || collision.CompareTag("Spell")) { return; }

        // Damage
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth health = collision.transform.GetComponent<EnemyHealth>();
            health.TakeDamage(power);
        }
    }
}
