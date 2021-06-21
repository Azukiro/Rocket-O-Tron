using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events

// Raised by Game Manager
public class GameMenuEvent : SDD.Events.Event { }

public class GamePlayEvent : SDD.Events.Event { }

public class GamePauseEvent : SDD.Events.Event { }

public class GameResumeEvent : SDD.Events.Event { }

public class GameOverEvent : SDD.Events.Event { }

public class GameVictoryEvent : SDD.Events.Event { }

public class GameStatisticsChangedEvent : SDD.Events.Event
{
    public int eScore { get; set; }

    public int eLife { get; set; }

    public float eTime { get; set; }
}

#endregion GameManager Events

#region Player Events

// Raised by Player
public class GamePlayerLooseLifeEvent : SDD.Events.Event
{
    public int eLife { get; set; }
}

public class GamePlayerKillEnnemyEvent : SDD.Events.Event { }

public class GamePlayerInExitDoorEvent : SDD.Events.Event { }

#endregion Player Events

#region MenuManager Events

// Raised by Menu Manager
public class MenuPlayButtonClickedEvent : SDD.Events.Event
{
}

public class MenuRePlayButtonClickedEvent : SDD.Events.Event
{
}

public class MenuButtonClickedEvent : SDD.Events.Event
{
}

public class MenuPauseButtonClickedEvent : SDD.Events.Event
{
}

public class MenuNextLevelButtonClickedEvent : SDD.Events.Event
{
}

public class MenuResumeButtonClickedEvent : SDD.Events.Event
{
}

#endregion MenuManager Events