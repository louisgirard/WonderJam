using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 50f;
    float health;

    private void Start()
    {
        health = maxHealth;
        GameManager.m_instance.AddEnemy(this);
    }

    private float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (health == 0)
        {
            Destroy(gameObject);
            GameManager.m_instance.RemoveEnemy(this);
        }
    }
}
