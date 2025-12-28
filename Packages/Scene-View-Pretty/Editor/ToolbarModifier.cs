using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;
[InitializeOnLoad]
internal static class ToolbarModifier
{
	static ToolbarModifier()
	{
		EditorApplication.delayCall += ApplyStyle;
      
	}

	private static void ApplyStyle()
	{
      
		StyleSheet styleSheet1 = AssetDatabase.LoadAssetAtPath<StyleSheet>(
			"Packages/com.bewildered.scene-view-pretty/Editor/UI/NicerToolbar.uss");
                     
		var toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

		var toolbar = Resources.FindObjectsOfTypeAll(toolbarType)[0];
		var field = toolbarType.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
		var toolbarRoot = (VisualElement)field.GetValue(toolbar);
		toolbarRoot.styleSheets.Add(styleSheet1);
		
		
		
	}
}