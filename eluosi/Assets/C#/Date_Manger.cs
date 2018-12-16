using UnityEngine;
using System;
using System.Collections;
using System.IO;
using JsonFx.Json;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;
public class Date_Manger{

    static string fileName = "userdate.dat";
    static string key = "12348578902223367877723456789012";     //密钥
    private static Mydate mydate_instance;
    public static Mydate mydate_Instance
    {
        get
        {
            if (mydate_instance == null)
            {
                if (!ExistFile())
                    mydate_instance = new Mydate();
                else
                    Load();
            }
            return mydate_instance;
        }
    }

    public static bool ExistFile()                              //是否存在保存数据的文件
    {
        return File.Exists(GetDatePath() + "/" + fileName);
    }

    public static void DeleteFile()                             //删除保存数据的文件
    {
        if (ExistFile())
            File.Delete(GetDatePath() + "/" + fileName);
    }

    public static void Load()                                   //加载数据
    {
        string orginText = File.ReadAllText(GetDatePath() + "/" + fileName);
        orginText = Decrypt(orginText);
        mydate_instance = JsonFx.Json.JsonReader.Deserialize<Mydate>(orginText);
    }

    public static void Save()                           //保存数据
    {
        string text = JsonFx.Json.JsonWriter.Serialize(mydate_instance);
        text = Encrypt(text);
        File.WriteAllText(GetDatePath() + "/" + fileName,text);
    }

    public static string GetDatePath()                 //获得设备路径
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return path + "/Documents";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            return Application.persistentDataPath + "/";
        }
        else
            return Application.dataPath;
    }

    public static string Encrypt(string toE)                //加密
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateEncryptor();
        byte[]toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public static string Decrypt(string toD)                    //解密
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        byte[] toEncryptArray = Convert.FromBase64String(toD);
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}
