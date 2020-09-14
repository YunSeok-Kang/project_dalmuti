using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers
{
    public class VoxMath : MonoBehaviour
    {
        // temp
        public Vector2 cPos1, cPos2, cPos3;

        /// <summary>
        /// 세 좌표를 기반으로 원의 중심과 반지름 크기를 알아오는 함수.
        /// 
        /// 참고
        /// - https://darkpgmr.tistory.com/60
        /// </summary>
        /// <param name="centerOfCircle"></param>
        /// <param name="radiusOfCircle"></param>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <param name="pos3"></param>
        /// <returns></returns>
        public static bool GetCenterOfCircleByThreePos(out Vector2 centerOfCircle, out float radiusOfCircle, Vector2 pos1, Vector2 pos2, Vector2 pos3)
        {
            centerOfCircle = Vector3.zero;
            radiusOfCircle = 0f;

            float det = (pos2.x - pos1.x) * (pos3.y - pos2.y) - (pos2.y - pos1.y) * (pos3.x - pos2.x);
            if (det == 0)
            {
                return false;
            }

            Vector2 poweredPos1 = pos1 * pos1;
            Vector2 poweredPos2 = pos2 * pos2;
            Vector2 poweredPos3 = pos3 * pos3;

            float p = poweredPos2.x - poweredPos1.x + poweredPos2.y - poweredPos1.y;
            float q = poweredPos3.x - poweredPos2.x + poweredPos3.y - poweredPos2.y;

            float centerX = (pos3.y - pos2.y) * p + (pos1.y - pos2.y) * q;
            float centerY = (pos2.x - pos3.x) * p + (pos2.x - pos1.x) * q;

            centerOfCircle = new Vector2(centerX, centerY);
            centerOfCircle *= 1 / (2 * det);

            radiusOfCircle = Vector2.Distance(pos2, centerOfCircle);

            return true;
        }

        /// <summary>
        /// 선형 보간 함수
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="currentX"></param>
        /// <returns></returns>
        public static float LinearInterpolation(float minX, float maxX, float minY, float maxY, float currentX)
        {
            float currentY;
            if (currentX < minX)
            {
                currentY = minY;
            }
            else if (maxX < currentX)
            {
                currentY = maxY;
            }
            else
            {
                float d1 = currentX - minX;
                float d2 = maxX - currentX;
                float width = maxX - minX;

                currentY = (d2 / width) * minY + (d1 / width) * maxY;
            }

            return currentY;
        }
    }
}
