using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TornadoSpell : Spell
{
    [SerializeField] float pullForce = 500f;
    float radius;

    readonly HashSet<string> attractedTags = new HashSet<string>();
    readonly HashSet<Collider2D> totalColliders = new HashSet<Collider2D>();
    List<Collider2D> inRangeColliders = new List<Collider2D>();
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

    public void FixedUpdate()
    {
        inRangeColliders = Physics2D.OverlapCircleAll(transform.position, radius).ToList<Collider2D>();

        AddAttractionForce();

        // Remove forces on colliders that are not range but were in range before
        RemoveAttractionForce();
    }

    public void Launch()
    {
        if (Random.Range(0f, 100f) <= efficacy)
        {
            // Success
            attractedTags.Add("Enemy");
            radius = transform.localScale.x / 2;
        }
        else
        {
            // Failure, GOES WRONG !!!
            attractedTags.Add("Enemy");
            attractedTags.Add("Player");
            float coefficient = Random.Range(2f, 2.5f);
            transform.localScale *= coefficient;
            pullForce *= 3f;
            radius = transform.localScale.x / 2;
        }
    }

    private void AddAttractionForce()
    {
        foreach (Collider2D collider in inRangeColliders)
        {
            if (!attractedTags.Contains(collider.tag)) { continue; }

            ClearForce(collider);

            totalColliders.Add(collider);

            Vector3 forceDirection = transform.position - collider.transform.position;
            if(forceDirection.magnitude > 0.01f)
            {
                collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }
        }
    }

    private void RemoveAttractionForce()
    {
        totalColliders.RemoveWhere(delegate (Collider2D collider)
        {
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

    private void StopAttraction()
    {
        foreach (Collider2D collider in totalColliders)
        {
            ClearForce(collider);
        }
    }
}
