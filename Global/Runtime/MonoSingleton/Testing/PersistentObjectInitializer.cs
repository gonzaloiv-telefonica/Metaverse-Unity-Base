using Meta.Global.MonoSingleton;
using UnityEngine;

internal static class PersistentObjectInitializer 
{
    private static GameObject GetPrefab()
    {
        return Resources.Load<GameObject>("PersistentObjects");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void OnSubsystemRegistration()
    {
        PersistentObject.Initialize(GetPrefab());
    }
}
