using Unity.Mathematics;
using UnityEngine;

public class SpellScroll : MonoBehaviour
{
    [SerializeField] Spell spell = null;
    [SerializeField] ParticleSystem animationParticles = null;

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
