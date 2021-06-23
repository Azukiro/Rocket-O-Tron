using SDD.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour, IEventHandler
{
    [SerializeField] private Text _BestScore;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameBestScoreEvent>(GameBestScore);
    }

    public void UnsubscribeEvents()
    {
        Debug.Log("UnsubscribeEvents");
        EventManager.Instance.RemoveListener<GameBestScoreEvent>(GameBestScore);
    }

    private void GameBestScore(GameBestScoreEvent e)
    {
        _BestScore.text = e._EBestScore.ToString();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}