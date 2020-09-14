using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers.EffectManagement
{
    public class OneShotEffectObject : MonoBehaviour
    {
        //public enum PivotingMode
        //{
        //    position,
        //    transform
        //}

        public string key;
        public bool makeChildOfEffectPivot = false;
        public float effectLifetime;
        [Header("Delay")]
        public float minDelay=0;
        public float maxDelay=0;

        public Transform effectPivot;
        public Vector3 biasPosition;

        private WaitForSeconds _waitForLifeTimeEnd;

        private void Awake()
        {
            _waitForLifeTimeEnd = new WaitForSeconds(effectLifetime);
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void Activate()
        {
            // EffectManager에 파티클 오브젝트 요청해야 함.
            // 요청할 때는, 위치 등을 지정해줘야 함(Pivot으로)

            StartCoroutine(EffectProcess());
        }

        private IEnumerator EffectProcess()
        {
            if (minDelay != 0 || maxDelay != 0)
            {
                float delay = Random.Range(minDelay, maxDelay);

                yield return new WaitForSeconds(delay);
            }
            Transform pivot = (effectPivot != null) ? effectPivot : gameObject.transform;
            GameObject effectObject = OneShotEffectManager.Instance.RequestEffect(key, pivot.position + biasPosition, pivot.rotation);

            if(makeChildOfEffectPivot == true)
            {
                effectObject.transform.parent = this.transform;
            }

            yield return _waitForLifeTimeEnd;

            OneShotEffectManager.Instance.ReturnEffect(key, effectObject);
        }
    }
}