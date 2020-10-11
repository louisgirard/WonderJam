using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected float power = 10f;
    public float timeBetweenCast = 1f;
    public Sprite icon;

    public virtual bool Launch(float efficacy) { return true; }
}
