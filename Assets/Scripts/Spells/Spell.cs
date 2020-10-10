using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected float power = 10f;
    protected float efficacy = 100; // Percentage of memory gauge

    public void SetEfficacy(float percentage)
    {
        efficacy = percentage;
    }
}
