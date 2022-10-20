using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Meta.FileAccess
{

    public static class Extensions
    {

        private const string JPG = ".jpg";
        private const string PNG = ".png";

        public static byte[] ToBytes(this Texture2D texture, string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext)
            {
                case JPG:
                    return texture.EncodeToJPG();
                case PNG:
                    return texture.EncodeToPNG();
                default:
                    return null;
            }
        }

    }

}