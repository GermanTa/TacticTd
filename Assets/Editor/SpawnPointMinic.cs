using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnPointMinic))]
public class SpawnPointMinicEditor : UnityEditor.Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnPointMinic spawnPointMinic, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPointMinic.transform.position, 0.2f);
    }

}

