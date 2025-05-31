using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour,ICollectible

{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Control _playerController;
    [SerializeField] private float _movementIncreaseSpeed;
    public void Collect()
    {
        _playerController.SetMovementSpeed(_movementIncreaseSpeed, _resetBoostDuration);


        Destroy(gameObject);
    }

}
