using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        Core.OnCoreHealthChanged += UpdateHealthUI;
        Core.OnCoreDestroyed += ShowGameOverUI;
    }

    private void OnDisable()
    {
        Core.OnCoreHealthChanged -= UpdateHealthUI;
        Core.OnCoreDestroyed -= ShowGameOverUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Ã‡ekirdek BÃ¼tÃ¼nlÃ¼ÄŸÃ¼: " + currentHealth + " / " + maxHealth;
        }
    }

    private void ShowGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
