using UnityEngine;

public class PhysicalSpell : Spell
{
    [SerializeField] float distance = 0.5f;

    public float GetDistance()
    {
        return distance;
    }
}
