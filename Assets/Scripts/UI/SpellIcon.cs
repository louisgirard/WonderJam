using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    [SerializeField] int index = 0;
    SpellsHolder spellsHolder;
    Image icon;

    // Start is called before the first frame update
    void Start()
    {
        spellsHolder = FindObjectOfType<SpellsHolder>();
        icon = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Spell spell = spellsHolder.GetSpell(index);
        if (spell == null)
        {
            icon.sprite = null;
            icon.color = Color.black;
        }
        else
        {
            icon.sprite = spell.icon;
            icon.color = Color.white;
        }
    }
}
