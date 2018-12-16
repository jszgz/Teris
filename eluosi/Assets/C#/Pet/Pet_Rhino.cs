using UnityEngine;
using System.Collections;

public class Pet_Rhino : Pet {

    public static Pet_Rhino instance;
    void Start()
    {
        instance = this;
        name = "Rhino";
        curExp = Date_Manger.mydate_Instance.pet_rhino_curExp;
        level = Date_Manger.mydate_Instance.pet_rhino_lv+1;
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
        Date_Manger.mydate_Instance.pet_rhino_curExp = curExp;
        Date_Manger.mydate_Instance.pet_rhino_lv = level;
        Date_Manger.Save();
    }

    public override void Ability()
    {
        Grid.allMoveLeft();

    }

}
