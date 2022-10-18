using System;
using UnityEngine;
using UnityEngine.UI;
using Meta.Panels;
using RenderHeads.Media.AVProVideo;
using Meta.VR;

namespace Meta.Video
{
    public class MetaVideoPlayer : MonoBehaviour
    {
        public static MetaVideoPlayer Instance { get; private set; }

        [SerializeField] private MediaPlayer player;
        [SerializeField] private GameObject videoGO;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject restartButton;
        [SerializeField] private GameObject exitButton;
        [SerializeField] private Slider slider;
        [SerializeField] private int videoId;
        [SerializeField] private MediaLibrary catalog;
        [SerializeField] private GameObject mediaPlayer;
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private GameObject errorPanel;

        public GameObject controlsPanel;

        private RenderTexture rt;
        private VideoMeta video;
        public float timeoutTime = 5.0f;
        private bool isVisible = false;
        private float lastButtonTime = 0f;
        private GameObject OVR_CameraRig;
        private bool readyToPlay = false;
        private bool isPlaying = false;

        public event EventHandler OnCloseVideo;
        public event EventHandler OnOpenVideo;
        public event EventHandler OnVideoReadyToPlay;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            OVR_CameraRig = FindObjectOfType<OVRCameraRig>().gameObject;
        }

        void HandleEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode code)
        {
            Debug.Log("eventType: " + eventType.ToString());

            if (eventType == MediaPlayerEvent.EventType.Error)
            {
                Debug.LogError("Error: " + code);
                readyToPlay = false;
                isPlaying = false;
                loadingPanel.SetActive(true);
                errorPanel.SetActive(true);
                player.Control.Stop();
                controlsPanel.GetComponent<MoveInFrontTarget>().ShowPanel();
                player.Events.RemoveAllListeners();

            }
            if (eventType == MediaPlayerEvent.EventType.FirstFrameReady)
            {
                Prepared();
            }
            if (eventType == MediaPlayerEvent.EventType.FinishedPlaying)
            {
                EndReached();
            }

            if (eventType == MediaPlayerEvent.EventType.Stalled)
            {
                controlsPanel.GetComponent<MoveInFrontTarget>().ShowPanel();
                isPlaying = false;
                loadingPanel.SetActive(true);

            }
            if (eventType == MediaPlayerEvent.EventType.Unstalled)
            {
                isPlaying = true;
                loadingPanel.SetActive(false);
            }
            if (eventType == MediaPlayerEvent.EventType.FinishedSeeking)
            {
                if(!errorPanel.activeSelf)
                    Play();
            }
            if (eventType == MediaPlayerEvent.EventType.Started)
            {
                
            }
            
        }


        public void LoadVideo(int catalogIdVideo)
        {
            videoId = catalogIdVideo;
            foreach(VideoMeta videoAux in catalog.mediaLibrary)
            {
                if(videoAux.id == videoId)
                {
                    video = videoAux;
                }
            }


            if(OVR_CameraRig != null)
            {
                videoGO.transform.rotation =  new Quaternion(OVR_CameraRig.transform.rotation.x, 0f, OVR_CameraRig.transform.rotation.z,OVR_CameraRig.transform.rotation.w);//new Quaternion(0f,OVR_CameraRig.transform.rotation.y,0f, 1f);
            }
            

            if(video.videoClip != null)
            {
                // player.clip = video.videoClip;
            }
            else
            {
                player.enabled = true;
                player.Events.RemoveAllListeners();
                player.Events.AddListener(HandleEvent);
                bool isOpening = player.OpenMedia(new MediaPath(video.url, MediaPathType.AbsolutePathOrURL), autoPlay:video.playOnAwake);
            }

            player.Loop = video.loop;

            if(video.playOnAwake)
            {
                Play();
                loadingPanel.SetActive(true);
                controlsPanel.GetComponent<MoveInFrontTarget>().ShowPanel();
            }else{
                playButton.SetActive(true);
                pauseButton.SetActive(false);
            }

        }

        void EndReached()
        {
            if(video.exitOnEnd)
                Close();

            if(!video.loop)
            {
                endPanel.SetActive(true);
                controlsPanel.GetComponent<MoveInFrontTarget>().ShowPanel();
            }
        }

        void Prepared() {
            readyToPlay = true;
            OnVideoReadyToPlay?.Invoke(this, EventArgs.Empty);
        }

        public void Play()
        {
            OnOpenVideo?.Invoke(this, EventArgs.Empty);
            videoGO.SetActive(true);
            SetVisible(video.showMediaPlayer);


            playButton.SetActive(false);
            pauseButton.SetActive(true);
            player.Control.Play();
            isPlaying = true;
        }

        public void Pause()
        {
            OnOpenVideo?.Invoke(this, EventArgs.Empty);
            videoGO.SetActive(true);

            player.Control.Pause();
            playButton.SetActive(true);
            pauseButton.SetActive(false);
            isPlaying = false;

        }

        public void FastForward()
        {
            // player.time = player.time + 10f;
        }
        public void Rewind()
        {
            player.Control.Stop();
            player.RewindPrerollPause();
            isPlaying = false;
            endPanel.SetActive(false);

        }
        public void Close()
        {
            readyToPlay = false;
            OnCloseVideo?.Invoke(this, EventArgs.Empty);
            player.Control.Pause();
            isPlaying = false;
            SetVisible(false);
            videoGO.SetActive(false);
            loadingPanel.SetActive(false);
            endPanel.SetActive(false);
            errorPanel.SetActive(false);
            AvatarController.SetInteractorsVisibility(true);
            player.Control.CloseMedia();
        }

        // Update is called once per frame
        void Update()
        {
            if(OVR_CameraRig != null)
            {
                videoGO.transform.position = OVR_CameraRig.transform.position;
            }
            
            if(video == null || !videoGO.activeSelf || errorPanel.activeSelf)
                return;
                

            if(video.is360)
            {
                if(!readyToPlay && videoGO.activeSelf){
                    SetVisible(false);
                    loadingPanel.SetActive(true);
                    return;
                }else if(isPlaying){
                        loadingPanel.SetActive(false);
                }

                if(OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    lastButtonTime = Time.time;
                    if (!isVisible)
                    {

                        if(!mediaPlayer.activeSelf)
                            controlsPanel.GetComponent<MoveInFrontTarget>().ShowPanel();

                        SetVisible(true);
                    }
                }

                if (OVRInput.Get(OVRInput.Button.Back))
                {
                    if (isVisible)
                    {
                        SetVisible(false);
                    }
                }

                if (isVisible && isPlaying && Time.time - lastButtonTime > timeoutTime)
                {
                    SetVisible(false);
                }
            }
        }

        private void SetVisible(bool visible)
        {
            if(video != null && video.videoType == VideoType.VideoIntro && PlayerPrefs.GetInt("Video"+video.id.ToString(),0) == 0){
                mediaPlayer.SetActive(false);
                return;
            }

            mediaPlayer.SetActive(visible);
            if(video != null && video.onlySkipOption)
            {
                playButton.SetActive(false);
                pauseButton.SetActive(false);
                restartButton.SetActive(false);
            }else{
                if(isPlaying)
                {
                    playButton.SetActive(false);
                    pauseButton.SetActive(true);
                }
                else{
                    playButton.SetActive(true);
                    pauseButton.SetActive(false);
                }

                restartButton.SetActive(true);
            }

            AvatarController.SetInteractorsVisibility(visible);
            isVisible = visible;
        }

        public bool IsPlayingVideo()
        {
            return isPlaying;
        }
        public bool IsVideoActive()
        {
            return videoGO.activeSelf;
        }
    }
}
