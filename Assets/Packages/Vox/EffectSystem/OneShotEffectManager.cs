using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Voxellers.ObjectPooling;

namespace Voxellers.EffectManagement
{
    public class OneShotEffectManager : MonoSingleton<OneShotEffectManager>
    {
        [System.Serializable]
        public class EffectObjectData
        {
            public string key;
            public GameObject prefab;
            public int numberOfEffects;
        }

        [SerializeField]
        private List<EffectObjectData> _effectObjectDatas;

        private EffectPooler _effectPooler;
        private Dictionary<GameObject, PooledObject> _usingEffects;

        // 프리셋을 지정해둘 수 있는 방법은?

        // 프리셋 List를 지정할 수 있어야 함 -> 등록을 위한 클래스가 하나 필요하려나? 아니면 오브젝트 배열만?
        // 등록된 애들이 있으면, 이를 지정된 갯수만큼 풀링.
        // 요청이 들어오면, 쉬고 있는 오브젝트를 가지고 와서 넘겨줌.

        private void Awake()
        {
            _usingEffects = new Dictionary<GameObject, PooledObject>();
            _effectPooler = new EffectPooler(); //gameObject.AddComponent<EffectPooler>();
            _effectPooler.effectSpawningTransform = gameObject.transform;

            for (int effectIndex = 0; effectIndex < _effectObjectDatas.Count; effectIndex++)
            {
                EffectObjectData currentData = _effectObjectDatas[effectIndex];
                _effectPooler.AddEffect(currentData.key, currentData.prefab, currentData.numberOfEffects);
            }

            _effectPooler.UpdateEffectInfo();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject RequestEffect(string key, Vector3 position, Quaternion rotation)
        {
            PooledObject requestedEffect = _effectPooler.GetEffect(key);
            GameObject effectObject = requestedEffect.gameObject;
            effectObject.transform.position = position;
            effectObject.transform.rotation = rotation;

            _usingEffects.Add(effectObject, requestedEffect);

            return effectObject;
        }

        public void ReturnEffect(string key, GameObject returningEffectObject)
        {
            PooledObject returningPooledObj = _usingEffects[returningEffectObject];
            _usingEffects.Remove(returningEffectObject);

            _effectPooler.ReturnEffect(key, returningPooledObj);
        }
    }
}