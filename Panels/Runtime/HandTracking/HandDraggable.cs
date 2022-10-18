using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

namespace Meta.Panels
{

    public class HandDraggable : MonoBehaviour
    {

        private const float MaxDragValue = 0.1f;

        [SerializeField] private ButtonController buttonController;

        protected RayBehaviour rayBehaviour;
        private Vector3 previousPosition = Vector3.positiveInfinity;

        private bool HasRay => rayBehaviour.CurrentInteractable != null && rayBehaviour.CurrentInteractable.gameObject == buttonController.gameObject;
        private bool IsDragging => previousPosition.x != float.PositiveInfinity ||
            previousPosition.y != float.PositiveInfinity ||
            previousPosition.z != float.PositiveInfinity;

        protected virtual void Awake()
        {
            ResetInitialPosition();
            IEnumerator WaitForToolsInitializationRoutine()
            {
                while (rayBehaviour == null)
                {
                    yield return new WaitForEndOfFrame();
                    this.rayBehaviour = FindObjectOfType<RayBehaviour>(true);
                }
            }
            StartCoroutine(WaitForToolsInitializationRoutine());
        }

        private void ResetInitialPosition()
        {
            previousPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        }

        private void Update()
        {
            if (rayBehaviour == null)
                return;
            if (HasRay && rayBehaviour.IsPinching)
            {
                if (IsDragging)
                {
                    Vector3 drag = previousPosition - rayBehaviour.CurrentCollisionPosition;
                    drag = ClampDrag(drag); // ? Required because of the values given by Oculus SDK 
                    transform.localPosition -= drag;
                }
                previousPosition = rayBehaviour.CurrentCollisionPosition;
            }
            else
            {
                ResetInitialPosition();
            }
        }

        private Vector3 ClampDrag(Vector3 drag)
        {
            float x = Mathf.Abs(drag.x) > 0.1f ? 0 : drag.x;
            float y = Mathf.Abs(drag.y) > 0.1f ? 0 : drag.y;
            float z = Mathf.Abs(drag.z) > 0.1f ? 0 : drag.z;
            return new Vector3(x, y, z);
        }

    }

}