using UnityEngine;

public class LightballSpell : ProjectileSpell
{
    public override void Launch(Vector2 orientation)
    {
        if (Random.Range(0f, 100f) <= efficacy)
        {
            // Success
            rigidbody.AddForce(orientation * speed);
        }
        else
        {
            // Failure, GOES WRONG !!!
            rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed);
        }
    }
}
