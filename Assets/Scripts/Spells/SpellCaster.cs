using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    SpellsHolder spellsHolder;
    Spell selectedSpell;

    PlayerOrientation playerOrientation;
    PlayerMemory playerMemory;

    private void Start()
    {
        spellsHolder = GetComponent<SpellsHolder>();
        playerOrientation = GetComponent<PlayerOrientation>();
        playerMemory = GetComponent<PlayerMemory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectSpell(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SelectSpell(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectSpell(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectSpell(3);
        }
        if (selectedSpell == null) { return; }
        ProcessSelectedSpell();
    }

    private void SelectSpell(int index)
    {
        selectedSpell = spellsHolder.GetSpell(index);
        // No memory == random spells
        if (playerMemory.GetMemoryPercentage() == 0)
        {
            selectedSpell = spellsHolder.RandomSpell();
        }
    }

    private void ProcessSelectedSpell()
    {
        if (selectedSpell is PhysicalSpell physicalSpell)
        {
            Vector3 position = playerOrientation.GetOrientation() * physicalSpell.GetDistance();
            // Instantiate around player
            Spell instantiatedSpell = Instantiate(selectedSpell, transform.position + position, Quaternion.identity);
            instantiatedSpell.Launch(playerMemory.GetMemoryPercentage());
        }
        else if (selectedSpell is TornadoSpell)
        {
            // Instantiate at mouse location
            TornadoSpell instantiatedSpell = (TornadoSpell)Instantiate(selectedSpell, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            instantiatedSpell.Launch(playerMemory.GetMemoryPercentage());
        }
        else // Projectile
        {
            // Instantiate on player + add force
            ProjectileSpell instantiatedSpell = (ProjectileSpell)Instantiate(selectedSpell, transform.position, Quaternion.identity);
            instantiatedSpell.SetOrientation(playerOrientation.GetOrientation());
            instantiatedSpell.Launch(playerMemory.GetMemoryPercentage());
        }
        selectedSpell = null;
    }
}
