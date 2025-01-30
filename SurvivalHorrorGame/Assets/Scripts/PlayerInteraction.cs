using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 3f;
    public TextMeshProUGUI interactionText; // Przypisz w inspektorze
    private Camera playerCamera;
    private Dictionary<string, bool> inventory = new Dictionary<string, bool>();
    private Flashlight flashlight;

    void Start()
    {
        playerCamera = Camera.main;
        flashlight = FindObjectOfType<Flashlight>();

        if (interactionText == null)
        {
            Debug.LogError("Brak przypisanego TextMeshProUGUI w PlayerInteraction!");
        }
        interactionText.text = "";
    }

    void Update()
    {
        CheckForInteraction();
    }

    void CheckForInteraction()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactionText.text = interactable.GetInteractionText();

                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    interactable.Interact(this);
                }
                return;
            }
        }
        interactionText.text = "";
    }

    public void AddToInventory(string itemName)
    {
        if (!inventory.ContainsKey(itemName))
        {
            inventory[itemName] = true;
            Debug.Log($"Dodano do ekwipunku: {itemName}");

            // Sprawdzenie, czy gracz podniós³ latarkê i aktywowanie jej
            if (itemName == "Latarka" && flashlight != null)
            {
                flashlight.EnableFlashlight();
            }
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName) && inventory[itemName];
    }

    public void RemoveFromInventory(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"Usuniêto z ekwipunku: {itemName}");
        }
    }
}
