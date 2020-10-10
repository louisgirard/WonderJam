using UnityEngine;

public class FireballSpell : ProjectileSpell
{
    public override void Launch(Vector2 orientation)
    {
        rigidbody.AddForce(orientation * speed);

        if (Random.Range(0f, 100f) > efficacy)
        {
            // Failure, GOES WRONG !!!
            float coefficient = Random.Range(0f, 1f);
            transform.localScale *= coefficient;
            power *= coefficient;
        }
    }
}
