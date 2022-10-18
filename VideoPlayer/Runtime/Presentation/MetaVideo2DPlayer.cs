using System;
using UnityEngine;
using UnityEngine.UI;


namespace Meta.Video
{
    public class MetaVideo2DPlayer : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.Video.VideoPlayer player;
        [SerializeField]
        private GameObject pauseButton;
        [SerializeField]
        private GameObject playButton;
        [SerializeField]
        private GameObject restartButton;
        [SerializeField]
        private GameObject exitButton;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private int videoId;

        [SerializeField]
        private MediaLibrary catalog;

        [SerializeField]
        private GameObject mediaPlayer;

        private RenderTexture rt;
        private VideoMeta video;
        public float timeoutTime = 5.0f;
        private bool isVisible = false;
        private float lastButtonTime = 0f;

        public event EventHandler OnCloseVideo;
        public event EventHandler OnOpenVideo;
        public event EventHandler OnVideoReadyToPlay;


        void Start()
        {
            foreach(VideoMeta videoAux in catalog.mediaLibrary)
            {
                if(videoAux.id == videoId)
                {
                    video = videoAux;
                }
            }

            if(!video.is360)
            {

                rt = new RenderTexture((int)video.size.x, (int)video.size.y, 16, RenderTextureFormat.ARGB32);
                rt.Create();

                RawImage image = GetComponent<RawImage>();
                if(image != null)
                {
                    image.texture = rt;
                    player.targetTexture = rt;
                }
            }

            

            player.playOnAwake = video.playOnAwake;
            player.renderMode = video.renderMode;
            player.aspectRatio = video.aspectRatio;

            if(video.videoClip != null)
            {
                player.clip = video.videoClip;
            }
            else
            {
                player.url = video.url;
            }

            player.isLooping = video.loop;

            if(video.playOnAwake)
            {
                Play();
            }else{
                player.Prepare();
                playButton.SetActive(true);
                pauseButton.SetActive(false);
            }

            if(video.showMediaPlayer)
                SetVisible(true);
            else
                SetVisible(false);

        }


        public virtual void Play()
        {

            // playButton.SetActive(false);
            // pauseButton.SetActive(true);
            player.Play();
            
        }

        public virtual void Pause()
        {

            player.Pause();
            // playButton.SetActive(true);
            // pauseButton.SetActive(false);


        }

        public virtual void FastForward()
        {
            player.time = player.time + 10f;
        }
        public virtual void Rewind()
        {
            player.time = 0f;
        }
        public virtual void Close()
        {
            player.Pause();
            player.time = 0f;
            player.clip = null;
            mediaPlayer.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(video == null)
                return;
                
            if(slider != null)
                if(player.isPlaying && video.showMediaPlayer)
                    if(!float.IsNaN((float)player.time / (float)player.length))
                        slider.value = (float)player.time / (float)player.length;


        }


        public void SetVolume(float volume)
        {
            player.SetDirectAudioVolume(0,volume);
        }

        private void SetVisible(bool visible)
        {

            // mediaPlayer.SetActive(visible);
            // if(video.onlySkipOption)
            // {
            //     playButton.SetActive(false);
            //     pauseButton.SetActive(false);
            //     restartButton.SetActive(false);
            // }else{
            //     if(player.isPlaying)
            //     {
            //         playButton.SetActive(false);
            //         pauseButton.SetActive(true);
            //     }
            //     else{
            //         playButton.SetActive(true);
            //         pauseButton.SetActive(false);
            //     }

            //     restartButton.SetActive(true);
            // }
            // isVisible = visible;
        }

        public bool IsPlayingVideo()
        {
            return player.isPlaying;
        }
    }
}
