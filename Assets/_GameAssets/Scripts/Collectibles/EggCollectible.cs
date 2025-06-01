using UnityEngine;

public class EggCollectible : MonoBehaviour,ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Debug.Log("Egg Collected");
        Destroy(gameObject);
    }
}
