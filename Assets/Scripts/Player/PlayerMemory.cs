using UnityEngine;

public class PlayerMemory : MonoBehaviour
{
    [SerializeField] float maxMemory = 10f;
    [SerializeField] float timeBetweenLost = 2f;
    float memory;
    float timer;

    MemoryBar memoryBar;

    private void Start()
    {
        memory = maxMemory;
        timer = timeBetweenLost;
        memoryBar = FindObjectOfType<MemoryBar>();
        memoryBar.UpdateSlider(memory, maxMemory);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            LoseMemory();
            timer = timeBetweenLost;
        }
    }

    public float GetMemoryPercentage()
    {
        return (memory / maxMemory) * 100;
    }

    private void LoseMemory()
    {
        memory = Mathf.Max(memory - 1f, 0);
        memoryBar.UpdateSlider(memory, maxMemory);
    }

    public bool CanHeal()
    {
        return memory != maxMemory;
    }

    public void Heal(float points)
    {
        memory = Mathf.Min(memory + points, maxMemory);
        memoryBar.UpdateSlider(memory, maxMemory);
    }
}
