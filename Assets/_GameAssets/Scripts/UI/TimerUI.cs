using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotateableTransform;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]
 

    private Vector3 _rotationVector = new Vector3(0, 0, -360f);
    private float _elapsedTime;
    private bool _isTimerRunning;
    private string _finalTime;
    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private Ease _rotationEase = Ease.Linear;

    private Tween _rotationTween;

    [SerializeField] private GameManager _gameManager;





    private void Start()
    {

        _gameManager.OnGameStateChanged += OnGameStateChanged;
        PlayRotationAnimation();
        StartTimer();
    }


    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Play:
                StartTimer();
                PlayRotationAnimation();
                break;
            case GameState.Pause:
                StopTimer();
                break;
            case GameState.Resume:
                ResumeTimer();
                break;
            case GameState.GameOver:
                FinishTimer();
                break;
        }

      
    }

    private void StartTimer()
    {
        if (_isTimerRunning)
            return;

        _elapsedTime = 0f;
        _isTimerRunning = true;
        CancelInvoke(nameof(UpdateTimerUI)); // eklenmeli
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
    }

    private void ResumeTimer()
    {
        if (!_isTimerRunning)
        {
            _isTimerRunning = true;
            CancelInvoke(nameof(UpdateTimerUI)); // eklenmeli
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            _rotationTween?.Play();
        }
    }


    private void StopTimer()
{
    _isTimerRunning = false;
    CancelInvoke(nameof(UpdateTimerUI));
    _rotationTween.Pause();
}



private void FinishTimer()
{
    StopTimer();
    _finalTime = GetFormattedElapsedTime();
}

private void PlayRotationAnimation()
    {
        _rotationTween = _timerRotateableTransform.DORotate(_rotationVector, _rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(_rotationEase);
    }




    private string GetFormattedElapsedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateTimerUI()
    {
        if (!_isTimerRunning)
            return;

        _elapsedTime += 1f;

        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetFinalTime()
    {
        return _finalTime;
    }

   
}
