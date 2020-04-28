using QFSW.QGUI;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QFSW.QH
{
    public class QuantumHierarchy : EditorWindow
    {
        private Vector2 _scroll;
        private readonly List<GameObject> _rootGameObjectBuffer = new List<GameObject>();

        [MenuItem("Window/Quantum Hierarchy")]
        public static void Create()
        {
            GetWindow<QuantumHierarchy>(false, "Quantum Hierarchy");
        }

        void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene.isLoaded)
                {
                    EditorGUILayout.LabelField(scene.name, EditorStyles.boldLabel);

                    _rootGameObjectBuffer.Clear();
                    scene.GetRootGameObjects(_rootGameObjectBuffer);

                    foreach (GameObject root in _rootGameObjectBuffer)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.ObjectField(root, typeof(GameObject), true);

                        HideFlags flags = (HideFlags) EditorGUILayout.EnumFlagsField(root.hideFlags);
                        if (flags != root.hideFlags)
                        {
                            root.hideFlags = flags;
                            EditorSceneManager.MarkSceneDirty(scene);
                        }

                        if (QGUILayout.ButtonAuto(new GUIContent("Delete"), EditorStyles.miniButton))
                        {
                            DestroyImmediate(root);
                            EditorSceneManager.MarkSceneDirty(scene);
                        }

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.Space();
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }
}