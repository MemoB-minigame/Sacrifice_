using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [Header("±≥æ∞“Ù¿÷£®BGM£©∆¨∂Œ")]
    [SerializeField] private AudioSource[] audioSource;
    private int activeIndex = -1;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayByIndex(int index)
    {
        if (activeIndex != -1)
        {
            audioSource[activeIndex].Stop();
            activeIndex = -1;
        }
        if (0 <= index && index < audioSource.Length)
        {
            if (audioSource[index] is not null)
            {
                audioSource[index].Play();
                activeIndex = index;
            }
        }
    }

    public AudioSource GetActiveAudioSource()
    {
        if (activeIndex == -1) return null;
        return audioSource[activeIndex];
    }

    public int GetActiveAudioSourceIndex()
    {
        return activeIndex;
    }
}
