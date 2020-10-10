using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 50f;
    float health;

    private void Start()
    {
        health = maxHealth;
    }

    private float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if(health == 0)
        {
            print("hero dead");
        }
        print("health = " + health);
    }
}
