using System;
using DG.Tweening;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _settingsPopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;
   
   

    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    [Header("Sprites")]
    [SerializeField] private Sprite _musicActiveSprite;
    [SerializeField] private Sprite _musicPassiveSprite;
    [SerializeField] private Sprite _soundActiveSprite;
    [SerializeField] private Sprite _soundPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private Image _blackBackgroundImage;



    [SerializeField] private GameManager _gameManager;

    


    private void Awake()
    {
        Debug.Log("Awake çalýþtý");

        _settingsButton.onClick.AddListener(() =>
        {
            Debug.Log("Butona týklandý!");
        });
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingsPopupObject.transform.localScale = Vector3.zero;

        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
     
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        
    }

  




    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
        _gameManager.ChangeGameState(GameState.Pause);
        _blackBackgroundObject.SetActive(true);
        _settingsPopupObject.SetActive(true);
        _blackBackgroundImage.DOFade(0.8f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(1.5f, _scaleDuration).SetEase(Ease.OutBack);
        
    }

    private void OnResumeButtonClicked()
    { 
      
        
        _blackBackgroundImage.DOFade(0f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(0f, _scaleDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            _gameManager.ChangeGameState(GameState.Resume);
            _settingsPopupObject.SetActive(false);
         
            _blackBackgroundObject.SetActive(false);
        });
    }

    

   

}
