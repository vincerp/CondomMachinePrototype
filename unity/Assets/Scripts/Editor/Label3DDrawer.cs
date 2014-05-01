using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Label3D))]
public class Label3DDrawer : Editor {

	[DrawGizmo(GizmoType.NotSelected|GizmoType.SelectedOrChild)]
	static void DrawSomething(Transform objectTransform, GizmoType gizmoType){
		Label3D l3d = objectTransform.GetComponent<Label3D>();

		if(null == l3d) return;

		Handles.Label(((l3d.target)?l3d.target.position:objectTransform.position) + l3d.offset, l3d.text, "Button");

	}

//	void OnSceneGUI(){
//		MonoBehaviour mb = target as MonoBehaviour;
//		Transform tr = mb.transform;
//
//		Handles.Label(tr.position, "Whatever");
//	}
}
