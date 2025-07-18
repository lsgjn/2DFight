using UnityEngine;
using UnityEngine.UI;

public class GuardSliderUI : MonoBehaviour
{
    public Slider guardSlider;
    private int maxGuard = 3;
    private int currentGuard;

    void Start()
    {
        currentGuard = maxGuard;
        guardSlider.maxValue = maxGuard;
        guardSlider.wholeNumbers = true;
        guardSlider.value = currentGuard;
    }

    public void UseGuard()
    {
        if (currentGuard <= 0) return;

        currentGuard--;
        guardSlider.value = currentGuard;
    }

    public void RestoreGuard(int amount = 1)
    {
        currentGuard = Mathf.Min(maxGuard, currentGuard + amount);
        guardSlider.value = currentGuard;
    }
}
