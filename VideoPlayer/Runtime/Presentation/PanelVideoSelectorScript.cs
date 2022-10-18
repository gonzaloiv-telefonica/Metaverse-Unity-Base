using UnityEngine;
using UnityEngine.UI;

namespace Meta.Video
{
    public class PanelVideoSelectorScript : MonoBehaviour
    {
        [SerializeField]Button loadBtn;
        [SerializeField]Button openBtn;
        [SerializeField]GameObject mundo;
        [SerializeField]GameObject panel;

        // Start is called before the first frame update
        void Start()
        {
            MetaVideoPlayer.Instance.OnVideoReadyToPlay += Instance_VideoLoaded;
            MetaVideoPlayer.Instance.OnOpenVideo += Instance_VideoPlaying;
            MetaVideoPlayer.Instance.OnCloseVideo += Instance_VideoClosed;
            openBtn.interactable = false;
        }

        // Update is called once per frame
        public void LoadVideo()
        {
            MetaVideoPlayer.Instance.LoadVideo(3);
        }

        public void PlayVideo()
        {
            MetaVideoPlayer.Instance.Play();
        }

        private void Instance_VideoLoaded(object sender, System.EventArgs e)
        {
            openBtn.interactable = true;
        }
        private void Instance_VideoPlaying(object sender, System.EventArgs e)
        {
            mundo.SetActive(false);
            panel.SetActive(false);
        }
        private void Instance_VideoClosed(object sender, System.EventArgs e)
        {
            mundo.SetActive(true);
            panel.SetActive(true);
            openBtn.interactable = false;
        }
        
    }
}