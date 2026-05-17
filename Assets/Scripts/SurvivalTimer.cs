using System;
using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    [SerializeField] private float timeToSurvive = 120f;
    [SerializeField] private TextMeshProUGUI timerText;

    private float currentTime;
    private bool isFinished = false;

    public static event Action OnTimeSurvived;

    private void Start()
    {
        currentTime = timeToSurvive;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (isFinished) return;

        currentTime -= Time.deltaTime;
        
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isFinished = true;
            OnTimeSurvived?.Invoke();
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
