using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
        }
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}
