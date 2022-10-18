using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Meta.Global;
using System.Linq;

namespace Meta.Audio
{

    public class SfxPlayer : MonoBehaviour
    {

        private static SfxPlayer instance;
        public static SfxPlayer Instance => instance ??= FindObjectOfType<SfxPlayer>();

        [SerializeField] private List<AudioSource> leftSources;
        [SerializeField] private List<AudioSource> rightSources;
        private float initialVolume;

        private void Awake()
        {
            Assert.AreNotEqual(leftSources.Count, 0, "A left audio source must be referenced in the editor!");
            Assert.AreNotEqual(rightSources.Count, 0, "A right source must be referenced in the editor!");
            initialVolume = leftSources[0].volume;
        }

        public static void Play(AudioClip clip)
        {
            GetValidSource(Instance.leftSources).Play(clip);
            GetValidSource(Instance.rightSources).Play(clip);
        }

        public static void Play(ClipData data)
        {
            Play(PitchedClipData.Create(data.clip, data.volume));
        }

        public static void Play(PitchedClipData data)
        {
            AudioSource leftSource = GetValidSource(Instance.leftSources);
            leftSource.volume = data.volume;
            leftSource.pitch = data.pitch;
            leftSource.Play(data.clip);
            AudioSource rightSource = GetValidSource(Instance.rightSources);
            rightSource.volume = data.volume;
            rightSource.pitch = data.pitch;
            rightSource.Play(data.clip);
        }

        public static void SetActive(bool isActive)
        {
            Instance.leftSources.ForEach(source => source.volume = isActive ? Instance.initialVolume : 0);
            Instance.rightSources.ForEach(source => source.volume = isActive ? Instance.initialVolume : 0);
        }

        private static AudioSource GetValidSource(List<AudioSource> sources)
        {
            AudioSource result = sources.FirstOrDefault(source => !source.isPlaying);
            if (result == null)
            {
                result = GameObject.Instantiate(sources[0], sources[0].transform.parent);
                sources.Add(result);
            }
            return result;
        }

    }

}

