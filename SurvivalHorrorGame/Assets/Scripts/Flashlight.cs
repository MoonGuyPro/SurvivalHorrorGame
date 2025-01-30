using UnityEngine;
using System.Collections;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight; // Przypisz w inspektorze
    public TextMeshProUGUI flashlightText; // Przypisz TextMeshPro do UI
    public float batteryDuration = 20f; // Czas dzia�ania latarki w sekundach

    private float currentBatteryTime;
    private bool isOn = false;
    private bool isFlickering = false;
    private bool hasFlashlight = false; // Gracz zaczyna bez latarki
    private PlayerInteraction player;

    void Start()
    {
        player = FindObjectOfType<PlayerInteraction>();
        if (flashlightLight == null)
        {
            Debug.LogError("Latarka nie ma przypisanego �wiat�a!");
        }
        if (flashlightText == null)
        {
            Debug.LogError("Brak przypisanego TextMeshProUGUI dla latarki!");
        }

        flashlightLight.enabled = false; // Latarka wy��czona na starcie
        flashlightText.text = "";
    }

    void Update()
    {
        // Gracz nie ma latarki - nie mo�e jej u�ywa�
        if (!hasFlashlight) return;

        // W��czanie i wy��czanie latarki
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn && !isFlickering;
        }

        // Naprawianie latarki (dodanie baterii)
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (player.HasItem("Bateria"))
            {
                player.RemoveFromInventory("Bateria"); // Zu�ycie baterii
                RechargeBattery();
                Debug.Log("Latarka naprawiona!");
            }
            else
            {
                Debug.Log("Brak baterii w ekwipunku!");
            }
        }
    }

    public void EnableFlashlight()
    {
        hasFlashlight = true;
        flashlightLight.enabled = true; // W��cz latark� po podniesieniu
        isOn = true;
        currentBatteryTime = batteryDuration;
        flashlightText.text = ""; // Ukryj komunikat
        StartCoroutine(BatteryTimer());
        Debug.Log("Gracz zdoby� latark�!");
    }

    void RechargeBattery()
    {
        StopAllCoroutines(); // Zatrzymanie migania
        flashlightLight.enabled = true; // W��cz �wiat�o
        isOn = true;
        isFlickering = false;
        flashlightText.text = ""; // Usuwamy komunikat
        currentBatteryTime = batteryDuration; // Resetujemy czas baterii
        StartCoroutine(BatteryTimer()); // Restart timera baterii
    }

    IEnumerator BatteryTimer()
    {
        while (currentBatteryTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentBatteryTime -= 1f;
        }
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        isFlickering = true;

        while (true)
        {
            flashlightLight.enabled = false;
            yield return new WaitForSeconds(2f);
            flashlightLight.enabled = true;
            yield return new WaitForSeconds(1f);

            // Je�li latarka si� roz�aduje, informacja na ekranie
            if (player.HasItem("Bateria"))
            {
                flashlightText.text = " U�yj baterii [U], aby naprawi� latark�!";
            }
            else
            {
                flashlightText.text = " Znajd� inn� latark�, aby zdoby� baterie!";
            }
        }
    }
}
