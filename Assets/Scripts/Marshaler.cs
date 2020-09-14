using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalmutiClient
{
    public class Marshaler : Nettention.Proud.Marshaler
    {
        // Nettention.Proud.Marshaler 안에 기본적인 Marshaler 함수가 정의되어 있음.
        // 3D 벡터 타입을 위한 Marshaler 함수를 추가한다.
        public static void Write(Nettention.Proud.Message msg, UnityEngine.Vector3 b)
        {
            msg.Write(b.x);
            msg.Write(b.y);
            msg.Write(b.z);
        }

        public static void Read(Nettention.Proud.Message msg, out UnityEngine.Vector3 b)
        {
            b = new UnityEngine.Vector3();
            msg.Read(out b.x);
            msg.Read(out b.y);
            msg.Read(out b.z);
        }
    }
}
