using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;

    private void OnEnable()
    {
        Core.OnCoreHealthChanged += UpdateHealthUI;
        Core.OnCoreDestroyed += ShowGameOverUI;
        SurvivalTimer.OnTimeSurvived += ShowGameWinUI;
    }

    private void OnDisable()
    {
        Core.OnCoreHealthChanged -= UpdateHealthUI;
        Core.OnCoreDestroyed -= ShowGameOverUI;
        SurvivalTimer.OnTimeSurvived -= ShowGameWinUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Core Integrity: " + currentHealth + " / " + maxHealth;
        }
    }

    private void ShowGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void ShowGameWinUI()
    {
        if (gameWinPanel != null)
        {
            gameWinPanel.SetActive(true);
        }
    }
}
