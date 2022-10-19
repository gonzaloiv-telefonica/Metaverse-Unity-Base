using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Meta.Global;

namespace Meta.Audio
{

    public class BufferedAudioSource : MonoBehaviour
    {

        [SerializeField] private List<AudioSource> sources;

        public void Play()
        {
            GetValidSource().Play();
        }

        private AudioSource GetValidSource()
        {
            AudioSource result = sources.FirstOrDefault(source => !source.isPlaying);
            if (result == null)
            {
                result = GameObject.Instantiate(sources[0], sources[0].transform.parent);
                sources.Add(result);
            }
            return result;
        }

        public void Play(AudioClip clip)
        {
            GetValidSource().Play(clip);
        }

    }

}