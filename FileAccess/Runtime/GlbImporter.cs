using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RSG;
using Piglet;
using MEC;

namespace Meta.FileAccess
{

    /// <summary>
    /// Facade for PigletglTFImporter 
    /// </summary>
    public class GlbImporter
    {

        public Promise<GameObject> Import(string url)
        {
            Promise<GameObject> promise = new Promise<GameObject>();
            GltfImportOptions importOptions = new GltfImportOptions();
            GltfImportTask taskImport = RuntimeGltfImporter.GetImportTask(url, importOptions);
            taskImport.OnCompleted = promise.Resolve;
            taskImport.OnException = result => promise.Reject(new Exception(result.Message));
            IEnumerator<float> UpdateTask()
            {
                while (taskImport.MoveNext())
                    yield return Timing.WaitForOneFrame;
            }
            Timing.RunCoroutine(UpdateTask());
            return promise;
        }

    }

}