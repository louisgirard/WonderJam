using UnityEngine;

public class MemoryPotion : MonoBehaviour
{
    [SerializeField] float memory = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMemory playerMemory = collision.GetComponent<PlayerMemory>();
            if (playerMemory.CanHeal())
            {
                playerMemory.Heal(memory);
                // Heal Animation
                Destroy(gameObject);
            }
        }
    }
}
