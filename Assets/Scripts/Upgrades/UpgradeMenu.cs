using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Références UI")]
    [Tooltip("Le Panel à activer/désactiver (pas le Canvas lui‑même)")]
    public GameObject panel;

    [Tooltip("Les 3 boutons d'upgrade")]
    public Button[] buttons;

    [Tooltip("Images UI enfants de chaque bouton pour l'icône")]
    public Image[] icons;

    [Tooltip("TextMeshProUGUI enfants de chaque bouton pour le label")]
    public TextMeshProUGUI[] labels;

    [Header("Toutes les upgrades possibles")]
    [Tooltip("Glisse ici tes assets UpgradeSO (Fireball, LifeDrain, Shot…)")]
    public UpgradeSO[] allUpgrades;

    // Options tirées aléatoirement au level up
    private UpgradeSO[] currentOptions;

    void OnEnable()
    {
        Debug.Log("[UpgradeMenu] OnEnable – je m’abonne");
        PlayerStats.OnLevelUp += OnPlayerLevelUp;
    }

    void OnDisable()
    {
        Debug.Log("[UpgradeMenu] OnDisable – je me désabonne");
        PlayerStats.OnLevelUp -= OnPlayerLevelUp;
    }

    void Start()
    {
        // Masque le panel au démarrage
        panel.SetActive(false);
    }

    /// <summary>
    /// Callback lorsqu'on reçoit l'événement de montée de niveau
    /// </summary>
    void OnPlayerLevelUp(int lvl)
    {
        Debug.Log($"[UpgradeMenu] Niveau {lvl} reçu : j’affiche le menu");
        Show();
    }

    /// <summary>
    /// Affiche le menu d'upgrades avec 3 choix aléatoires
    /// </summary>
    public void Show()
    {
        // Tire jusqu'à buttons.Length upgrades parmi toutes
        currentOptions = allUpgrades
            .OrderBy(u => Random.value)
            .Take(buttons.Length)
            .ToArray();

        // Debug : affiche la taille de tous nos tableaux
        Debug.Log($"[UpgradeMenu] lengths ⇒ buttons:{buttons.Length}, icons:{icons.Length}, labels:{labels.Length}, allUpgrades:{allUpgrades.Length}, options:{currentOptions.Length}");

        // Nombre d'éléments à remplir
        int count = Mathf.Min(buttons.Length, currentOptions.Length, icons.Length, labels.Length);

        // Remplissage des boutons existants
        for (int i = 0; i < count; i++)
        {
            var so = currentOptions[i];
            icons[i].sprite = so.icon;
            labels[i].text  = so.upgradeName;

            buttons[i].onClick.RemoveAllListeners();
            int idx = i;
            buttons[i].onClick.AddListener(() => OnSelect(idx));
            buttons[i].gameObject.SetActive(true);
        }

        // Masque les boutons en trop (au cas où)
        for (int i = count; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        // Affiche le panel et met le jeu en pause
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Appelé quand l'utilisateur clique sur un des boutons
    /// </summary>
    void OnSelect(int index)
    {
        var chosen = currentOptions[index];
        Debug.Log($"[UpgradeMenu] Upgrade sélectionnée : {chosen.upgradeName}");

        // Applique l'upgrade au joueur
        PlayerAbilityManager.instance.ApplyUpgrade(chosen);

        // Ferme le menu et reprend le jeu
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}

