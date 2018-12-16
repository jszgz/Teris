using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pet : MonoBehaviour {

    static public bool hasFight = false;        //用于记录是否已有宠物出战（确保只有一只出战）
    protected int level;                      //宠物等级
    protected string pet_name;                      //宠物名字
    protected int price;                    //购买价格
    protected int curExp;                   //当前经验值
    protected int maxExp;                   //升级所需经验值
    protected int eachExp;                  //每次饲养得到的经验值
    public float CD;                     //宠物发动技能的冷却时间
    public bool isFight=false;                 //是否出战
    public Sprite petSqr;                   //宠物头像
    public GameObject expScroll;                //经验条图片
    public Sprite FightSpr;                 //出战按钮图片
    public Sprite noFightSpr;               //未出战按钮图片
    public Button FightBton;                //出战按钮
    public GameObject PetGamingSpe;         //游戏中的宠物图片显示

    public virtual void UpExp()                     //饲养升级的经验
    {
        curExp += eachExp;
        ChangeExpScroll();
        if (curExp >= maxExp)
        {
            UpLevel();
        }
    }

    public void UpLevel()                   //升级
    {
        curExp = curExp - maxExp;
        level++;
        maxExp *= level;
    }

    public void ChangeExpScroll()               //改变经验条
    {
            expScroll.GetComponent<Image>().fillAmount = (float)curExp / (float)maxExp;
    }

    public void IsFight()                       //是否出战
    {
        if (isFight)
        {
            isFight = false;
            hasFight = false;
        }
        else if (!isFight && !hasFight)
        {
            isFight =true;
            hasFight = true;
        }

        UsingFightPet();
    }

    public void UsingFightPet()
    {
        if (isFight)
        {
            PetGamingSpe.GetComponent<Image>().sprite = petSqr;
            FightBton.GetComponent<Image>().sprite = FightSpr;
        }
        else
        {
            PetGamingSpe.GetComponent<Image>().sprite = null;
            FightBton.GetComponent<Image>().sprite = noFightSpr;
        }
    }
    public virtual void Ability() { }
}
