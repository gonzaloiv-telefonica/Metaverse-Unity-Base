using System.Collections;
using UnityEngine;

namespace Meta.Panels
{
	public class PanelApearInFrontTarget : MonoBehaviour
	{
		private const float TOTAL_DURATION = 0.0f;
		private const float HMD_MOVEMENT_THRESHOLD = 0.3f;

		[SerializeField] private float _maxDistance = 0.3f;
		[SerializeField] private float _minDistance = 0.05f;
		[SerializeField] private float _minZDistance = 0.05f;

		private OVRCameraRig _cameraRig;
		private Vector3 _panelInitialPosition = Vector3.zero;
		private Coroutine _coroutine = null;
		private Vector3 _prevPos = Vector3.zero;
		private Vector3 _lastMovedToPos = Vector3.zero;

		private void Awake()
		{
			_cameraRig = FindObjectOfType<OVRCameraRig>();
			_panelInitialPosition = transform.position;
		}

		private void Update()
		{
			var centerEyeAnchorPos = _cameraRig.centerEyeAnchor.position;
			var myPosition = transform.position;
			//Distance from centereye since last time we updated panel position.
			float distanceFromLastMovement = Vector3.Distance(centerEyeAnchorPos, _lastMovedToPos);
			float headMovementSpeed = (_cameraRig.centerEyeAnchor.position - _prevPos).magnitude / Time.deltaTime;
			var currDiffFromCenterEye = transform.position - centerEyeAnchorPos;
			var currDistanceFromCenterEye = currDiffFromCenterEye.magnitude;

			// 1) wait for center eye to stabilize after distance gets too large
			// 2) check if center eye is too close to panel
			// 3) check if depth isn't too close
			if (((distanceFromLastMovement > _maxDistance) || (_minZDistance > currDiffFromCenterEye.z) || (_minDistance > currDistanceFromCenterEye)) &&
				headMovementSpeed < HMD_MOVEMENT_THRESHOLD && _coroutine == null)
			{
				if (_coroutine == null)
				{
					_coroutine = StartCoroutine(LerpToHMD());
				}
			}

			_prevPos = _cameraRig.centerEyeAnchor.position;
		}

		private Vector3 CalculateIdealAnchorPosition()
		{
			return _cameraRig.centerEyeAnchor.position + _panelInitialPosition;
		}

		private IEnumerator LerpToHMD()
		{
			Vector3 newPanelPosition = CalculateIdealAnchorPosition();
			_lastMovedToPos = _cameraRig.centerEyeAnchor.position;
			float startTime = Time.time;
			float endTime = Time.time + TOTAL_DURATION;

			while (Time.time < endTime)
			{
				transform.position =
				  Vector3.Lerp(transform.position, newPanelPosition, (Time.time - startTime) / TOTAL_DURATION);
				yield return null;
			}

			transform.position = newPanelPosition;

			_coroutine = null;
		}

        public void SetPanelOrientation()
        {
            transform.rotation = new Quaternion(0.0f, _cameraRig.centerEyeAnchor.rotation.y, 0.0f, _cameraRig.centerEyeAnchor.rotation.w);
        }
	}
}
