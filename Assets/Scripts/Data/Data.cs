using System.IO;
using System.Text;
using UnityEngine;

public class Data<T>
{
    private string filepath;
    public string FileName { set => filepath = $"{Application.persistentDataPath}/{value}"; }

    public Data()
    {
        filepath = null;
    }

    public Data(string filename)
    {
        FileName = filename;
    }

    public T Read()
    {
        if (filepath == null) return default;
        if (!File.Exists(filepath)) return default;
        byte[] load = File.ReadAllBytes(filepath);
        T data = JsonUtility.FromJson<T>(Encoding.UTF8.GetString(load));
        return data;
    }

    public void Write(T data)
    {
        if (filepath == null) throw new System.NullReferenceException("File name is null.");
        string save = JsonUtility.ToJson(data);
        byte[] bytes = Encoding.UTF8.GetBytes(save);
        File.WriteAllBytes(filepath, bytes);
    }

    public bool Exists()
    {
        return File.Exists(filepath);
    }
}
