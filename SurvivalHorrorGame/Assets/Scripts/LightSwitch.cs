using UnityEngine;
using TMPro;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light controlledLight; // Œwiat³o, które prze³¹czamy
    public AudioSource electricBuzz; // DŸwiêk prze³¹cznika/iskrzenia
    public TextMeshProUGUI interactionText; // PodpowiedŸ na ekranie

    private bool isLightOn = false; // Czy œwiat³o jest w³¹czone?

    void Start()
    {
        if (controlledLight == null)
        {
            Debug.LogError("Brak przypisanego œwiat³a w LightSwitch!");
        }

        if (electricBuzz == null)
        {
            Debug.LogError("Brak przypisanego dŸwiêku w LightSwitch!");
        }

        controlledLight.enabled = false; // Œwiat³o zaczyna jako wy³¹czone
        interactionText.text = ""; // Na pocz¹tku brak podpowiedzi
    }

    public string GetInteractionText()
    {
        return isLightOn ? "Naciœnij [E], aby wy³¹czyæ œwiat³o" : "Naciœnij [E], aby w³¹czyæ œwiat³o";
    }

    public void Interact(PlayerInteraction player)
    {
        ToggleLight();
    }

    private void ToggleLight()
    {
        isLightOn = !isLightOn;
        controlledLight.enabled = isLightOn;

        if (electricBuzz != null)
        {
            electricBuzz.Play(); // Odtwarzamy dŸwiêk przy prze³¹czaniu
        }

        interactionText.text = GetInteractionText();
        Debug.Log(isLightOn ? "Œwiat³o W£¥CZONE!" : "Œwiat³o WY£¥CZONE!");
    }
}
