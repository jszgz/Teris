using UnityEngine;
using System.Collections;

public class Pet_Elephant : Pet {

    public static Pet_Elephant instance;
    void Start()
    {
        instance = this;
        name = "Elephant";
        curExp = Date_Manger.mydate_Instance.pet_elephant_curExp;
        level = Date_Manger.mydate_Instance.pet_elephant_lv+1;
        maxExp = level * 50;
        eachExp = 10;
        ChangeExpScroll();
        if (level == 0)
            CD = 0;
        else
            CD = 20 / level;

    }

    public override void UpExp()
    {
        base.UpExp();
        Date_Manger.mydate_Instance.pet_elephant_curExp = curExp;
        Date_Manger.mydate_Instance.pet_elephant_lv = level;
        Date_Manger.Save();
    }

    public override void Ability()
    {
        Grid.allMoveDown();
    }
}
