using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] Spell[] spells;
    Spell selectedSpell;

    PlayerOrientation playerOrientation;

    private void Start()
    {
        playerOrientation = GetComponent<PlayerOrientation>();
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
            Instantiate(selectedSpell, transform.position + position, Quaternion.identity);
        }
        else if (selectedSpell is FixedSpell)
        {
            // Instantiate at mouse location
            // TODO add range
            Instantiate(selectedSpell, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
        else // Projectile
        {
            // Instantiate on player + add force
            ProjectileSpell spell = (ProjectileSpell)Instantiate(selectedSpell, transform.position, Quaternion.identity);
            spell.Launch(playerOrientation.GetOrientation());
        }
        selectedSpell = null;
    }
}
