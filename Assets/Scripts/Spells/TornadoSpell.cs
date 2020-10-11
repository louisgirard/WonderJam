using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TornadoSpell : Spell
{
    [SerializeField] float pullForce = 0.5f;
    float radius;

    readonly HashSet<string> attractedTags = new HashSet<string>();
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
        }
        AddAttractionForce();
    }

    public override bool Launch(float efficacy)
    {
        if (Random.Range(0f, 100f) <= efficacy)
        {
            // Success
            attractedTags.Add("Enemy");
            radius = transform.localScale.x / 2;
            return true;
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
            return false;
        }
    }

    private void AddAttractionForce()
    {
        inRangeColliders = Physics2D.OverlapCircleAll(transform.position, radius).ToList<Collider2D>();
        foreach (Collider2D collider in inRangeColliders)
        {
            if (!attractedTags.Contains(collider.tag)) { continue; }

            Vector3 forceDirection = transform.position - collider.transform.position;
            if(forceDirection.magnitude > 0.01f)
            {
                collider.transform.position = collider.transform.position + forceDirection.normalized * pullForce * Time.deltaTime;
            }
        }
    }
}
