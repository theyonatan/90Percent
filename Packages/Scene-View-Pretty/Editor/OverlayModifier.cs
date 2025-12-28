using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bewildered.SceneViewPretty
{
    [InitializeOnLoad]
    internal static class OverlayModifier
    {
        private static HashSet<SceneView> _appliedSceneViews = new HashSet<SceneView>();

        static OverlayModifier()
        {
            SceneView.duringSceneGui += ApplyStyle;
        }
        private static void ApplyStyle(SceneView sceneView)
        {
            if (_appliedSceneViews.Contains(sceneView))
                return;

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/com.bewildered.scene-view-pretty/Editor/UI/NicerOverlays.uss");
            
            sceneView.rootVisualElement.panel.visualTree.Q("unity-overlay-canvas").styleSheets.Add(styleSheet);
            _appliedSceneViews.Add(sceneView);
        }
    }
}
