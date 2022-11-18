using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    [Header("“Ù–ß∆¨∂Œ")]
    [SerializeField] private AudioSource[] audioSource;
    public enum ACTION { MOVE, ATTACK };
    public enum GUN_MOTION { REVOLVER, WINCHESTER, SHOTGUN, RELOAD };

    public void PlaySoundEffect(ACTION action)
    {
        if (0 <= (int)action && (int)action < audioSource.Length)
        {
            if (audioSource[(int)action] is not null)
            {
                audioSource[(int)action].Play();
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
