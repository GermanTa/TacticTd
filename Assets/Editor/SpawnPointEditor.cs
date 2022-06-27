using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : UnityEditor.Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnPoint spawnPoint, GizmoType gizmo)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPoint.transform.position, 0.2f);
    }

}
