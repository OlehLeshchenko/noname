using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCam;
    public float distance_to_item = 10f;

    public GameObject interaction_UI;
    public TextMeshProUGUI interaction_text;


    private HashSet<string> keys = new HashSet<string>();

    private bool haveWater = false;
    
    void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;
        if (Physics.Raycast(ray, out hit, distance_to_item))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                hitSomething = true;
                interaction_text.text = interactable.GetDescription();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(this);
                }
            }
        }

        interaction_UI.SetActive(hitSomething);
    }

    public void AddKey(string keyColor)
    {
        keys.Add(keyColor);
    }

    public bool HasKey(string keyColor)
    {
        return keys.Contains(keyColor);
    }

    public void SetHaveWater(){
        haveWater=true;
    }

    public bool getHaveWater(){
        return haveWater;
    }
}
