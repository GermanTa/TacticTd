using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : UnityEditor.Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(WayPoint wayPoint, GizmoType gizmo)
    {
        Gizmos.DrawSphere(wayPoint.transform.position, 0.2f);
    }
   
}
