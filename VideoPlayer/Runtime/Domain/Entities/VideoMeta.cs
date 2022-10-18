using UnityEngine;
namespace Meta.Video
{
    [System.Serializable]
    public class VideoMeta
    {
        public int id;
        public bool is360;
        public string url;
        public UnityEngine.Video.VideoClip videoClip;
        public Vector2 size;
        public bool loop;
        public bool playOnAwake;
        public bool showMediaPlayer;
        public bool exitOnEnd;
        public bool onlySkipOption;
        public UnityEngine.Video.VideoRenderMode renderMode;
        public UnityEngine.Video.VideoAspectRatio aspectRatio;
        public VideoType videoType;

    }
    public enum VideoType {
    VideoIntro,
    VideoRegular
  }

}
