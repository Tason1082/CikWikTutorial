using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _playerAnimator;

    [Header("Settings")]
    [SerializeField] private float _eggRotationDuration;

    private Vector3 _eggRotationVector = new Vector3(0f, 360f, 0f);
    [SerializeField] private GameManager _gameManager;
    private Control _playerController;
    private StateController _stateController;

    private void Awake()
    {
        

        _stateController = GetComponent<StateController>(); 
        _playerController = GetComponent<Control>();
    }

    private void Start()
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }


    private void Update()
    {
        if (_gameManager.GetCurrentGameState() != GameState.Play
           && _gameManager.GetCurrentGameState() != GameState.Resume) { return; }

        SetPlayerAnimations();
    }

    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    private void SetPlayerAnimations()
    {
        var currentState = _stateController.GetCurrentState();

        switch (currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;
            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;
            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;
            case PlayerState.Slide:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }

}
