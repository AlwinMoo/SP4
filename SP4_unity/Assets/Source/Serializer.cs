using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class Serializer : MonoBehaviour {

    private static Serializer instance = new Serializer();

    private Serializer() { }

    public static Serializer GetInstance()
    {
        return instance;
    }

    public byte[] Serialize<T> (T param)
    {
        byte[] bytes;
        IFormatter formatter = new BinaryFormatter();

        using (MemoryStream stream = new MemoryStream())
        {

            formatter.Serialize(stream, param);
            return bytes = stream.ToArray();
        }
    }

    public T Deserialize<T>(byte[] param)
    {
        using (MemoryStream ms = new MemoryStream(param))
        {
            IFormatter br = new BinaryFormatter();
            return (T)br.Deserialize(ms);
        }
    }
}
