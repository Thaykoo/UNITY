using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Références UI")]
    public GameObject panel;
    public Button[] buttons;
    public Image[] icons;
    public TextMeshProUGUI[] labels;

    [Header("Toutes les upgrades possibles")]
    public UpgradeSO[] allUpgrades;

    private UpgradeSO[] currentOptions;

    void OnEnable()
    {
        PlayerStats.OnLevelUp += OnPlayerLevelUp;
    }

    void OnDisable()
    {
        PlayerStats.OnLevelUp -= OnPlayerLevelUp;
    }

    void Start()
    {
        panel.SetActive(false);
    }

    void OnPlayerLevelUp(int lvl)
    {
        Show();
    }

    public void Show()
    {
        currentOptions = allUpgrades
            .OrderBy(u => Random.value)
            .Take(buttons.Length)
            .ToArray();

        int count = Mathf.Min(buttons.Length, currentOptions.Length, icons.Length, labels.Length);

        for (int i = 0; i < count; i++)
        {
            var so = currentOptions[i];
            icons[i].sprite = so.icon;

            int lvl = PlayerAbilityManager.instance.GetUpgradeLevel(so.type);
            if (lvl == 0) lvl = 1;
            labels[i].text = $"{so.upgradeName} (Lv {lvl})";

            buttons[i].onClick.RemoveAllListeners();
            int idx = i;
            buttons[i].onClick.AddListener(() => OnSelect(idx));
            buttons[i].gameObject.SetActive(true);
        }

        for (int i = count; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);

        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    void OnSelect(int index)
    {
        var so = currentOptions[index];
        PlayerAbilityManager.instance.ApplyUpgrade(so.type);

        // Mise à jour immédiate du label
        int newLvl = PlayerAbilityManager.instance.GetUpgradeLevel(so.type);
        labels[index].text = $"{so.upgradeName} (Lv {newLvl})";

        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}

