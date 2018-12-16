using UnityEngine;
using System.Collections;

public class Group : MonoBehaviour
{

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            //not indide border?

            if (!Grid.insideBorder(v))
                return false;

            //block in grid cell(and not part of same group)
            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        //remove old child from grid
        for (int y = 0; y < Grid.h; ++y)
        {
            for (int x = 0; x < Grid.w; ++x)
            {
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;
            }
        }
        //add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
    // Use this for initialization
    void Start()
    {
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Game_Manger.getInstance().AddGold();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame




    // Time since last gravity tick
    float lastFall = 0;

    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetMouseButtonUp(0))
        {
            float x = 0.5f;
            float y = 0.5f;
            foreach (Transform child in transform)
            {
                Vector2 pos = child.localPosition;
                float m;
                m = pos.x;
                pos.x = x - y + pos.y;
                pos.y = x + y - m;
                child.transform.localPosition = pos;
            }

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    if (isValidGridPos())
                    {
                        updateGrid();
                        return;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    transform.position += new Vector3(1, 0, 0);
                    if (isValidGridPos())
                    {
                        updateGrid();
                        return;
                    }
                }
                transform.position += new Vector3(-1, 0, 0);
                transform.position += new Vector3(-1, 0, 0);
                foreach (Transform child in transform)
                {
                    Vector2 pos = child.localPosition;
                    float m;
                    m = pos.x;
                    pos.x = x + y - pos.y;
                    pos.y = y-x + m;
                    child.transform.localPosition = pos;
                }
            }
        }

        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }

        else if(Input.GetKey(KeyCode.DownArrow)){
            if (Time.time - lastFall >= 0.05)
            {
                transform.position += new Vector3(0, -1, 0);
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Grid.deleteFullRows();

                    // Spawn next Group
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }

                lastFall = Time.time;
            }
        }
    }
}
