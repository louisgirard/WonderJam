using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] Spell[] spells;
    Spell selectedSpell;

    PlayerOrientation playerOrientation;
    Memory playerMemory;

    private void Start()
    {
        playerOrientation = GetComponent<PlayerOrientation>();
        playerMemory = GetComponent<Memory>();
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
        if (selectedSpell is PhysicalSpell physicalSpell)
        {
            Vector3 position = playerOrientation.GetOrientation() * physicalSpell.GetDistance();
            // Instantiate around player
            Spell instantiatedSpell = Instantiate(selectedSpell, transform.position + position, Quaternion.identity);
            instantiatedSpell.SetEfficacy(playerMemory.GetMemoryPercentage());
        }
        else if (selectedSpell is TornadoSpell)
        {
            // Instantiate at mouse location
            Spell instantiatedSpell = Instantiate(selectedSpell, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            instantiatedSpell.SetEfficacy(playerMemory.GetMemoryPercentage());
        }
        else // Projectile
        {
            // Instantiate on player + add force
            ProjectileSpell instantiatedSpell = (ProjectileSpell)Instantiate(selectedSpell, transform.position, Quaternion.identity);
            instantiatedSpell.SetEfficacy(playerMemory.GetMemoryPercentage());
            instantiatedSpell.Launch(playerOrientation.GetOrientation());
        }
        selectedSpell = null;
    }
}
