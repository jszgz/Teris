using UnityEngine;
using System.Collections;

public class use_boom_skill : MonoBehaviour {

    public GameObject Bomb;
    public static int boomeffect=0;               //技能等级决定炸的效果
    Vector2 v=new Vector2(-1,-1);
    bool canClick = true;
	void Update () 
    {
        if (Input.GetMouseButtonUp(0))
        {
            v = Grid.roundVec2(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Grid.insideBorder(v)&&canClick)
        {
            StartCoroutine(waitBoom());
            canClick = false;
        }
	}

    IEnumerator waitBoom()
    {
        GameObject tempGO=Instantiate(Bomb, v, Quaternion.identity)as GameObject;
        yield return new WaitForSeconds(2f);
        Destroy(tempGO);
        if (boomeffect > 0)
        {
            Grid.deleteSingle(v);
            Grid.deleteSingle(v + new Vector2(1, 0));
            Grid.deleteSingle(v + new Vector2(-1, 0));
            Grid.deleteSingle(v + new Vector2(0,1));
            Grid.deleteSingle(v + new Vector2(0, -1));
            if (boomeffect > 1)
            {
                Grid.deleteSingle(v + new Vector2(1, 1));
                Grid.deleteSingle(v + new Vector2(1, -1));
                Grid.deleteSingle(v + new Vector2(-1, 1));
                Grid.deleteSingle(v + new Vector2(-1, -1));
                if (boomeffect > 2)
                {
                    Grid.deleteSingle(v + new Vector2(2, 0));
                    Grid.deleteSingle(v + new Vector2(-2, 0));
                    Grid.deleteSingle(v + new Vector2(0, 2));
                    Grid.deleteSingle(v + new Vector2(0, -2));
                }
            }
        }
        if (boomeffect == 1)
        {
            Grid.decreaseColumnAboveMore(v + new Vector2(1, 0));
            Grid.decreaseColumnAboveMore(v + new Vector2(-1, 0));
            Grid.decreaseColumnAboveMore(v + new Vector2(0, 1));
        }
        if (boomeffect == 2)
        {
            Grid.decreaseColumnAboveMore(v + new Vector2(1, 1));
            Grid.decreaseColumnAboveMore(v + new Vector2(-1, 1));
            Grid.decreaseColumnAboveMore(v + new Vector2(0, 1));
        }
        if (boomeffect == 3)
        {
            Grid.decreaseColumnAboveMore(v + new Vector2(0, 2));
            Grid.decreaseColumnAboveMore(v + new Vector2(1, 1));
            Grid.decreaseColumnAboveMore(v + new Vector2(-1, 1));
            Grid.decreaseColumnAboveMore(v + new Vector2(2, 0));
            Grid.decreaseColumnAboveMore(v + new Vector2(-2, 0));
        }
        canClick = true;
        v = new Vector2(-1, -1);
        enabled = false;
    }
}
