using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private Vector2 _vectro2Position;
    public Vector2 Vectro2LocalPositionWayPoint
    {
        get { return _vectro2Position; }
    }
    private void Awake()
    {
        _vectro2Position = new Vector2(transform.localPosition.x, transform.localPosition.z); 
    }
}
