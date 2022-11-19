using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    [Header("“Ù–ß∆¨∂Œ")]
    [SerializeField] private AudioSource[] audioSource;
    public enum ACTION { MOVE, ATTACK };
    public enum GUN_MOTION { REVOLVER, WINCHESTER, SHOTGUN, RELOAD };

    private int lastAudioSource = -1;

    public void PlaySoundEffect(ACTION action)
    {
        if (0 <= (int)action && (int)action < audioSource.Length)
        {
            if (audioSource[(int)action] is not null)
            {
                audioSource[(int)action].Play();
                lastAudioSource = (int)action;
            }
        }
    }

    public void PlaySoundEffect(GUN_MOTION motion)
    {
        if (0 <= (int)motion && (int)motion < audioSource.Length)
        {
            if (audioSource[(int)motion] is not null)
            {
                audioSource[(int)motion].Play();
                lastAudioSource = (int)motion;
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
                lastAudioSource = index;
            }
        }
    }

    public AudioSource GetLastAudioSource()
    {
        if (lastAudioSource == -1) return null;
        return audioSource[lastAudioSource];
    }

    public int GetLastAudioSourceIndex()
    {
        return lastAudioSource;
    }
}
