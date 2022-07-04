using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private Vector2 _vector2Position;
    public Vector2 Vector2LocalPositionWayPoint
    {
        get { return _vector2Position; }
    }
    private void Awake()
    {
        _vector2Position = new Vector2(transform.localPosition.x, transform.localPosition.z); 
    }
}
