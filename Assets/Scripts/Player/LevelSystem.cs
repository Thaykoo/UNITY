using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    [Header("Stats de base")]
    public int level = 0;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;

    [Header("Références UI")]
    public Slider xpSlider;
    public TMP_Text levelText;

    void Start()
    {
        UpdateUI();
    }

    public void AddXP(float amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            level++;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value = currentXP;
        }

        if (levelText != null)
            levelText.text = "Lvl " + level;
    }
}

