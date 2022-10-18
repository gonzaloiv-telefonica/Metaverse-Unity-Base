using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Meta.Global
{

    public static class Routines
    {

        public static IEnumerator InvokeAfterSecondsOnCondition(Action onComplete, float remainingTime, Func<bool> condition)
        {
            while (remainingTime > 0)
            {
                if (condition())
                    remainingTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            onComplete();
        }

    }

}

