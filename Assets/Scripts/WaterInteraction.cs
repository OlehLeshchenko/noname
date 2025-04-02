using UnityEngine;

public class WaterInteraction : MonoBehaviour, Interactable
{


    public string GetDescription()
    {
        return "Bucket of cold water";
    }

    public void Interact(PlayerInteraction player)
    {
        player.SetHaveWater();
        Destroy(gameObject);
    }


}
