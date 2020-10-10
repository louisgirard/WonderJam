using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] float maxMemory = 10f;
    [SerializeField] float memory;

    private void Start()
    {
        memory = maxMemory;
    }

    public float GetMemoryPercentage()
    {
        return (memory / maxMemory) * 100;
    }

    public void LoseMemory(float damage)
    {
        memory = Mathf.Max(memory - damage, 0);
        print("memory = " + memory);
    }
}
