using UnityEngine;

namespace Voxellers
{
    public class VoxPhysics
    {
        /// <summary>
        /// 특정 시간이 흐른 후, 현재 위치로부터 어떻게 위치가 변화하였는지를 추정하는 함수.
        /// </summary>
        /// <param name="positionLastFrame"> 현재 프레임 이전 프레임에서의 위치 값 </param>
        /// <param name="positionCurrent"> 현재 프래임에서의 위치 값 </param>
        /// <param name="secondsFromNow"> 해당 시간이 지난 후의 위치 값을 추정 </param>
        /// <returns> 예상 추정 위치 </returns>
        public static Vector3 PredictPosition(Vector3 positionLastFrame, Vector3 positionCurrent, float secondsFromNow)
        {
            Vector3 velocity = positionCurrent - positionLastFrame;
            velocity /= Time.deltaTime;

            return PredictPositionWithVelocity(positionCurrent, velocity, secondsFromNow);
        }

        /// <summary>
        /// 특정 시간이 흐른 후, 현재 위치로부터 어떻게 위치가 변화하였는지를 추정하는 함수. 위치 값이 아니라 속도 값을 이용하여 계산함.
        /// </summary>
        /// <param name="positionCurrent"> 현재 위치 </param>
        /// <param name="velocity"> 현재 위치에서 작용하는 속도 </param>
        /// <param name="secondsFromNow"> 해당 시간이 지난 후의 위치 값을 추정 </param>
        /// <returns> 예상 추정 위치 </returns>
        public static Vector3 PredictPositionWithVelocity(Vector3 positionCurrent, Vector3 velocity, float secondsFromNow)
        {
            Vector3 newPosition = positionCurrent + velocity * secondsFromNow;

            return newPosition;
        }

        /// <summary>
        /// base, target 두 개의 물체가 충돌하는 위치를 구하는 함수. position, velocity 정보가 필요함.
        /// </summary>
        /// <param name="hitPoint"> 두 물체가 충돌할 수 있는 경우, 예상되는 충돌 위치 정보를 가지고 있음 </param>
        /// <param name="basePosition"> base 오브젝트의 위치 값 </param>
        /// <param name="baseVelocity"> base 오브젝트의 속도 값 </param>
        /// <param name="targetPosition"> target 오브젝트의 위치 값 </param>
        /// <param name="targetVelocity"> target 오브젝트의 속도 값 </param>
        /// <returns> 충돌 가능 여부를 판정함 </returns>
        public static bool PredictHitPoint(out Vector3 hitPoint, Vector3 basePosition, Vector3 baseVelocity, Vector3 targetPosition, Vector3 targetVelocity)
        {
            Vector3 distanceBetweenObjects = targetPosition - basePosition;

            float a = targetVelocity.sqrMagnitude - baseVelocity.sqrMagnitude;
            if (a >= 0)
            {
                hitPoint = Vector3.zero;
                return false;
            }

            float b = 2 * Vector3.Dot(distanceBetweenObjects, targetVelocity);
            float c = distanceBetweenObjects.sqrMagnitude;

            float rt = Mathf.Sqrt(b * b - 4 * a * c);
            float dt1 = (-b + rt) / (2 * a);
            float dt2 = (-b - rt) / (2 * a);
            float dt = (dt1 < 0 ? dt2 : dt1);

            hitPoint = targetPosition + targetVelocity * dt;
            return true;
        }
    }
}

