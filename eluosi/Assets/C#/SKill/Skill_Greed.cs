using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Skill_Greed : Skill {
    void Start()
    {
        level = Date_Manger.mydate_Instance.skill_greed_lv;
        upCost = 10000 * (level + 1);
        lighting();
    }
    public override void levelup()
    {
        base.levelup();
        Date_Manger.mydate_Instance.skill_greed_lv = level;
        Date_Manger.Save();
    }
    public void OnPress_Start()
    {
        if (hasChoose)
            Game_Manger.getInstance().SkillMultiple = level * 0.25 + 1;
        else
            Game_Manger.getInstance().SkillMultiple = 1;
    }
}
