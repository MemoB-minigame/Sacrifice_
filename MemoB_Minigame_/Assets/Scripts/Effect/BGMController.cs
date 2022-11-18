using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [Header("�������֣�BGM��Ƭ��")]
    [SerializeField] private AudioSource[] audioSource;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayByIndex(int index)
    {
        foreach (var clip in audioSource)
        {
            clip.Stop();
        }
        if (0 <= index && index < audioSource.Length)
        {
            if (audioSource[index] is not null)
            {
                audioSource[index].Play();
            }
        }
    }
}
