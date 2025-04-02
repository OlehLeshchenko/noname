using UnityEngine;

public interface Interactable
{
    void Interact(PlayerInteraction player);
    string GetDescription();
}
