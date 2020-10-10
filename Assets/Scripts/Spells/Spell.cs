using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected float power = 10f;

    public virtual void Launch(float efficacy) { }
}
