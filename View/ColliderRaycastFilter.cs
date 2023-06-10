using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public sealed class ColliderRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
	Collider2D _collider;

	void Start()
	{
		_collider = GetComponent<PolygonCollider2D>();
	}

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		return _collider && _collider.OverlapPoint(eventCamera.ScreenToWorldPoint(sp));
	}
}
