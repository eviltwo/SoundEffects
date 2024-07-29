using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SoundEffects.Editor
{
    [CustomEditor(typeof(SoundEffectDetailList))]
    public class SoundEffectDetailListEditor : UnityEditor.Editor
    {
        private static List<SoundEffectDetail> _soundEffectDetailBuffer = new List<SoundEffectDetail>();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            if (GUILayout.Button("Collect"))
            {
                Collect(_soundEffectDetailBuffer);
                var property = serializedObject.FindProperty("_soundEffectDetails");
                property.ClearArray();
                property.arraySize = _soundEffectDetailBuffer.Count;
                for (int i = 0; i < _soundEffectDetailBuffer.Count; i++)
                {
                    property.GetArrayElementAtIndex(i).objectReferenceValue = _soundEffectDetailBuffer[i];
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void Collect(List<SoundEffectDetail> result)
        {
            result.Clear();
            var detailList = target as SoundEffectDetailList;
            var parentPath = AssetDatabase.GetAssetPath(detailList);
            var parentDirectory = Path.GetDirectoryName(parentPath);
            var directories = detailList.AutoCollectDirectories;
            if (directories.Length == 0)
            {
                return;
            }
            var searchDirectories = new string[directories.Length];
            for (int i = 0; i < directories.Length; i++)
            {
                var dir = directories[i];
                searchDirectories[i] = Path.Combine(parentDirectory, dir);
            }
            var foundGUIDs = AssetDatabase.FindAssets($"t:{nameof(SoundEffectDetail)}", searchDirectories);
            foreach (var guid in foundGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var detail = AssetDatabase.LoadAssetAtPath<SoundEffectDetail>(path);
                result.Add(detail);
            }
        }
    }
}
