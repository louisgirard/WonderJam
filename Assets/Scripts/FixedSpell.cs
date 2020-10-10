using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixedSpell : Spell
{
    [SerializeField] float distanceToPlayer = 6f;
    [SerializeField] float radius = 2f;
    [SerializeField] float pullForce = 5f;

    HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    float timeToLive;

    private void Awake()
    {
        timeToLive = GetComponent<ParticleSystem>().main.duration;
    }

    private void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
            StopAttraction();
        }

    }

    private void StopAttraction()
    {
        foreach (Collider2D collider in colliders)
        {
            ClearForce(collider);
        }
    }

    public void FixedUpdate()
    {
        List<Collider2D> inRangeColliders = Physics2D.OverlapCircleAll(transform.position, radius).ToList<Collider2D>();

        foreach (Collider2D collider in inRangeColliders)
        {
            colliders.Add(collider);
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;

            // apply force on target towards me
            collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
        }
        
        colliders.RemoveWhere(delegate (Collider2D collider) {
            bool notInRange = !inRangeColliders.Contains(collider);
            if (notInRange)
            {
                ClearForce(collider);
            }
            return notInRange;
        });
    }

    private static void ClearForce(Collider2D collider)
    {
        collider.attachedRigidbody.velocity = Vector2.zero;
        collider.attachedRigidbody.angularVelocity = 0;
    }
}
