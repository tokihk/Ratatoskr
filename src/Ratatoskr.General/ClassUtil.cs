using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.General
{
    public static class ClassUtil
    {
        public static T Clone<T>(T obj)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream()) {
                /* オブジェクトをバイナリデータに変換 */
                formatter.Serialize(stream, obj);

                /* ストリームの位置をリセット */
                stream.Position = 0;

                /* 新規オブジェクトとして複製 */
                return ((T)formatter.Deserialize(stream));
            }
        }

        public static bool Compare<T>(T obj1, T obj2)
        {
            var formatter = new BinaryFormatter();
            var obj1_data = (byte[])null;
            var obj2_data = (byte[])null;

            /* オブジェクト1をバイナリデータに変換 */
            using (var stream = new MemoryStream()) {
                formatter.Serialize(stream, obj1);
                obj1_data = stream.ToArray();
            }

            /* オブジェクト2をバイナリデータに変換 */
            using (var stream = new MemoryStream()) {
                formatter.Serialize(stream, obj2);
                obj2_data = stream.ToArray();
            }

            return (obj1_data.SequenceEqual(obj2_data));
        }

        public static T[] CloneCopy<T>(T[] objs)
        {
            return (CloneCopy(objs, 0, objs.Length));
        }

        public static T[] CloneCopy<T>(T[] objs, int count)
        {
            return (CloneCopy(objs, 0, count));
        }

        public static T[] CloneCopy<T>(T[] objs, int offset, int count)
        {
            offset = Math.Min(objs.Length, Math.Max(0, offset));
            count  = Math.Min(count, Math.Max(0, objs.Length - offset));

            var copy_buffer = new T[count];

            Array.Copy(objs, offset, copy_buffer, 0, copy_buffer.Length);

            return (copy_buffer);
        }

        public static T[] ShiftCopy<T>(T[] objs, int offset, int count)
        {
            offset = Math.Min(objs.Length, Math.Max(0, offset));
            count  = Math.Min(count, Math.Max(0, objs.Length - offset));

            var copy_buffer = new T[objs.Length];

            Array.Copy(objs, offset, copy_buffer, 0, copy_buffer.Length);

            return (copy_buffer);
        }

        public static bool IsMatch<T>(T[] obj1, T[] obj2)
        {
            if (obj1.Length != obj2.Length)return (false);

            for (var index = 0; index < obj1.Length; index++) {
                if (!obj1[index].Equals(obj2[index]))return (false);
            }

            return (true);
        }
    }
}
