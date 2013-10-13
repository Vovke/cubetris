using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Grid))]
public class GridEditor : Editor
{
	Grid grid;
	
	public void OnEnable()
	{
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate += GridUpdate;
	}
	
	public void OnDisable()
	{
		SceneView.onSceneGUIDelegate -= GridUpdate;
	}
	
	void GridUpdate(SceneView sceneview)
	{
		Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;
		
		if (e.isKey && e.character == 'a')
		{
			GameObject obj;
			Object prefab = EditorUtility.GetPrefabParent(Selection.activeObject);
			
			if (prefab)
			{
				Undo.IncrementCurrentEventIndex();
				obj = (GameObject)EditorUtility.InstantiatePrefab(prefab);
				Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width + grid.width/2.0f,
				                              Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.height/2.0f, 0.0f);
				obj.transform.position = aligned;
				Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name); 
			}
		}
		else if (e.isKey && e.character == 'd')
		{
			Undo.IncrementCurrentEventIndex();
			Undo.RegisterSceneUndo("Delete Selected Objects");
			foreach (GameObject obj in Selection.gameObjects)
				DestroyImmediate(obj);
		}
	}
	
	public override void OnInspectorGUI()
	{	
		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Width ");
		grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label(" Grid Height ");
		grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
		GUILayout.EndHorizontal();
		
		if (GUILayout.Button("Open Grid Window", GUILayout.Width(255)))
        {   
           GridWindow window = (GridWindow) EditorWindow.GetWindow(typeof (GridWindow));
		   window.Init();
        }

		SceneView.RepaintAll();
	}
}
