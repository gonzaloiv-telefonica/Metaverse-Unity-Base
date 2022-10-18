using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;

namespace Meta.Audio
{

    public class AudioSample : MonoBehaviour
    {

        [SerializeField] private AudioSource[] sources;
        [SerializeField] private AudioClip clip;

        private AudioSource source;

        private void Start()
        {
            PlayClip();
        }

        private void PlayClip()
        {
            source = GetRandomSource();
            source.Play(clip);
            StartCoroutine(WaitForCompletion());
        }

        private AudioSource GetRandomSource()
        {
            return sources[Random.Range(0, sources.Length)];
        }

        private IEnumerator WaitForCompletion()
        {
            while (source.isPlaying && source.time < clip.length)
                yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);
            PlayClip();
        }

    }

}