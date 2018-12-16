using UnityEngine;
using System.Collections;

public class Skill_AddGrid : Skill
{
    void Start()
    {
        this.GetComponent<use_add_skill>().enabled = false;
        level = Date_Manger.mydate_Instance.skill_add_lv;
        upCost = 10000 * (level + 1);
        lighting();
    }

    public override void levelup()
    {
        base.levelup();
        Date_Manger.mydate_Instance.skill_add_lv = level;
        Date_Manger.Save();
    }
    public override void myAbility()
    {
        use_add_skill.useTimes = level;
        this.GetComponent<use_add_skill>().enabled = true;
    }
}