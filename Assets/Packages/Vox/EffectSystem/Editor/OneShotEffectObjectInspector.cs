using UnityEngine;
using UnityEditor;

namespace Voxellers.EffectManagement
{
    [CustomEditor(typeof(OneShotEffectObject))]
    public class OneShotEffectObjectInspector : Editor
    {
        private OneShotEffectObject targetObject;

        private void OnEnable()
        {
            targetObject = (OneShotEffectObject)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //EditorGUI.BeginChangeCheck();

            ////EditorGUILayout.LabelField("테스트", targetObject.key);
            //string key = EditorGUILayout.TextField("Effect Key", targetObject.key);
            ////float lifeTime = EditorGUILayout.FloatField("Effect Lifetime", targetObject.effectLifetime);
            //float lifeTime = EditorGUILayout.Slider("Effect Lifetime", targetObject.effectLifetime, 0f, 30f);


            //if (EditorGUI.EndChangeCheck())
            //{
            //    //변경전에 Undo 에 등록
            //    Undo.RecordObject(targetObject, "OneShotEffectObject");

            //    targetObject.key = key;
            //    targetObject.effectLifetime = lifeTime;
            //}
        }
    }
}