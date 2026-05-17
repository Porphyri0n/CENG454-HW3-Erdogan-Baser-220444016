using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverPanel;

    // Ödev belgesindeki "Lifecycle" uyarýsýna istinaden abonelikler
    // OnEnable ve OnDisable içinde kesinlikle yönetilmelidir.
    private void OnEnable()
    {
        Core.OnCoreHealthChanged += UpdateHealthUI;
        Core.OnCoreDestroyed += ShowGameOverUI;
    }

    private void OnDisable()
    {
        // Hayalet abonelikleri (Ghost subscribers) önlemek için abonelikten çýkýyoruz
        Core.OnCoreHealthChanged -= UpdateHealthUI;
        Core.OnCoreDestroyed -= ShowGameOverUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"Çekirdek Bütünlüðü: {currentHealth} / {maxHealth}";
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