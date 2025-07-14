using UnityEngine;
using UnityEngine.UI;
using TMPro;            // nécessaire pour TMP_Text

public class LevelSystem : MonoBehaviour
{
    [Header("Stats de base")]
    public int level = 0;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;

    [Header("Références UI")]
    public Slider xpSlider;     // ta barre d'XP (UI → Slider)
    public TMP_Text levelText;  // ton TextMeshPro (UI → TextMeshPro - Text)

    void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Appelle cette méthode pour ajouter de l'XP.
    /// </summary>
    public void AddXP(float amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            level++;
            // xpToNextLevel *= 1.2f;
        }
        UpdateUI();
    }

    /// <summary>
    /// Met à jour la barre et le texte de niveau.
    /// </summary>
    void UpdateUI()
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
            xpSlider.value    = currentXP;
        }

        if (levelText != null)
        {
            levelText.text = "Lvl " + level;
        }
    }
}

