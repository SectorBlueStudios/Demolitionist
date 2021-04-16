using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to track & pedestral this GameObject (e.g. Camera) based on finger drags
	public class LeanCameraMove : MonoBehaviour
	{
        public bool levelSelection = false;

        public GameObject paused;

        [Tooltip("The camera the movement will be done relative to (None = MainCamera)")]
		public Camera Camera;

		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreStartedOverGui = true;

		[Tooltip("Ignore fingers with IsOverGui?")]
		public bool IgnoreIsOverGui;

		[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("The sensitivity of the movement, use -1 to invert")]
		public float Sensitivity = 1.0f;

		public LeanScreenDepth ScreenDepth;

        public float minX, maxX, minY, maxY, minZ, maxZ;

        public virtual void SnapToSelection()
		{
			var center = default(Vector3);
			var count  = 0;

			for (var i = 0; i < LeanSelectable.Instances.Count; i++)
			{
				var selectable = LeanSelectable.Instances[i];

				if (selectable.IsSelected == true)
				{
					center += selectable.transform.position;
					count  += 1;
				}
			}

			if (count > 0)
			{
				transform.position = center / count;
			}
		}

        protected virtual void LateUpdate()
		{
            var tempZoom = 1500 - gameObject.GetComponent<LeanCameraZoom>().Zoom;

            if (!paused.GetComponent<Pause>().isPaused)
            {
                // Get the fingers we want to use
                var fingers = LeanTouch.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount);

                // Get the last and current screen point of all fingers
                var lastScreenPoint = LeanGesture.GetLastScreenCenter(fingers);
                var screenPoint = LeanGesture.GetScreenCenter(fingers);

                // Get the world delta of them after conversion
                var worldDelta = ScreenDepth.ConvertDelta(lastScreenPoint, screenPoint, Camera, gameObject);

				var oldPosition = transform.localPosition;

				if (fingers.Count == 1 && fingers[0].IsActive == true)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, 1 << 9))
                    { }
                    else
                    {
                        if (levelSelection == true) //Clamps the camera using the Min/Max set by the user
                        {

							transform.position = new Vector3(Mathf.Clamp(transform.position.x - (worldDelta.x * Sensitivity), minX - tempZoom, maxX + tempZoom),
                                                              Mathf.Clamp(transform.position.y - (worldDelta.y * Sensitivity), minY - tempZoom, maxY + tempZoom),
                                                              Mathf.Clamp(transform.position.z - (worldDelta.z * Sensitivity), minZ, maxZ));
						}
						else
						{
							transform.position -= worldDelta * Sensitivity; //Main function that moves the camera around

							//UGLY CODE -- BOUNDS
								if (transform.localPosition.x > maxX)
									transform.localPosition = new Vector3(maxX, transform.localPosition.y, transform.localPosition.z);
								if (transform.localPosition.x < minX)
									transform.localPosition = new Vector3(minX, transform.localPosition.y, transform.localPosition.z);
								if (transform.localPosition.y > maxY)
									transform.localPosition = new Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
								if (transform.localPosition.y < minY)
									transform.localPosition = new Vector3(transform.localPosition.x, minY, transform.localPosition.z);
								if (transform.localPosition.z > maxZ)
									transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
								if (transform.localPosition.z < minZ)
									transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, minZ);
						}
                    }
                }
                else 
                {
                    if (levelSelection == true) //Ensures zooming out affects the current position of the camera
                    {
                        transform.position = new Vector3(Mathf.Clamp(transform.localPosition.x - (worldDelta.x * Sensitivity), minX - tempZoom, maxX + tempZoom),
                                                    Mathf.Clamp(transform.localPosition.y - (worldDelta.y * Sensitivity), minY - tempZoom, maxY + tempZoom),
                                                    Mathf.Clamp(transform.localPosition.z - (worldDelta.z * Sensitivity), minZ, maxZ));

						/*

						// Pan the camera based on the world delta


						// Add to remainingDelta
						remainingDelta += transform.localPosition - oldPosition;

						// Get t value
						var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

						// Dampen remainingDelta
						var newRemainingDelta = Vector3.Lerp(remainingDelta, Vector3.zero, factor);

						// Shift this position by the change in delta
						transform.localPosition = oldPosition + remainingDelta - newRemainingDelta;

						if (fingers.Count == 0 && Inertia > 0.0f && Dampening > 0.0f)
						{
							newRemainingDelta = Vector3.Lerp(newRemainingDelta, remainingDelta, Inertia);
						}

						// Update remainingDelta with the dampened value
						remainingDelta = newRemainingDelta;
						*/
					}


                }
            }
        }
	}
}