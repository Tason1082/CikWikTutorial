using UnityEngine;


[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject
        

{
      [SerializeField] private float resetBoostDuration;
[SerializeField] private float _increaseDecreaseMultiplier;


public float ResetBoostDuration => resetBoostDuration;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
}
