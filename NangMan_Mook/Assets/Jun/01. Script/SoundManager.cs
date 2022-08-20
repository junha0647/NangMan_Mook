using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("���� ���� �ֱ�")][SerializeField] private AudioClip audioJump;
    [Header("������ ���� �� ���� �ֱ�")][SerializeField] private AudioClip audioDamaged;
    [Header("���� ���� ���� �ֱ�")][SerializeField] private AudioClip audioDie;
    [Header("�������� Ŭ���� ���� �ֱ�")][SerializeField] private AudioClip audioFinish;

    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //============ �Ҹ� ============//
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                audioSource.volume = 0.1f;
                audioSource.PlayOneShot(audioJump);
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                audioSource.PlayOneShot(audioDamaged);
                break;
            case "GAMEOVER":
                audioSource.clip = audioDie;
                audioSource.PlayOneShot(audioDie);
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                audioSource.PlayOneShot(audioFinish);
                break;
        }
    }
    //=============================//
}