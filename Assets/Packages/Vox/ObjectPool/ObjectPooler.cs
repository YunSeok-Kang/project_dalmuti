using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers.ObjectPooling
{
    [System.Serializable]
    public class ObjectPooler
    {
        public string poolKey = null;

        public GameObject poolingPrefab = null;

        public int numberOfObjectsOnce = 5;

        private LinkedList<PooledObject> _notUsingObjects = new LinkedList<PooledObject>(); // 현재 사용중이지 않은 객체
        private LinkedList<PooledObject> _usingObjects = new LinkedList<PooledObject>(); // 현재 사용 중인 객체

        public Transform SpawningParent
        {
            set; get;
        }

        //        public GameObject GetNew()
        public PooledObject GetNew()
        {
            if (_notUsingObjects.Count <= 0)
            {
                Debug.LogWarning("ObjectPooler: 런타임 도중 Pooler" + "(" + poolKey + ")" + "의 MakeNew()가 호출되었습니다. 성능 저하를 불러 일으킬 수 있으니, numberOfObjectsOnce 값을 증가시켜 이를 방지하십시오.");

                MakeNew();
            }

            PooledObject gettingObj = _notUsingObjects.First.Value;

            _notUsingObjects.RemoveFirst();
            _usingObjects.AddLast(gettingObj);

            // 상속 아니면 컴포넌트 이원화
            // 이원화로 결정
            IPooled pooledInterface = gettingObj;
            pooledInterface.ResetObject();

            gettingObj.gameObject.transform.parent = null;
            gettingObj.gameObject.SetActive(true);

            return gettingObj;
        }

        public void Return(PooledObject returningObject)
        {
            _notUsingObjects.AddLast(returningObject);
            _usingObjects.Remove(returningObject);

            returningObject.gameObject.transform.parent = SpawningParent;
            returningObject.gameObject.SetActive(false);
        }

        /// <summary>
        /// 사용하지 않는 오브젝트를 찾아서 풀링 할당 해제
        /// </summary>
        public void CollectGarbages()
        {
            PooledObject[] notUsings = FindNotUsingObjectsInUsings();

            for (int objectIndex = 0; objectIndex < notUsings.Length; objectIndex++)
            {
                PooledObject currentObject = notUsings[objectIndex];
                Return(currentObject);
            }
        }

        /// <summary>
        /// numberOfObjectsOnce 만큼 새롭게 오브젝트를 생성
        /// </summary>
        public void MakeNew()
        {
            for (int objectCount = 0; objectCount < numberOfObjectsOnce; objectCount++)
            {
                GameObject newObject = Object.Instantiate(poolingPrefab, Vector3.zero, Quaternion.identity);
                newObject.transform.parent = SpawningParent;
                newObject.SetActive(false);
                
                PooledObject pooledObjectComp = newObject.GetComponent<PooledObject>();
                if (pooledObjectComp == null)
                {
                    pooledObjectComp = newObject.AddComponent<PooledObject>();

                    ObjectPoolingOptions options = newObject.GetComponent<ObjectPoolingOptions>();
                    if (options)
                    {
                        pooledObjectComp.OnObjectReset = options.OnPoolingObjectReset;
                    }
                }

                _notUsingObjects.AddLast(pooledObjectComp);
            }
        }

        /// <summary>
        /// 함수 이름 추천좀;
        /// </summary>
        /// <returns></returns>
        private PooledObject[] FindNotUsingObjectsInUsings()
        {
            List<PooledObject> notUsingObjects = new List<PooledObject>(); // 이거 GC 부르는 행위 아닐까? 초기회 시점에 이거 해줘야.
            //foreach (PooledObject currentObject in _usingObjects)
            //{
            //    if (currentObject.gameObject.activeSelf == false)
            //    {
            //        notUsingObjects.Add(currentObject);
            //    }
            //}

            LinkedList<PooledObject>.Enumerator em = _usingObjects.GetEnumerator();
            while (em.MoveNext())
            {
                PooledObject currentObject = em.Current;
                if (currentObject.gameObject.activeSelf == false)
                {
                    notUsingObjects.Add(currentObject);
                }
            }

            return notUsingObjects.ToArray();
        }
    }
}

