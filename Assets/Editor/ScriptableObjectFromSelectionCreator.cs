using System.IO;
using UnityEditor;
using UnityEngine;

namespace crlimacastro.Editor
{
    public static class ScriptableObjectFromSelectionCreator
    {
        [MenuItem("Assets/Create/ScriptableObject Asset From Selected MonoScript")]
        public static void CreateScriptableObjectAssetFromSelectedMonoScript()
        {
            if (Selection.activeObject == null)
            {
                Debug.LogError("No object selected.");
                return;
            }
            if (Selection.activeObject is not MonoScript)
            {
                Debug.LogError("Selected object must be a MonoScript for a class extending ScriptableObject.");
                return;
            }

            var type = (Selection.activeObject as MonoScript)?.GetClass();

            if (type == null)
            {
                Debug.LogError("Could not determine class in MonoScript.");
                return;
            }

            if (!type.IsSubclassOf(typeof(ScriptableObject)))
            {
                Debug.LogError("Class must extend ScriptableObject.");
                return;
            }

            var scriptableObject = ScriptableObject.CreateInstance(type);
            var path = $"{Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeObject))}/{ObjectNames.NicifyVariableName(Selection.activeObject.name)}.asset";
            ProjectWindowUtil.CreateAsset(scriptableObject, path);
        }
    }
}