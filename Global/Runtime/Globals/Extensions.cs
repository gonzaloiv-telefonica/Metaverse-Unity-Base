using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using MEC;

namespace Meta.Global
{

    public static class Extensions
    {

        public static Sprite ToSprite(this Texture texture)
        {
            return Sprite.Create(
                texture as Texture2D,
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f
            );
        }

        public static string ToFileName(this Uri uri)
        {
            string fileName = uri.PathAndQuery;
            // ? Uncomment in case we want to remove the extension from the name
            // string ext = System.IO.Path.GetExtension("https://model3d.shopifycdn.com/models/o/587d31e2b8c7cd5b/iphone12promax.glb");
            // fileName = uri.Remove(uri.Length - ext.Length, ext.Length);
            fileName.Replace("/", "_");
            return fileName;
        }

        public static void MoveToWorldPose(this MonoBehaviour monoBehaviour, Pose pose)
        {
            monoBehaviour.transform.position = pose.position;
            monoBehaviour.transform.rotation = pose.rotation;
        }

        public static Pose ToWorldPose(this Transform transform)
        {
            return new Pose
            {
                position = transform.position,
                rotation = transform.rotation
            };
        }

        public static Pose ToLocalPose(this Transform transform)
        {
            return new Pose
            {
                position = transform.localPosition,
                rotation = transform.localRotation
            };
        }

        public static void Play(this AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }

        public static void OnSceneLoaded(this MonoBehaviour mono, string sceneName, Action onSceneLoaded)
        {
            IEnumerator<float> WaitForArtSceneRoutine()
            {
                Scene scene = SceneManager.GetSceneByName(sceneName);
                while (!scene.isLoaded)
                {
                    yield return Timing.WaitForOneFrame;
                }
                onSceneLoaded();
            }
            Timing.RunCoroutine(WaitForArtSceneRoutine());
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static List<Vector3> Flatten(this List<List<Vector3>> vectors)
        {
            List<Vector3> result = new List<Vector3>();
            foreach (List<Vector3> group in vectors)
            {
                foreach (Vector3 vector in group)
                {
                    result.Add(vector);
                }
            }
            return result;
        }

    }

}
