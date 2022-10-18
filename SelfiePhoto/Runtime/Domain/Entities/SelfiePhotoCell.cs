using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Meta.SelfiePhotos
{
    public class SelfiePhotoCell : MonoBehaviour
    {

        [SerializeField] private RawImage backGround;
        [SerializeField] private Image selfiePhoto;

        private MenuGalleryPanelScript parent;
        private SelfiePhoto photo;

        // Start is called before the first frame update
        public IEnumerator PaintPhoto(SelfiePhoto photoPassed, MenuGalleryPanelScript parentPassed)
        {
            parent = parentPassed;
            photo = photoPassed;

            yield return 0;

            selfiePhoto.sprite = LoadNewSprite(photoPassed.location);
            selfiePhoto.color = Color.white;
            backGround.texture = null;
            // backGround.color = new Color(0f,0f,0f,1f);
        }

        public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
        {    
            // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
            Texture2D SpriteTexture = LoadTexture(FilePath);
            Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);
            return NewSprite;
        }
        public Texture2D LoadTexture(string FilePath)
        {
            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails
            Texture2D Tex2D;
            byte[] FileData;
            if (File.Exists(FilePath))
            {
                FileData = File.ReadAllBytes(FilePath);
                Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                    return Tex2D;                 // If data = readable -> return texture
            }
            return null;                     // Return null if load failed
        }

        public void EditPhoto()
        {
            parent.ShowPhotoPanel(photo);
        }

    }
}
