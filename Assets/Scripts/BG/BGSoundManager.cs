using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundState
{
    Silent,
    Background,
    MonsterHit
}

public class BGSoundManager : MonoBehaviour
{
    public static BGSoundManager Instance;
    public float detectionRange = 5f;
    public Transform[] monsters;

    [Header("Audio Clips")]
    public AudioClip normalBG;
    public AudioClip battleBG;

    private AudioSource audioSource;
    private bool isMonsterNearby = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        bool anyMonsterNearby = false;

        foreach (Transform monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.position);
            if (distance <= detectionRange)
            {
                anyMonsterNearby = true;
                break; 
            }
        }

        if (anyMonsterNearby)
        {
            isMonsterNearby = true;
            PlayBG(SoundState.MonsterHit);
        }
        else if (!anyMonsterNearby)
        {
            isMonsterNearby = false;
            PlayBG(SoundState.Background);
        }
    }

    public void PlayBG(SoundState state)
    {
        switch (state)
        {
            case SoundState.Silent:
                audioSource.Stop();
                break;
            case SoundState.Background:
                if (audioSource.clip != normalBG)
                {
                    audioSource.clip = normalBG;
                    audioSource.Play();
                }
                break;
            case SoundState.MonsterHit:
                if (audioSource.clip != battleBG)
                {
                    audioSource.clip = battleBG;
                    audioSource.Play();
                }
                break;
        }
    }
}
