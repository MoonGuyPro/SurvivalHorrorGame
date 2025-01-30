using UnityEngine;
using System.Collections;

public class TriggerMoveObject : MonoBehaviour
{
    public Transform objectToMove; // Obiekt, który ma siê przesun¹æ
    public Vector3 moveOffset = new Vector3(0, 0, 5); // Jak daleko przesun¹æ (np. 5m do przodu)
    public float moveDuration = 1f; // Czas trwania animacji (np. 1 sekunda)
    public AudioSource soundEffect; // DŸwiêk odtwarzany przy wejœciu w trigger
    public bool destroyAfterMove = false; // Czy obiekt ma znikn¹æ po przesuniêciu?

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) // Sprawdzamy, czy gracz wszed³ w trigger
        {
            hasTriggered = true;
            StartCoroutine(MoveAndDisappear());

            // **Odtwarzamy dŸwiêk (do koñca, bez przerwania)**
            if (soundEffect != null && !soundEffect.isPlaying)
            {
                soundEffect.Play();
            }
        }
    }

    IEnumerator MoveAndDisappear()
    {
        if (objectToMove == null) yield break;

        Vector3 startPosition = objectToMove.position;
        Vector3 targetPosition = startPosition + moveOffset;
        float elapsedTime = 0f;

        // Animacja przesuniêcia obiektu
        while (elapsedTime < moveDuration)
        {
            objectToMove.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.position = targetPosition; // Upewniamy siê, ¿e obiekt trafi dok³adnie na pozycjê koñcow¹

        // Po zakoñczeniu animacji obiekt znika
        if (destroyAfterMove)
        {
            Destroy(objectToMove.gameObject); // Usuwa obiekt z gry
        }
        else
        {
            objectToMove.gameObject.SetActive(false); // Ukrywa obiekt
        }
    }
}
