using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Game_Manger : MonoBehaviour {
    public int Scores=0;
    public int Gold;
    public Text Text_Scores;
    public Text Text_Gold_normal;
    private static Game_Manger game_instance;
    public GameObject AccountPanel;
    public Text Text_finalscores;
    public Text Text_Gold;
    public double SkillMultiple;                    //greed技能增加的游戏倍数
    public bool hasStart;                           //是否开始游戏
    private Pet curPet;                                    //游戏中使用的宠物
    private float lastUsePSkill;                                   //上一次发动宠物技能的时间


    void Start()
    {
        game_instance = this;
        Gold = Date_Manger.mydate_Instance.Gold;
        if (Gold == 0)
            Gold = 100000;
        DisGold();

    }
    private Game_Manger() { }
    public static  Game_Manger getInstance()
    {
        return game_instance;
    }

    void Update()
    {
        if (Pet.hasFight&&hasStart)
        {
            if (Time.time - lastUsePSkill > curPet.CD)
            {
                curPet.Ability();
                lastUsePSkill = Time.time;
            }
        }
    }

    public void AddScores(int sco)
    {
        Scores += sco;
        DisScores();
    }
    public void DisScores()
    {
        Text_Scores.text = "" + Scores;
    }

    public void DisGold()                                       //显示金币
    {   
        Text_Gold_normal.text =  ""+Gold;
    }
    public void AddGold()                                       //增加金币
    {
        Gold += (int)((Scores/2)*SkillMultiple);
        Date_Manger.mydate_Instance.Gold = Gold;
        Date_Manger.Save();
        OpenAccount();
    }

    public void CostGold(int cost)                              //消耗金币
    {
        Gold -= cost;
        DisGold();
        Date_Manger.mydate_Instance.Gold = Gold;
        Date_Manger.Save();
    }

    public void OpenAccount()                               //游戏结算
    {
        AccountPanel.SetActive(true);
        Text_finalscores.text = "分数:" + Scores;
        Text_Gold.text = "金币：" + Gold;
    }

    public void CloseAccount()
    {
        AccountPanel.SetActive(false);
    }

    public void CurrentPet()                            //获得当前出战的宠物
    {
        if (Pet_Snake.instance.isFight)
            curPet = Pet_Snake.instance;
        if (Pet_Rhino.instance.isFight)
            curPet = Pet_Rhino.instance;
        if (Pet_Elephant.instance.isFight)
            curPet = Pet_Elephant.instance;
        lastUsePSkill = Time.time;
        hasStart = true;
    }
}
