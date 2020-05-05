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
        private GameObject _target;
        private Vector2 _scroll;

        private readonly List<GameObject> _rootGameObjectBuffer = new List<GameObject>();

        [MenuItem("Window/Quantum Hierarchy")]
        public static void Create()
        {
            GetWindow<QuantumHierarchy>(false, "Quantum Hierarchy");
        }

        private void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                DrawScene(SceneManager.GetSceneAt(i));
            }

            DrawTarget();

            EditorGUILayout.EndScrollView();
        }

        private void DrawTarget()
        {
            _target = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Target", "View the children of the specified target"), _target, typeof(GameObject), true);
            if (_target)
            {
                for (int i = 0; i < _target.transform.childCount; i++)
                {
                    DrawRow(_target.transform.GetChild(i).gameObject);
                }
            }
            EditorGUILayout.Space();
        }

        private void DrawScene(Scene scene)
        {
            if (scene.isLoaded)
            {
                EditorGUILayout.LabelField(scene.name, EditorStyles.boldLabel);

                _rootGameObjectBuffer.Clear();
                scene.GetRootGameObjects(_rootGameObjectBuffer);

                foreach (GameObject root in _rootGameObjectBuffer)
                {
                    DrawRow(root);
                }

                EditorGUILayout.Space();
            }
        }

        private void DrawRow(GameObject obj)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.ObjectField(obj, typeof(GameObject), true);

            HideFlags flags = (HideFlags)EditorGUILayout.EnumFlagsField(obj.hideFlags);
            if (flags != obj.hideFlags)
            {
                obj.hideFlags = flags;
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }

            if (QGUILayout.ButtonAuto(new GUIContent("View", "Makes this object the current target and displays its children"), EditorStyles.miniButton))
            {
                _target = obj;
            }

            if (QGUILayout.ButtonAuto(new GUIContent("Delete", "Destroys and deletes the object from the scene"), EditorStyles.miniButton))
            {
                DestroyImmediate(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}