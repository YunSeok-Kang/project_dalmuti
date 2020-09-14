using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers.EffectManagement
{
    public class OneShotEffectGroupPlayer : MonoBehaviour
    {
        public OneShotEffectObject[] oneShotEffectObjects;
        private void Awake()
        {
            oneShotEffectObjects =  GetComponentsInChildren<OneShotEffectObject>();
        }
        public void Activate()
        {
            foreach(var effect in oneShotEffectObjects)
            {
                effect.Activate();
            }
        }
    }
}