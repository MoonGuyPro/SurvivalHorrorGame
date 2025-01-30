using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    public string itemName;

    public string GetInteractionText()
    {
        return $"Podnie� {itemName} [E]";
    }

    public void Interact(PlayerInteraction player)
    {
        player.AddToInventory(itemName);
        Destroy(gameObject);
    }
}
