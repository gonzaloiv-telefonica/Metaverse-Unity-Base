using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Audio
{

    [System.Serializable]
    public class ClipData
    {

        public AudioClip clip;
        public float volume = 1;

        public bool HasClip => clip != null;

        public static ClipData Create(AudioClip clip, float volume = 1)
        {
            return new ClipData(clip, volume);
        }

        protected ClipData(AudioClip clip, float volume = 1)
        {
            this.clip = clip;
            this.volume = volume;
        }

        public T SetRandomVolume<T>(float ratio) where T : ClipData
        {
            volume += Random.Range(-ratio, ratio);
            return (T)this;
        }

    }

}