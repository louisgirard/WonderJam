using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] Spell[] spells;
    Spell selectedSpell;

    PlayerOrientation playerOrientation;
    PlayerMemory playerMemory;

    private void Start()
    {
        playerOrientation = GetComponent<PlayerOrientation>();
        playerMemory = GetComponent<PlayerMemory>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedSpell = spells[0];
        }
        if(Input.GetMouseButtonDown(1))
        {
            selectedSpell = spells[1];
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedSpell = spells[2];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedSpell = spells[3];
        }
        if(selectedSpell == null) { return; }
        ProcessSelectedSpell();
    }

    private void ProcessSelectedSpell()
    {
        // No memory == random spells
        if(playerMemory.GetMemoryPercentage() == 0)
        {
            selectedSpell = spells[Random.Range(0, spells.Length)];
        }

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
