using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;

[CustomEditor(typeof(WallBuilder))]
public class WallBuilderEditor : OdinEditor
{
    private WallBuilder wallBuilder;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private void OnSceneGUI()
    {
        wallBuilder = target as WallBuilder;

        if (wallBuilder.showHelper)
        {
            handleTransform = wallBuilder.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

            for (int i = 0; i < wallBuilder.points.Length; i++)
            {
                ShowPoint(i);
                ShowLine(i);
            }
        }
    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(wallBuilder.points[index].point);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        Handles.Label(point, "point " + index);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(wallBuilder, "Move Point");
            EditorUtility.SetDirty(wallBuilder);
            wallBuilder.points[index].point = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }

    private void ShowLine(int index)
    {
        int maxIndex = index + 1;

        if (maxIndex >= wallBuilder.points.Length)
        {
            return;
        }

        Vector3 upperPoint1 = wallBuilder.points[index].point + wallBuilder.transform.position + (Vector3.up * wallBuilder.height);
        Vector3 upperPoint2 = wallBuilder.points[maxIndex].point + wallBuilder.transform.position + (Vector3.up * wallBuilder.height);
        Vector3 bottomPoint1 = wallBuilder.points[index].point + wallBuilder.transform.position;
        Vector3 bottomPoint2 = wallBuilder.points[maxIndex].point + wallBuilder.transform.position;

        Handles.color = Color.white;
        Handles.DrawLine(upperPoint1, upperPoint2);
        Handles.DrawLine(upperPoint1, bottomPoint1);
        Handles.DrawLine(bottomPoint1, bottomPoint2);
        Handles.DrawLine(upperPoint2, bottomPoint2);
    }
}
