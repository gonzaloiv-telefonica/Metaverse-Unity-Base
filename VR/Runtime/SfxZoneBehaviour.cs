using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Meta.Audio;

namespace Meta.VR
{

    public class SfxZoneBehaviour : MonoBehaviour
    {

        [SerializeField] private AudioClip audioClip;
        [SerializeField] private float pitchVariation = 0.5f;
        [SerializeField] private float delayVariation = 0.2f;
        [SerializeField] private float volumeVariation = 0.2f;
        [SerializeField] private float volume = 0.1f;

        private bool isPlaying;

        private void OnTriggerStay(Collider col)
        {
            if (col.gameObject.GetComponent<CharacterController>())
            {
                if (!isPlaying && AvatarController.IsMoving)
                    PlayClip();
            }
        }

        private void PlayClip()
        {
            isPlaying = true;
            PitchedClipData data = PitchedClipData.Create(clip: audioClip, volume: volume)
                .SetRandomPitch(pitchVariation)
                .SetRandomVolume<PitchedClipData>(volumeVariation);
            SfxPlayer.Play(data);
            IEnumerator<float> WaitForClip()
            {
                yield return Timing.WaitForSeconds(audioClip.length);
                yield return Timing.WaitForSeconds(Random.Range(0, delayVariation));
                isPlaying = false;
            }
            Timing.RunCoroutine(WaitForClip());
        }

    }

}