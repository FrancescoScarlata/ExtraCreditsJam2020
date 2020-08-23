using UnityEngine.Events;

/// <summary>
/// A class to handle events, currently in use for transmitting GameStates.
/// </summary>
public class Events
{
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
}
