using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioContent : MonoBehaviour {
    
    public TextMeshProUGUI label;
    public AudioClip clip;
    public AudioSource audioSource;
    [SerializeField]
    Button button;

    public void Setup()
    {
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
