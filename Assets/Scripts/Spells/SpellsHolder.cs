using System.Collections.Generic;
using UnityEngine;

public class SpellsHolder : MonoBehaviour
{
    [SerializeField] List<Spell> spells = null;

    public Spell GetSpell(int index)
    {
        if(index >= spells.Count)
        {
            return null;
        }
        else
        {
            return spells[index];
        }
    }

    public Spell RandomSpell()
    {
        return spells[Random.Range(0, spells.Count)];
    }

    public int IndexOf(Spell spell)
    {
        return spells.IndexOf(spell);
    }

    public bool CanLearn(Spell spell)
    {
        return !spells.Contains(spell);
    }

    public void LearnSpell(Spell spell)
    {
        spells.Add(spell);
    }
}
