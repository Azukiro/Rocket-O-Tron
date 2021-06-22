using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events

/// <summary>
///     When displaying the menu
/// </summary>
public class GameMenuEvent : SDD.Events.Event { }

/// <summary>
///     When the player is playing
/// </summary>
public class GamePlayEvent : SDD.Events.Event { }

/// <summary>
///     When the game is paused
/// </summary>
public class GamePauseEvent : SDD.Events.Event { }

/// <summary>
///     When the game resumed
/// </summary>
public class GameResumeEvent : SDD.Events.Event { }

/// <summary>
///     When the player loosed
/// </summary>
public class GameOverEvent : SDD.Events.Event { }

/// <summary>
///     When the player won
/// </summary>
public class GameVictoryEvent : SDD.Events.Event { }

/// <summary>
///     When the best score changed
/// </summary>
public class GameBestScoreEvent : SDD.Events.Event
{
    /// <summary>
    ///     The current best score for the level
    /// </summary>
    public int _EBestScore { get; set; }
}

/// <summary>
///     When the game statistics changed (to update the HUD)
/// </summary>
public class GameStatisticsChangedEvent : SDD.Events.Event
{
    /// <summary>
    ///     Current score of the player
    /// </summary>
    public int _EScore { get; set; }

    /// <summary>
    ///     Current time of the game
    /// </summary>
    public float _ETime { get; set; }
}

#endregion GameManager Events

#region Player Events

/// <summary>
///     When the player loose a life
/// </summary>
public class GamePlayerLooseLifeEvent : SDD.Events.Event
{
    /// <summary>
    ///     Life of the user
    /// </summary>
    public int _ELife { get; set; }
}

/// <summary>
///     When the player kill an enemy
/// </summary>
public class GamePlayerKillEnemyEvent : SDD.Events.Event { }

/// <summary>
///     When the player enter in the final exit door
/// </summary>
public class GamePlayerInExitDoorEvent : SDD.Events.Event { }

#endregion Player Events

#region MenuManager Events

/// <summary>
///     When the play button is clicked
/// </summary>
public class MenuPlayButtonClickedEvent : SDD.Events.Event
{
}

/// <summary>
///     When the replay button is clicked
/// </summary>
public class MenuRePlayButtonClickedEvent : SDD.Events.Event
{
}

/// <summary>
///     When the menu button is clicked
/// </summary>
public class MenuButtonClickedEvent : SDD.Events.Event
{
}

/// <summary>
///     When the pause button is clicked
/// </summary>
public class MenuPauseButtonClickedEvent : SDD.Events.Event
{
}

/// <summary>
///     When the next level button is clicked
/// </summary>
public class MenuNextLevelButtonClickedEvent : SDD.Events.Event
{
}

/// <summary>
///     When the resume button is clicked
/// </summary>
public class MenuResumeButtonClickedEvent : SDD.Events.Event
{
}

#endregion MenuManager Events