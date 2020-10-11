using UnityEngine;

public class SpellIcons : MonoBehaviour
{
    [SerializeField] SpellIcon[] icons = null;
    readonly bool[] disabledIcons = new bool[] { false, false, false, false };
    SpellsHolder spellsHolder;

    // Start is called before the first frame update
    void Start()
    {
        spellsHolder = FindObjectOfType<SpellsHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < icons.Length; i++)
        {
            icons[i].UpdateIcon(spellsHolder.GetSpell(i), disabledIcons[i]);
        }
    }

    public void DisableIcon(int index)
    {
        disabledIcons[index] = true;
    }

    public void EnableIcon(int index)
    {
        disabledIcons[index] = false;
    }
}
