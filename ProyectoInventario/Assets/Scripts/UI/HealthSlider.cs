using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider slider;


    public void SetHealth(float i)
    {
        slider.value = i;
    }
}
