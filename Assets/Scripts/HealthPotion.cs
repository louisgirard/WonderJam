using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] float health = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth.CanHeal())
            {
                playerHealth.Heal(health);
                // Heal Animation
                Destroy(gameObject);
            }
        }
    }
}
