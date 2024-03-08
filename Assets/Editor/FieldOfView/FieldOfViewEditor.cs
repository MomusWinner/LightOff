using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor {

    void OnSceneGUI() {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;

        foreach (var viewParam in fow.ViewParams)
        {
            Handles.DrawWireArc (fow.transform.position, Vector3.up, fow.transform.forward, viewParam.ViewAngle / 2, viewParam.ViewRadius);
            Handles.DrawWireArc (fow.transform.position, Vector3.up, fow.transform.forward, -viewParam.ViewAngle / 2, viewParam.ViewRadius);
            Vector3 viewAngleA = fow.DirFromAngle (-viewParam.ViewAngle / 2, false);
            Vector3 viewAngleB = fow.DirFromAngle (viewParam.ViewAngle / 2, false);

            Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * viewParam.ViewRadius);
            Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * viewParam.ViewRadius);
        }

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.VisibleTargets) {
            Handles.DrawLine (fow.transform.position, visibleTarget.position);
        }
    }
}