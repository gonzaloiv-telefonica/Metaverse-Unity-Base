/************************************************************************************

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using System;
using UnityEngine;
using Meta.VR;

namespace Meta.VR
{
    /// <summary>
    /// This component is responsible for moving the character capsule to match the HMD, fading out the camera or blocking movement when 
    /// collisions occur, and adjusting the character capsule height to match the HMD's offset from the ground.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(OVRPlayerController))]
    public class RoomScaleFix : MonoBehaviour
    {

        /// <summary>
        /// This should be a reference to the OVRCameraRig that is usually a child of the PlayerController.
        /// </summary>
        [Tooltip("This should be a reference to the OVRCameraRig that is usually a child of the PlayerController.")]
        public OVRCameraRig CameraRig;

        /// <summary>
        /// This value represents the character capsule's distance from the HMD's position. When the player is moving in legal space without collisions, this will be zero.
        /// </summary>
        [Tooltip("This value represents the character capsule's distance from the HMD's position. When the player is moving in legal space without collisions, this will be zero.")]
        public float CurrentDistance;

        /// <summary>
        /// When true, the camera will be prevented from passing through collidable geometry. This is usually considered uncomfortable for users.
        /// </summary>
        [Tooltip("When true, the camera will be prevented from passing through collidable geometry. This is usually considered uncomfortable for users.")]
        public bool EnableCollision;

        [SerializeField] TinyHeadCollider tinyHeadCollider;

        private CharacterController _character;
        private OVRPlayerController _playerController;

        void Awake()
        {
            _character = GetComponent<CharacterController>();
            _playerController = GetComponent<OVRPlayerController>();
        }

        void OnEnable()
        {
            _playerController.PreCharacterMove += PreCharacterMovement;
        }

        void OnDisable()
        {
            _playerController.PreCharacterMove -= PreCharacterMovement;
        }


        /// <summary>
        /// This method is the handler for the PlayerController.PreCharacterMove event, which is used
        /// to do the work of fading out the camera or adjust the position depending on the 
        /// settings and the relationship of where the camera is and where the character is.
        /// </summary>
        void PreCharacterMovement()
        {
            if (_playerController.Teleported)
                return;
            // First, determine if the lateral movement will collide with the scene geometry.
            var oldCameraPos = CameraRig.transform.position;
            var wpos = CameraRig.centerEyeAnchor.position;
            var delta = wpos - transform.position;
            delta.y = 0;
            var len = delta.magnitude;
            if (len > 0.0f)
            {
                _character.Move(delta);
                var currentDelta = transform.position - wpos;
                currentDelta.y = 0;
                CurrentDistance = currentDelta.magnitude;
                CameraRig.transform.position = new Vector3(oldCameraPos.x, CameraRig.transform.position.y, oldCameraPos.z);
                if (EnableCollision && CurrentDistance > 0)
                {
                    if (tinyHeadCollider.IsTouchingWalls)
                        CameraRig.transform.position -= delta;
                }
            }
        }

    }

}