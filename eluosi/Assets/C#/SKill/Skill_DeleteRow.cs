using UnityEngine;
using System.Collections;

public class Skill_DeleteRow : Skill {

    void Start()
    {
        level = Date_Manger.mydate_Instance.skill_delete_lv;
        upCost = 10000 * (level + 1);
        lighting();
    }
    public override void levelup()
    {
        base.levelup();
        Date_Manger.mydate_Instance.skill_delete_lv = level;
        Date_Manger.Save();
    }
    public override void myAbility()
    {
        for (int i = 0; i < level; i++)
        {
            int row = Random.Range(0, Grid.highestRow());
            if (row != -1)
            {
                Grid.deleteRow(row);
                Grid.decreaseRowsAbove(row + 1);
            }
        }        
    }
}
