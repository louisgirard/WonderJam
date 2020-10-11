using Unity.Mathematics;
using UnityEngine;

public class SpellScroll : MonoBehaviour
{
    [SerializeField] Spell spell;
    [SerializeField] ParticleSystem animationParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpellsHolder spellsHolder = collision.GetComponent<SpellsHolder>();
            if (spellsHolder.CanLearn(spell))
            {
                spellsHolder.LearnSpell(spell);
                Instantiate(animationParticles, collision.transform.position, Quaternion.identity, collision.transform);
                Destroy(gameObject);
            }
        }
    }
}
