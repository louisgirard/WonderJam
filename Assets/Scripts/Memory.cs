using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] float maxMemory = 10f;
    float memory;

    private void Start()
    {
        memory = maxMemory;
    }

    private float GetMemory()
    {
        return memory;
    }

    public void LoseMemory(float damage)
    {
        memory = Mathf.Max(memory - damage, 0);
        print("memory = " + memory);
    }
}
