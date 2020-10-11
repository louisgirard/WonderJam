using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    Image icon;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponentInChildren<Image>();
    }

    public void UpdateIcon(Spell spell, bool disabled)
    {
        if (spell == null)
        {
            icon.sprite = null;
            icon.color = Color.black;
        }
        else if (disabled)
        {
            icon.sprite = spell.icon;
            icon.color = new Color(0.4f,0.4f,0.4f);
        }
        else
        {
            icon.sprite = spell.icon;
            icon.color = Color.white;
        }
    }
}
