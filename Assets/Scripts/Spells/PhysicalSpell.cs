using UnityEngine;

public class PhysicalSpell : Spell
{
    [SerializeField] float distance = 0.5f;
    [SerializeField] ParticleSystem goodParticle;
    [SerializeField] ParticleSystem wrongParticle;

    new ParticleSystem particleSystem;

    public override void Launch(float efficacy)
    {
        if (Random.Range(0f, 100f) <= efficacy)
        {
            particleSystem = goodParticle;
        }
        else
        {
            // Failure, GOES WRONG !!!
            particleSystem = wrongParticle;
            power *= -1f;
        }
        Instantiate(particleSystem, transform.position, Quaternion.identity, transform);

        Destroy(gameObject, particleSystem.main.duration);
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
