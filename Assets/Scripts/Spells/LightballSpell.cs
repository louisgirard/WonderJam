using UnityEngine;

public class LightballSpell : ProjectileSpell
{
    public override bool Launch(float efficacy)
    {
        if (Random.Range(0f, 100f) <= efficacy)
        {
            // Success
            rigidbody.AddForce(orientation * speed);
            return true;
        }
        else
        {
            // Failure, GOES WRONG !!!
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rigidbody.AddForce(direction * speed);
            return false;
        }
    }
}
