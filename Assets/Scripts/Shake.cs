using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;
    
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(PlayShake());
    }

    IEnumerator PlayShake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
