using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    //the gird itself
    public static int w = 10;
    public static int h = 35;

    public static Transform[,] grid = new Transform[w, h];


    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    public static void deleteRow(int y)//Single row at once
    {
        for (int x=0;x<w;++x)
        {
            if (grid[x, y])
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
        Game_Manger.getInstance().AddScores(100);
    }

    public static void decreaseRow(int y)//Single row at once
    {
        for (int x=0;x<w;++x)
        {
            if(grid[x,y]!=null)
            {
                //move one toward bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                //update block posiiton
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowsAbove(int y)
    {
        for (int i=y;i< h;++i)
        {
            decreaseRow(i);
        }
    }


    public static bool isFullRow(int y)
    {
        for (int x=0;x<w;++x)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    public static void deleteFullRows()
    {
        for(int y=0;y<h;++y)
        {
            if(isFullRow(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    public static void clear()                              //清除全部
    {
        for (int j = 0; j < h; j++)
        {
            for (int x = 0; x < w; ++x)
            {
                if (grid[x, j] != null)
                {
                    Destroy(grid[x, j].gameObject);
                    grid[x, j] = null;
                }
            }
        }
    }

    public static int highestRow()                  //有小方块的最高一行
    {
        for (int y = 0; y < h; ++y)
        {
            if (!rowhas(y))
                return (y - 1);
        }
        return -1;
    }

    public static bool rowhas(int y)                     //某一行是否有小方块
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
                return true;
        }
        return false;
    }

    public static void deleteSingle(Vector2 v)               //消除单个方块
    {
        if (insideBorder(v))
        {
            if (grid[(int)v.x, (int)v.y] !=null)
            {
                Destroy(grid[(int)v.x, (int)v.y].gameObject);
                grid[(int)v.x, (int)v.y] = null;
            }
        }
    }

    public static void decreaseColumnAboveMore(Vector2 v)               //某个方块以上的一列向下移动多行，直到填满以下全部空白块
    {
        Vector2 v1 = lowestBlank(v);
        int Times=decreaseColumnAboveTimes(v);
        for (int i = 0; i <Times ; i++)
            decreaseColumnAboveOne(v1);
        deleteFullRows();
    }
    public static void decreaseColumnAboveOne(Vector2 pos)             //某个方块以上的一列向下移动一行
    {
        pos = roundVec2(pos);
        int x = (int)pos.x;
        int y = (int)pos.y + 1;
        for (int i = y; i < h; ++i)
        {
            if (grid[x, i] != null )
            {
                if (!Grid.grid[x, i].parent.GetComponent<Group>().enabled)
                {
                    grid[x, i - 1] = grid[x, i];
                    grid[x, i] = null;
                    grid[x, i - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    public static int decreaseColumnAboveTimes(Vector2 v)           //某个方块以上的一列向下移动一行的次数(取决于这个方块上下的空白块数量)
    {
        int i = 0;
        Vector2 temp = v;
        while (insideBorder(v))
        {
            if (grid[(int)v.x, (int)v.y] == null)
            {
                v += new Vector2(0, -1);
                i++;
            }
            else
                break;
        }
        while (temp.y<=20)
        {
            if (grid[(int)temp.x, (int)temp.y] == null)
            {
                temp += new Vector2(0, 1);
                i++;
            }
            else
                break;
        }
        return i-1;
    }
    public static Vector2 lowestBlank(Vector2 v)                //某方快最底下的空的位置
    {
        while (insideBorder(v))
        {
            if (grid[(int)v.x, (int)v.y] == null)
                v += new Vector2(0, -1);
            else
                break;
        }
        return (v+=new Vector2(0, 1));

    }

    public static void allMoveLeft()                                //全部左移
    {
        for (int i=0; i < h; i++)
        {
            MoveLeft(i);
        }
    }

    public static void MoveLeft(int curheight)                            //一行左移
    {
        for (int i=0; i < w; ++i)
        {
            if (grid[i, curheight] == null)
            {
                for (int j = i + 1; j < w; j++)
                {
                    if (grid[j, curheight])
                    {
                        if (!grid[j, curheight].parent.GetComponent<Group>().enabled)
                        {
                            grid[i, curheight] = grid[j, curheight];
                            grid[j, curheight] = null;
                            grid[i, curheight].position += new Vector3(-(j - i), 0, 0);
                        }
                        break;
                    }
                }
            }
        }
    }

    public static void allMoveDown()                                //全部下移
    {
        for (int i = 0; i < w; i++)
        {
            MoveDown(i);
        }
    }

    public static void MoveDown(int curWidth)
    {
        for (int i = 0; i < h; ++i)
        {
            if (grid[curWidth, i] == null)
            {
                for (int j = i + 1; j < h; j++)
                {
                    if (grid[curWidth, j])
                    {
                        if (!grid[curWidth, j].parent.GetComponent<Group>().enabled)
                        {
                            grid[curWidth, i] = grid[curWidth, j];
                            grid[curWidth, j] = null;
                            grid[curWidth, i].position += new Vector3(0, -(j - i),0);
                        }
                        break;
                    }
                }
            }
        }
    }
}
