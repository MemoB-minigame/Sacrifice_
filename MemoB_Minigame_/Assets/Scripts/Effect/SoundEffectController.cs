using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    [Header("“Ù–ß∆¨∂Œ")]
    [SerializeField] private AudioSource[] audioSource;
    public enum ACTION { MOVE, ATTACK };

    public void PlaySoundEffect(ACTION act)
    {
        if (0 <= (int)act && (int)act < audioSource.Length)
        {
            if (audioSource[(int)act] is not null)
            {
                audioSource[(int)act].Play();
            }
        }
    }

    public void PlayByIndex(int index)
    {
        foreach(var clip in audioSource){
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
