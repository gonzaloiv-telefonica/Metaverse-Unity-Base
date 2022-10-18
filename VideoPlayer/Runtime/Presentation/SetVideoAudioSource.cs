using UnityEngine;
using UnityEngine.Video;

namespace Meta.Video
{

    public class SetVideoAudioSource : MonoBehaviour
    {

        [SerializeField] private VideoPlayer player;
        [SerializeField] private AudioSource source;

        private void Awake()
        {
            player.SetTargetAudioSource(0, source);
        }

    }

}