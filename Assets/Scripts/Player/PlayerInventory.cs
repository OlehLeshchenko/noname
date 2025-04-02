using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasBomb;
    void Start()
    {
        hasBomb = false;
    }

    public void TakeBomb()
    {
        hasBomb = true;
    }

}
