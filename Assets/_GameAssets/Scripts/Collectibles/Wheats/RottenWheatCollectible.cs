using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Control _playerController;
    [SerializeField] private float _movementDecreaseSpeed;
    public void Collect()
    {
        _playerController.SetMovementSpeed(_movementDecreaseSpeed, _resetBoostDuration);


        Destroy(gameObject);
    }
}
