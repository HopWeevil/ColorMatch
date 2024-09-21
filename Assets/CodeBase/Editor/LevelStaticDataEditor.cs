using UnityEngine.SceneManagement;
using CodeBase.Logic.GameBasket;
using CodeBase.SO;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.SceneName = SceneManager.GetActiveScene().name;

                levelData.InitialBasketPosition = FindObjectOfType<BasketSpawnPoint>().transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}