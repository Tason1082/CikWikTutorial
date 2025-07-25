using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _maxEggCount = 5;
    private int _currentEggCount;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private GameManager _gameManager;
    public event Action<GameState> OnGameStateChanged;
    private GameState _currentGameState;
   

    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);
        

        if (_currentEggCount == _maxEggCount)
        {
            
            
            _eggCounterUI.SetEggCompleted();
            
        }
    }
    private void Start()
    {
        ChangeGameState(GameState.Play);
    }


    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log($"Game State: {gameState}");
    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}

