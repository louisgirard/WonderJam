using System.Collections.Generic;
using UnityEngine;

public class SpellsHolder : MonoBehaviour
{
    [SerializeField] List<Spell> spells;

    public Spell GetSpell(int index)
    {
        return spells[index];
    }

    public Spell RandomSpell()
    {
        return spells[Random.Range(0, spells.Count)];
    }
}
