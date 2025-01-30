using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    public Light lampLight; // Przypisz tutaj �wiat�o (Spot/Point Light)
    public AudioSource electricBuzz; // Przypisz d�wi�k iskrzenia (AudioSource)

    public float minFlickerTime = 0.05f; // Minimalny czas migotania
    public float maxFlickerTime = 0.2f;  // Maksymalny czas migotania
    public float offTime = 3f; // Czas ca�kowitego zgaszenia
    public float offChance = 0.1f; // 10% szansy na pe�ne wy��czenie
    public float sparkChance = 0.3f; // 30% szansy na efekt iskrzenia

    private bool isFlickering = false;

    void Start()
    {
        if (lampLight == null)
        {
            lampLight = GetComponentInChildren<Light>();
        }
        if (electricBuzz == null)
        {
            electricBuzz = GetComponent<AudioSource>();
        }

        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            // Czy lampa ca�kowicie zga�nie?
            if (Random.value < offChance)
            {
                lampLight.enabled = false;
                yield return new WaitForSeconds(offTime);
            }

            // Migotanie �wiat�a
            float flickerDuration = Random.Range(minFlickerTime, maxFlickerTime);
            lampLight.enabled = false;
            yield return new WaitForSeconds(flickerDuration);

            lampLight.enabled = true;
            yield return new WaitForSeconds(flickerDuration);

            // Efekt iskrzenia (d�wi�k + lekkie zgaszenie)
            if (electricBuzz != null && Random.value < sparkChance)
            {
                electricBuzz.Play();
                lampLight.enabled = false;
                yield return new WaitForSeconds(0.1f);
                lampLight.enabled = true;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 2f)); // Przerwa mi�dzy migotaniami
        }
    }
}
