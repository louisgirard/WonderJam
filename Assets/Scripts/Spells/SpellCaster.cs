using System.Collections;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    SpellsHolder spellsHolder;

    PlayerOrientation playerOrientation;
    PlayerMemory playerMemory;

    bool[] canCastSpell = new bool[] { true, true, true, true };

    private void Start()
    {
        spellsHolder = GetComponent<SpellsHolder>();
        playerOrientation = GetComponent<PlayerOrientation>();
        playerMemory = GetComponent<PlayerMemory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canCastSpell[0])
        {
            Spell selectedSpell = SelectSpell(0);
            if(selectedSpell != null)
            {
                StartCoroutine(ProcessSelectedSpell(selectedSpell));
            }
        }
        if (Input.GetMouseButtonDown(1) && canCastSpell[1])
        {
            Spell selectedSpell = SelectSpell(1);
            if (selectedSpell != null)
            {
                StartCoroutine(ProcessSelectedSpell(selectedSpell));
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && canCastSpell[2])
        {
            Spell selectedSpell = SelectSpell(2);
            if (selectedSpell != null)
            {
                StartCoroutine(ProcessSelectedSpell(selectedSpell));
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && canCastSpell[3])
        {
            Spell selectedSpell = SelectSpell(3);
            if (selectedSpell != null)
            {
                StartCoroutine(ProcessSelectedSpell(selectedSpell));
            }
        }
    }

    private Spell SelectSpell(int index)
    {
        Spell spell = spellsHolder.GetSpell(index);
        // No memory == random spells
        if (playerMemory.GetMemoryPercentage() == 0)
        {
            spell = spellsHolder.RandomSpell();
        }
        return spell;
    }

    IEnumerator ProcessSelectedSpell(Spell selectedSpell)
    {
        Spell currentSpell = selectedSpell;
        canCastSpell[spellsHolder.IndexOf(currentSpell)] = false;

        if (currentSpell is PhysicalSpell physicalSpell)
        {
            Vector3 position = playerOrientation.GetOrientation() * physicalSpell.GetDistance();
            // Instantiate around player
            Spell instantiatedSpell = Instantiate(currentSpell, transform.position + position, Quaternion.identity);
            instantiatedSpell.Launch(playerMemory.GetMemoryPercentage());
        }
        else if (currentSpell is TornadoSpell)
        {
            // Instantiate at mouse location
            TornadoSpell instantiatedSpell = (TornadoSpell)Instantiate(currentSpell, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            instantiatedSpell.Launch(playerMemory.GetMemoryPercentage());
        }
        else // Projectile
        {
            // Instantiate on player + add force
            ProjectileSpell instantiatedSpell = (ProjectileSpell)Instantiate(currentSpell, transform.position, Quaternion.identity);
            instantiatedSpell.SetOrientation(playerOrientation.GetOrientation());
            instantiatedSpell.Launch(playerMemory.GetMemoryPercentage());
        }

        yield return new WaitForSeconds(currentSpell.timeBetweenCast);

        canCastSpell[spellsHolder.IndexOf(currentSpell)] = true;
    }
}
