using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] Projectile fireballPrefab;
    [SerializeField] Projectile energyPrefab;

    PlayerOrientation playerOrientation;

    Projectile selectedSpell;

    private void Start()
    {
        playerOrientation = GetComponent<PlayerOrientation>();
        selectedSpell = energyPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Projectile spell = Instantiate(selectedSpell, transform.position, Quaternion.identity);
            spell.Cast(playerOrientation.GetOrientation());
        }
    }
}
