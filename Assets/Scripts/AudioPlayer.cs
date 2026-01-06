using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;

    [SerializeField] [Range(0f, 1f)] private float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;

    [SerializeField] [Range(0f, 1f)] private float damageVolume = 1f;
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }
    
    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
