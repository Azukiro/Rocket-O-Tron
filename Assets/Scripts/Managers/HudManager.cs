using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.UI;

/// This class is used to shox the informations of the HUD Panel
public class HudManager : MonoBehaviour, IEventHandler
{
    /// Score of the player
    [SerializeField] private Text _Score;

    /// Temps de jeu
    [SerializeField] private Text _Time;

    #region Events

    /// <summary>
    ///     Subscribe to all the events
    /// </summary>
    private void OnEnable()
    {
        SubscribeEvents();
    }

    /// <summary>
    ///     Unsubscribe to al the events
    /// </summary>
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    /// <summary>
    ///     Subscribe to the GameStatisticsChanged event in order to get the HUD data
    /// </summary>
    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
    }

    /// <summary>
    ///     Subscribe to the GameStatisticsChanged event
    /// </summary>
    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
    }

    /// <summary>
    ///     Update the HUD text fields
    /// </summary>
    /// <param name="e">The event</param>
    private void GameStatisticsChanged(GameStatisticsChangedEvent e)
    {
        _Score.text = Mathf.Max(0, e._EScore).ToString();
        _Time.text = Mathf.Max(0, e._ETime).ToString("N01");
    }

    #endregion Events
}