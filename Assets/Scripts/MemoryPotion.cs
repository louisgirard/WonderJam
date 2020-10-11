using UnityEngine;

public class MemoryPotion : MonoBehaviour
{
    [SerializeField] float memory = 1f;
    [SerializeField] ParticleSystem animationParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMemory playerMemory = collision.GetComponent<PlayerMemory>();
            if (playerMemory.CanHeal())
            {
                playerMemory.Heal(memory);
                Instantiate(animationParticles, collision.transform.position, Quaternion.identity, collision.transform);
                Destroy(gameObject);
            }
        }
    }
}
