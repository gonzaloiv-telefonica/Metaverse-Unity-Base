using UnityEngine;

namespace Meta.SelfiePhotos
{
    [System.Serializable]
    public class SelfiePhoto
    {
        public string location;
        public int width;
        public int height;
        public System.DateTime date; 

        public SelfiePhoto(int widthPass, int heightPass, string path)
        {
            width = widthPass;
            height = heightPass;
            location = path;
            date = System.DateTime.Now;
        }
    }
}
