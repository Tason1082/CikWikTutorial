using UnityEngine;

public class HollyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Control _playerController;
    [SerializeField] private float _forceIncrease;
    public void Collect()
    {
        _playerController.SetMovementSpeed(_forceIncrease, _resetBoostDuration);


        Destroy(gameObject);
    }
}
