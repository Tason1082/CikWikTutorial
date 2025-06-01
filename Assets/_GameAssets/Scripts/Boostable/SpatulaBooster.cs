using UnityEngine;

public class SpatulaBooster : MonoBehaviour,IBoostable
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;
    [SerializeField] private GameObject _boostParticlesPrefab;

    [Header("Settings")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _boostParticlesDestroyDuration = 2f;

    private bool _isActivated;

   

   

    public void Boost(Control playerController)
    {
        if (_isActivated) { return; }

        PlayBoostAnimation();
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
     
    }
    public void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger("IsSpatulaJumping");
    }
    private void ResetActivation()
    {
        _isActivated = false;
    }

}
