using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.UI;

public class HudManager : MonoBehaviour, IEventHandler
{
    // Score of the player
    [SerializeField] private Text m_Score;

    // Temps de jeu
    [SerializeField] private Text m_Time;

    #region Events

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
    }

    // Update HUD Text Fields
    private void GameStatisticsChanged(GameStatisticsChangedEvent e)
    {
        m_Score.text = Mathf.Max(0, e.eScore).ToString();
        m_Time.text = Mathf.Max(0, e.eTime).ToString("N01");
    }

    #endregion Events
}