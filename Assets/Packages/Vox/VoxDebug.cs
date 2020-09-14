using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxellers
{
    public class VoxDebug
    {
        /// <summary>
        /// Cube를 화면에 렌더링 함. OnDrawGizmos, OnDrawGizmosSelected 메서드에서 사용됨.
        /// </summary>
        /// <param name="position"> 큐브가 그려질 좌표. World 좌표 기준.</param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
            Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

            Gizmos.matrix *= cubeTransform;

            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

            Gizmos.matrix = oldGizmosMatrix;
        }

        public static void DrawSphere(Vector3 position, float radius)
        {
            Gizmos.DrawWireSphere(position, radius);
        }
    }
}

