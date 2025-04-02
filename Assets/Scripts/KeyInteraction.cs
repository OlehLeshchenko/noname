using UnityEngine;

public class KeyPickup : MonoBehaviour, Interactable
{
    public string keyColor;

    public bool isHot=false;
    public Light fire;

    public string GetDescription()
    {
        if(isHot){
            return "This key is really hot! I can't take it.";
        }
        return $"This is a <color={keyColor}>{keyColor} key</color>";

    }

    public void Interact(PlayerInteraction player)
    {
        if(!isHot){
            player.AddKey(keyColor);
            Destroy(gameObject);
        }
        else{
            if(player.getHaveWater()){
                fire.enabled = false;
                isHot=false;
            }
        }
    }


}
