using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Global;

namespace Meta.Audio
{

    public class MusicPlayer : MonoBehaviour
    {

        [Header("Components")]
        [SerializeField] protected AudioSource[] sources;
        [SerializeField] protected AudioClip[] clips;

        [Header("Behaviour")]
        [SerializeField] protected bool playRandom = true;
        [SerializeField] protected bool playOnAwake = true;

        protected float initialVolume;
        protected List<AudioClip> clipsToPlay = new List<AudioClip>();
        protected Coroutine routine;

        protected virtual void Awake()
        {
            initialVolume = sources[0].volume;
            if (playOnAwake)
                Play();
        }

        public virtual void Play()
        {
            if (clipsToPlay.Count == 0)
                clipsToPlay.AddRange(clips);
            AudioClip clip = clipsToPlay[playRandom ? Random.Range(0, clipsToPlay.Count) : 0];
            foreach (AudioSource source in sources)
            {
                source.Play(clip);
            }
            routine = StartCoroutine(CheckForCompletion());
        }

        protected IEnumerator CheckForCompletion()
        {
            while (sources[0].isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }
            Play();
        }

        public virtual void SetVolume(float volume = 0)
        {
            if (volume == 0)
                volume = initialVolume;
            foreach (AudioSource source in sources)
            {
                source.volume = volume;
            }
        }

        public virtual void Pause()
        {
            foreach (AudioSource source in sources)
            {
                source.Pause();
            }
            if (routine != null)
                StopCoroutine(routine);
        }

        public virtual void Resume()
        {
            foreach (AudioSource source in sources)
            {
                source.UnPause();
            }
            routine = StartCoroutine(CheckForCompletion());
        }

    }


}