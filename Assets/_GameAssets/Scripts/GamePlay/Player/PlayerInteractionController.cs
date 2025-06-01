using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private Control _playerController;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerController = GetComponent<Control>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
         {
        collectible.Collect();
    }
}
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
           
        }
      
    }
}
