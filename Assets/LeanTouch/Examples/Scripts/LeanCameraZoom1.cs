using UnityEngine;

namespace Lean.Touch
{
    // This script allows you to zoom a camera in and out based on the pinch gesture
    // This supports both perspective and orthographic cameras

	[ExecuteInEditMode]
	public class LeanCameraZoom1 : MonoBehaviour
	{
        public GameObject paused;

        [Tooltip("The camera that will be zoomed (None = MainCamera)")]
		public Camera Camera;

		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreStartedOverGui = true;

		[Tooltip("Ignore fingers with IsOverGui?")]
		public bool IgnoreIsOverGui;

		[Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("If RequiredSelectable.IsSelected is false, ignore?")]
		public LeanSelectable RequiredSelectable;

		[Tooltip("If you want the mouse wheel to simulate pinching then set the strength of it here")]
		[Range(-1.0f, 1.0f)]
		public float WheelSensitivity;

		[Tooltip("The current FOV/Size")]
		public float Zoom = 50.0f;

		[Tooltip("Limit the FOV/Size?")]
		public bool ZoomClamp;

		[Tooltip("The minimum FOV/Size we want to zoom to")]
		public float ZoomMin = 10.0f;

		[Tooltip("The maximum FOV/Size we want to zoom to")]
		public float ZoomMax = 60.0f;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Start();
		}
#endif

		protected virtual void Start()
		{
            if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}


		protected virtual void LateUpdate()
		{
            if (!paused.GetComponent<Pause>().isPaused)
            {
                // Get the fingers we want to use
                var fingers = LeanSelectable.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount, RequiredSelectable);

                // Get the pinch ratio of these fingers
                var pinchRatio = LeanGesture.GetPinchRatio(fingers, WheelSensitivity);

                // Modify the zoom value
                Zoom /= pinchRatio;

                if (ZoomClamp == true)
                {
                    Zoom = Mathf.Clamp(Zoom, ZoomMin, ZoomMax);
                }

                // Set the new zoom
                SetZoom(Zoom);
            }
		}

		protected void SetZoom(float current)
		{
            Debug.Log(current);
            GetComponent<SetZoom>().Zoom(current);
            
            
            /*
            // Make sure the camera exists
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
				if (camera.orthographic == true)
				{
					camera.orthographicSize = current;
				}
				else
				{
					camera.fieldOfView = current;
				}
			}
			else
			{
				Debug.LogError("Failed to find camera. Either tag your cameras MainCamera, or set one in this component.", this);
			}
            */
		}
	}
}