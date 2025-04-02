using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour, Interactable
{
    public Animator m_Animator;
    public bool isOpen = false;
    public bool isLocked = false;
    public string requiredKey;

    public GameObject lock_for_key;
    public bool isFinalDoor = false; 
    public GameObject winText;

    void Start()
    {
        if (isOpen)
        {
            m_Animator.SetBool("IsOpen", true);
        }


        if (winText != null)
        {
            winText.SetActive(false);
        }
    }

    public string GetDescription()
    {
        if (isLocked)
        {
            return "This door is locked! Find the key.";
        }
        return isOpen ? "Press [E] to <color=red>close</color> the door"
                      : "Press [E] to <color=green>open</color> the door";
    }

    public void Interact(PlayerInteraction player)
    {
        if (isLocked)
        {
            if (player.HasKey(requiredKey))
            {
                isLocked = false;
                Destroy(lock_for_key);

            }
            else
            {
                return;
            }
        }


        isOpen = !isOpen;
        m_Animator.SetBool("IsOpen", isOpen);



        if (isFinalDoor && isOpen)
        {
            if (winText != null)
            {
                winText.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
