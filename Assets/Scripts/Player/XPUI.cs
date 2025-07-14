using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPUI : MonoBehaviour
{
    public Slider xpSlider;
    public TMP_Text levelText; // ou Text si tu utilises legacy

    void OnEnable()
    {
        PlayerStats.OnXPChanged += UpdateXPBar;
        PlayerStats.OnLevelUp   += UpdateLevelText;
    }

    void OnDisable()
    {
        PlayerStats.OnXPChanged -= UpdateXPBar;
        PlayerStats.OnLevelUp   -= UpdateLevelText;
    }

    void UpdateXPBar(int current, int max)
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = max;
            xpSlider.value    = current;
        }
    }

    void UpdateLevelText(int lvl)
    {
        if (levelText != null)
            levelText.text = $"Lvl {lvl}";
    }
}

