using UnityEngine;
using System.Collections;

public class use_add_skill : MonoBehaviour {

    public GameObject singleSquare;
    public static int useTimes=0;               //技能使用次数
    Vector2 v = new Vector2(-1, -1);

    void Update()
    {
        if (useTimes > 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                v = Grid.roundVec2(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            if (Grid.insideBorder(v))
            {
                if (Grid.grid[(int)v.x, (int)v.y] == null)
                {
                    GameObject tempTr = Instantiate(singleSquare, v, Quaternion.identity) as GameObject;
                    while (Grid.insideBorder(v + new Vector2(0, -1)) && Grid.grid[(int)v.x, (int)v.y - 1] == null)
                    {
                        tempTr.transform.position += new Vector3(0, -1, 0);
                        v += new Vector2(0, -1);
                    }
                    Grid.grid[(int)v.x, (int)v.y] = tempTr.transform;
                    Grid.deleteFullRows();
                    v = new Vector2(-1, -1);
                    useTimes--;
                }
            }
        }
        if (useTimes == 0)
            enabled = false;
    }
}
