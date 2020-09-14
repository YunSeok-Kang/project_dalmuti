using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Voxellers.ObjectPooling;

namespace Voxellers.EffectManagement
{
    /// <summary>
    /// 기존의 ObjectPooler와 다른 점
    /// 
    /// - 0.1초마다 굳이 이펙트가 끝났는지 확인할 필요가 없음.
    /// - IPooled 인터페이스를 굳이 상속받을 필요가 없음. (리셋 시에 하는 행동이 다이나믹하지 않음)
    /// </summary>
    public class EffectPooler
    {
        private struct EffectData
        {
            public string key;
            public GameObject prefab;
            public int numberOfEffects;

            public EffectData(string key, GameObject prefab, int numberOfEffects)
            {
                this.key = key;
                this.prefab = prefab;
                this.numberOfEffects = numberOfEffects;
            }
        }

        public Transform effectSpawningTransform;

        private Dictionary<string, ObjectPooler> _poolsDic = new Dictionary<string, ObjectPooler>();
        private LinkedList<EffectData> _unInitalizedEffectsList = new LinkedList<EffectData>();

        #region UnityEvents

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        public void AddEffect(string key, GameObject prefab, int numberOfEffects)
        {
            EffectData effectData = new EffectData(key, prefab, numberOfEffects);
            _unInitalizedEffectsList.AddLast(effectData);
        }

        public void RemoveEffect(string key)
        {

        }

        public void UpdateEffectInfo()
        {
            LinkedList<EffectData>.Enumerator enumerator = _unInitalizedEffectsList.GetEnumerator();
            while (_unInitalizedEffectsList.Count > 0)
            {
                EffectData currentData = _unInitalizedEffectsList.First.Value;
                string currentKey = currentData.key;
                GameObject currentPrefab = currentData.prefab;
                int currentSpawningNumbers = currentData.numberOfEffects;

                if (currentKey != null && currentPrefab != null)
                {
                    ObjectPooler newPool = new ObjectPooler();
                    newPool.poolKey = currentKey;
                    newPool.poolingPrefab = currentPrefab;
                    newPool.numberOfObjectsOnce = currentSpawningNumbers;

                    newPool.SpawningParent = effectSpawningTransform;
                    newPool.MakeNew();

                    _poolsDic.Add(currentKey, newPool);
                }

                _unInitalizedEffectsList.RemoveFirst();
            }
        }

        public PooledObject GetEffect(string key)
        {
            bool isContains = _poolsDic.ContainsKey(key);
            if (isContains)
            {
                ObjectPooler currentPool = _poolsDic[key];
                return currentPool.GetNew();
            }

            return null;
        }

        public void ReturnEffect(string key, PooledObject returningObject)
        {
            bool isContains = _poolsDic.ContainsKey(key);
            if (isContains)
            {
                ObjectPooler currentPool = _poolsDic[key];
                currentPool.Return(returningObject);
            }
        }
    }
}