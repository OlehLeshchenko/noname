using UnityEngine;
using UnityEngine.SceneManagement;

public class Ufo : Interactable
{
    private PlayerInventory playerInventory;
    private float delay = 3f; 
    private float delayTimer;
    private bool isTriggered = false; 

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (playerInventory == null) return;

        if (!playerInventory.hasBomb)
            promptMessage = "You need to find a bomb!";
        else
            promptMessage = "Use Bomb (Press E)";

        if (isTriggered)
        {
            promptMessage = "Bomb activated! Ship will explode in 3 seconds...";
            delayTimer += Time.deltaTime;
            if (delayTimer >= delay)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("VictoryScene");
            }
        }
    }

    protected override void Interact()
    {
        if (playerInventory.hasBomb && !isTriggered)
        {
            
            isTriggered = true; 
            delayTimer = 0f; 
        }
    }
}
