using UnityEngine;
using System.Collections;

public class TriggerMoveObject : MonoBehaviour
{
    public Transform objectToMove; // Obiekt, kt�ry ma si� przesun��
    public Vector3 moveOffset = new Vector3(0, 0, 5); // Jak daleko przesun�� (np. 5m do przodu)
    public float moveDuration = 1f; // Czas trwania animacji (np. 1 sekunda)
    public AudioSource soundEffect; // D�wi�k odtwarzany przy wej�ciu w trigger
    public bool destroyAfterMove = false; // Czy obiekt ma znikn�� po przesuni�ciu?

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) // Sprawdzamy, czy gracz wszed� w trigger
        {
            hasTriggered = true;
            StartCoroutine(MoveAndDisappear());

            // **Odtwarzamy d�wi�k (do ko�ca, bez przerwania)**
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

        // Animacja przesuni�cia obiektu
        while (elapsedTime < moveDuration)
        {
            objectToMove.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.position = targetPosition; // Upewniamy si�, �e obiekt trafi dok�adnie na pozycj� ko�cow�

        // Po zako�czeniu animacji obiekt znika
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
