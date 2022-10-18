using System.Collections.Generic;
using UnityEngine;


namespace Meta.Video
{
    
    [CreateAssetMenu(fileName = "Media_Library", menuName = "Meta/Media Library/Media_Library")]
    public class MediaLibrary : ScriptableObject
    {
        public List<VideoMeta> mediaLibrary;
    }
}
