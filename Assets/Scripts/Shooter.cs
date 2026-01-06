using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    
    [Header("AI - CHECK THIS FOR ENEMIES ONLY!")]
    [SerializeField] bool useAI = false;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minRateVariance = 0.1f;
    
    Coroutine firingCoroutine;
    
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        // CRITICAL: Stop any firing that might have started
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
        Debug.Log($"{gameObject.name}: useAI = {useAI}");
        
        // ONLY start auto-firing if useAI is TRUE
        if (useAI == true)
        {
            Debug.Log($"{gameObject.name}: AI enabled - auto-firing");
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else
        {
            Debug.Log($"{gameObject.name}: Player mode - manual firing only");
        }
    }
    
    void Update()
    {
        // CRITICAL: Only handle input if useAI is FALSE
        if (useAI == false)
        {
            HandlePlayerInput();
        }
    }

    void HandlePlayerInput()
    {
        bool firePressed = false;
        
        // Check Space key
        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                firePressed = true;
            }
        }
        
        // Check Left Mouse Button
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                firePressed = true;
            }
        }

        // Start firing when button is held
        if (firePressed)
        {
            if (firingCoroutine == null)
            {
                Debug.Log($"{gameObject.name}: FIRE STARTED by player input");
                firingCoroutine = StartCoroutine(FireContinuously());
            }
        }
        else
        {
            // Stop firing when button is released
            if (firingCoroutine != null)
            {
                Debug.Log($"{gameObject.name}: FIRE STOPPED by player release");
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (projectilePrefab == null)
            {
                Debug.LogError($"{gameObject.name}: Projectile Prefab is not assigned!");
                yield break;
            }
            
            GameObject instance = Instantiate(projectilePrefab, 
                transform.position, 
                Quaternion.identity);
            
            Debug.Log($"{gameObject.name}: Fired projectile at {transform.position}");
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // For AI enemies, shoot downward. For player, shoot upward.
                Vector2 direction = useAI ? Vector2.down : transform.up;
                rb.linearVelocity = direction * projectileSpeed;
                
                Debug.Log($"{gameObject.name}: Projectile moving in direction {direction} at speed {projectileSpeed}");
            }
            else
            {
                Debug.LogWarning($"{gameObject.name}: Projectile missing Rigidbody2D!");
            }
            
            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = baseFiringRate;
            
            if (useAI)
            {
                timeToNextProjectile = Random.Range(
                    baseFiringRate - firingRateVariance, 
                    baseFiringRate + firingRateVariance);
            }
            
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minRateVariance, float.MaxValue);

            audioPlayer.PlayShootingClip();
                
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
    
    void OnDisable()
    {
        // Stop firing when disabled
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
}