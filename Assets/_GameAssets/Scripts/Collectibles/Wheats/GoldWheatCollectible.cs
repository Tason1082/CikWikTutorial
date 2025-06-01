using UnityEngine;
using UnityEngine.UI; // <-- Image için gerekli

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Control _playerController;
    [SerializeField] private float _movementIncreaseSpeed;

    // Eksik olan alanlar:
    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    [SerializeField] private PlayerStateUI _playerStateUI;

    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _passiveWheatSprite;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _passiveSprite;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
        InitializeSO();
    }

    public void InitializeSO()
    {
        
        
        _resetBoostDuration = _wheatDesignSO.ResetBoostDuration;
 
        _activeSprite = _wheatDesignSO.ActiveSprite;
        _passiveSprite = _wheatDesignSO.PassiveSprite;
        _activeWheatSprite = _wheatDesignSO.ActiveWheatSprite;
        _passiveWheatSprite = _wheatDesignSO.PassiveWheatSprite;
    }
    public void Collect()
    {
        _playerController.SetMovementSpeed(_movementIncreaseSpeed, _resetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(
      _playerBoosterTransform,
      _playerBoosterImage,
      _playerStateUI.GetGoldBoosterWheatImage,
      _activeSprite,
      _passiveSprite,
      _activeWheatSprite,
      _passiveWheatSprite,
      _resetBoostDuration
  );


        Destroy(gameObject);
    }
}
