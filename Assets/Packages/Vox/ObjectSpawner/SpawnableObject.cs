using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

namespace Voxellers.ObjectSpawning
{
    public class SpawnableObject : MonoBehaviour
    {
        [System.Serializable]
        public class ObjectAction
        {
            public UnityEvent actionEvent = new UnityEvent();
            public float time;
        }

        public ObjectAction[] actions;

        //public UnityEvent OnActionsDone = new UnityEvent();

        public void ExecuteReturnActions(UnityAction onProcessEnd = null)
        {
            StartCoroutine(ReturningProcess(onProcessEnd));
        }

        private IEnumerator ReturningProcess(UnityAction onProcessEnd)
        {
            for (int actionIndex = 0; actionIndex < actions.Length; actionIndex++)
            {
                ObjectAction currentAction = actions[actionIndex];
                currentAction.actionEvent.Invoke();

                yield return new WaitForSeconds(currentAction.time);
            }

            if (onProcessEnd != null)
            {
                onProcessEnd.Invoke();
            }
        }
    }
}


