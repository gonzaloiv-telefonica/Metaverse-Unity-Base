using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.Audio
{

    public class PitchedClipData : ClipData
    {

        public float pitch = 1;

        public static PitchedClipData Create(AudioClip clip, float volume = 1, float pitch = 1)
        {
            return new PitchedClipData(clip, volume, pitch);
        }

        protected PitchedClipData(AudioClip clip, float volume = 1, float pitch = 1) : base(clip, volume)
        {
            this.pitch = pitch;
        }

        public PitchedClipData SetRandomPitch(float ratio)
        {
            pitch -= Random.Range(0, ratio);
            return this;
        }

    }

}