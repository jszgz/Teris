using UnityEngine;
using System.Collections;

public class Skill_Boom : Skill
{
    public void Start()
    {
        this.GetComponent<use_boom_skill>().enabled = false;
        level = Date_Manger.mydate_Instance.skill_boom_lv;
        upCost = 10000 * (level + 1);
        lighting();
    }

    public override void levelup()
    {
        base.levelup();
        Date_Manger.mydate_Instance.skill_boom_lv = level;
        Date_Manger.Save();
    }
    public override void myAbility()
    {
        use_boom_skill.boomeffect = level;
        this.GetComponent<use_boom_skill>().enabled = true;
    }
}