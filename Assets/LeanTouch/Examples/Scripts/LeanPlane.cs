using UnityEngine;

namespace Lean.Touch
{
	// This component stores information about a 3D plane
	public class LeanPlane : MonoBehaviour
	{
		[Header("Clamp")]
		[Tooltip("Should the plane be clamped on the x axis?")]
		public bool ClampX;

		public float MinX;

		public float MaxX;

		[Tooltip("Should the plane be clamped on the y axis?")]
		public bool ClampY;

		public float MinY;

		public float MaxY;

		[Header("Snap")]
		[Tooltip("The distance between each position snap on the x axis")]
		public float SnapX;

		[Tooltip("The distance between each position snap on the x axis")]
		public float SnapY;

		public Vector3 GetClosest(Vector3 position, float offset = 0.0f)
		{
			// Transform point to plane space
			var point = transform.InverseTransformPoint(position);

			// Clamp values?
			if (ClampX == true)
			{
				point.x = Mathf.Clamp(point.x, MinX, MaxX);
			}

			if (ClampY == true)
			{
				point.y = Mathf.Clamp(point.y, MinY, MaxY);
			}

			// Snap values?
			if (SnapX != 0.0f)
			{
				point.x = Mathf.Round(point.x / SnapX) * SnapX;
			}

			if (SnapY != 0.0f)
			{
				point.y = Mathf.Round(point.y / SnapY) * SnapY;
			}

			// Reset Z to plane
			point.z = 0.0f;

			// Transform back into world space
			return transform.TransformPoint(point) + transform.forward * offset;
		}

		public bool TryRaycast(Ray ray, ref Vector3 hit, float offset = 0.0f)
		{
			var point    = transform.position;
			var plane    = new Plane(transform.forward, point);
			var distance = default(float);

			if (plane.Raycast(ray, out distance) == true)
			{
				hit = ray.GetPoint(distance);
				hit = GetClosest(hit, offset);
				return true;
			}

			return false;
		}

		public Vector3 GetClosest(Ray ray)
		{
			var point    = transform.position;
			var plane    = new Plane(transform.forward, point);
			var distance = default(float);

			if (plane.Raycast(ray, out distance) == true)
			{
				return GetClosest(ray.GetPoint(distance));
			}

			return point;
		}

#if UNITY_EDITOR
		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.matrix = transform.localToWorldMatrix;

			var x1 = MinX;
			var x2 = MaxX;
			var y1 = MinY;
			var y2 = MaxY;

			if (ClampX == false)
			{
				x1 = -1000.0f;
				x2 =  1000.0f;
			}

			if (ClampY == false)
			{
				y1 = -1000.0f;
				y2 =  1000.0f;
			}

			if (ClampX == false && ClampY == false)
			{
				Gizmos.DrawLine(new Vector3(  x1, 0.0f), new Vector3(  x2, 0.0f));
				Gizmos.DrawLine(new Vector3(0.0f,   y1), new Vector3(0.0f,   y2));
			}
			else
			{
				Gizmos.DrawLine(new Vector3(x1, y1), new Vector3(x2, y1));
				Gizmos.DrawLine(new Vector3(x1, y2), new Vector3(x2, y2));

				Gizmos.DrawLine(new Vector3(x1, y1), new Vector3(x1, y2));
				Gizmos.DrawLine(new Vector3(x2, y1), new Vector3(x2, y2));
			}
		}
#endif
	}
}