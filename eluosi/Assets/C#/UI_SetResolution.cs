using UnityEngine;
using System.Collections;

public class UI_SetResolution : MonoBehaviour {
    public const float defaultScreenWidth = 1080f;
    public const float defaultScreenHeight = 1920f;
    public static float XRatio;
    public static float YRatio;
    public static float minRatio;

    void Awake()
    {
        XRatio = Screen.width / defaultScreenWidth;
        YRatio = Screen.height / defaultScreenHeight;
        minRatio = XRatio < YRatio ? XRatio : YRatio;
    }

    public static void SetAResolution(Transform root)
    {
        UISetReso[] objs = root.GetComponentsInChildren<UISetReso>(true);
        for (int i = 0; i < objs.Length; i++)
        {
            SetOne(objs[i]);
        }
    }

    public static void SetOne(UISetReso uis)
    {
        if (uis == null)
            return;
        uis.transform.localPosition = new Vector3(uis.transform.localPosition.x * XRatio, uis.transform.localPosition.y * YRatio,uis.transform.localPosition.z);
        if (uis.type == ResolutionType.normal)
        {
            uis.transform.localScale = new Vector3(uis.transform.localScale.x * minRatio, uis.transform.localScale.y * minRatio,1f);
        }
        else if (uis.type == ResolutionType.stretch)
        {
            uis.transform.localScale = new Vector3(uis.transform.localScale.x * XRatio, uis.transform.localScale.y * YRatio,1f);
        }
    }
}
