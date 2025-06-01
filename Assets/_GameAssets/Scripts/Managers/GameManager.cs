using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _maxEggCount = 5;
    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;

        if (_currentEggCount == _maxEggCount)
        {
            Debug.Log("You Win! All eggs collected.");
            // Buraya kazanma durumunu tetikleyen kod eklenebilir
        }
    }
}

