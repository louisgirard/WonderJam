using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    Slider slider;
    Text text;

    void Awake()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
    }

    public void UpdateSlider(float value, float maxValue)
    {
        slider.value = value / maxValue;
        text.text = value + " / " + maxValue;
    }
}
