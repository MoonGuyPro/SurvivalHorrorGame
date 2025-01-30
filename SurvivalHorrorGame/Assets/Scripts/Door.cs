using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string requiredItem = "Klucz";
    private Animator animator;
    private AudioSource audioSource; // Dodajemy zmienn� do d�wi�ku
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Pobieramy komponent AudioSource
    }

    public string GetInteractionText()
    {
        return isOpen ? "" : "Otw�rz drzwi [E]";
    }

    public void Interact(PlayerInteraction player)
    {
        if (isOpen) return;

        if (player.HasItem(requiredItem))
        {
            animator.SetTrigger("Open");
            isOpen = true;

            // Odtwarzamy d�wi�k skrzypienia drzwi
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            player.interactionText.text = "Brak klucza!";
        }
    }
}
