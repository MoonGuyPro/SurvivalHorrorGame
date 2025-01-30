using UnityEngine;
using TMPro;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light controlledLight; // �wiat�o, kt�re prze��czamy
    public AudioSource electricBuzz; // D�wi�k prze��cznika/iskrzenia
    public TextMeshProUGUI interactionText; // Podpowied� na ekranie

    private bool isLightOn = false; // Czy �wiat�o jest w��czone?

    void Start()
    {
        if (controlledLight == null)
        {
            Debug.LogError("Brak przypisanego �wiat�a w LightSwitch!");
        }

        if (electricBuzz == null)
        {
            Debug.LogError("Brak przypisanego d�wi�ku w LightSwitch!");
        }

        controlledLight.enabled = false; // �wiat�o zaczyna jako wy��czone
        interactionText.text = ""; // Na pocz�tku brak podpowiedzi
    }

    public string GetInteractionText()
    {
        return isLightOn ? "Naci�nij [E], aby wy��czy� �wiat�o" : "Naci�nij [E], aby w��czy� �wiat�o";
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
            electricBuzz.Play(); // Odtwarzamy d�wi�k przy prze��czaniu
        }

        interactionText.text = GetInteractionText();
        Debug.Log(isLightOn ? "�wiat�o W��CZONE!" : "�wiat�o WY��CZONE!");
    }
}
