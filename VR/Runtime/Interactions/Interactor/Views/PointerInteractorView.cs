using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class PointerInteractorView : MonoBehaviour, IInteractorView
    {

        private const float MaxScaleRatio = 0.1f;
        private const float PositionOffset = 0.01f;

        [SerializeField] private Transform point;
        [SerializeField] private Color interactableColor;
        [SerializeField] private Color zoneColor = Color.gray;
        [SerializeField] private TouchRayVisualizer rayVisualizer;

        private Vector3 initialScale;
        private Renderer rend;
        private Collider currentCol;

        private void Awake()
        {
            initialScale = point.localScale;
            rend = point.gameObject.GetComponent<Renderer>();
        }

        public void Hide()
        {
            point.gameObject.SetActive(false);
            currentCol = null;
        }

        public void ShowForInteractable(RaycastHit hit)
        {
            Show(hit);
            rend.material.color = interactableColor;
        }

        public void ShowForZone(RaycastHit hit)
        {
            Show(hit);
            rend.material.color = zoneColor;
        }

        private void Show(RaycastHit hit)
        {
            currentCol = hit.collider;
            point.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (currentCol == null)
                return;
            CastRay();
        }

        private void CastRay()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (currentCol.Raycast(ray, out RaycastHit hit, 10))
            {
                SetPosition(hit);
                SetScale(hit);
            }
        }

        private void SetPosition(RaycastHit hit)
        {
            point.position = hit.point;
            point.position += (transform.position - point.position).normalized * PositionOffset;
        }

        private void SetScale(RaycastHit hit)
        {
            point.localScale = initialScale * hit.distance;
            if (point.localScale.x > MaxScaleRatio)
                point.localScale = new Vector3(MaxScaleRatio, point.localScale.y, MaxScaleRatio);
        }

        public void SetVisibility(bool isActive)
        {
            rayVisualizer.SetActive(isActive);
            point.gameObject.SetActive(isActive);
        }

    }

}

