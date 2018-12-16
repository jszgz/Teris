using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Skill : MonoBehaviour {

    public Sprite mySprite;               //技能ui
    protected int level;                  //初始等级
    protected int maxlevel=3;               //最高等级
    protected string skill_name;            //技能名称
    protected int upCost;                   //升级花费
    protected bool hasChoose;               //是否被选用
    private static int canChooseNum=3;                  //可以选择的技能数量
    public Text myText;                  //等级文本框
    static public ArrayList SprList=new ArrayList();        //技能图标
    public GameObject[] skill_light;                       //技能灯的UI
    public Sprite light_sprite;                        //技能灯的图片
    public GameObject[] skill_texture;
    public GameObject[] skill_button;
    private Vector2[] skill_pos = { new Vector2(-180, -408), new Vector2(-45, -408), new Vector2(84, -408) };

    public virtual void levelup()
    {
        if (level <3&&Game_Manger.getInstance().Gold>=upCost)
        {
            level++;
            lighting();
            Game_Manger.getInstance().CostGold(upCost);
            upCost = 10000 * (level + 1);
            myText.text=level+"级";
        }
    }

    public void lighting()
    {
        for(int i=0;i<level;i++)
            skill_light[i].transform.GetComponent<Image>().sprite = light_sprite;
    }

    public void ClickSkill()
    {
        if (!hasChoose&&SprList.Count<canChooseNum)
        {
            SprList.Add(mySprite);
            skill_button[SprList.IndexOf(mySprite)].GetComponent<Button>().onClick.AddListener(delegate()
            {
                this.myAbility();
            });
            skill_button[SprList.IndexOf(mySprite)].transform.GetComponent<Image>().sprite = mySprite;
            hasChoose = true;
            UseSkill();
        }
        else
        {
            if (SprList.Contains(mySprite))
            {
                skill_button[SprList.IndexOf(mySprite)].GetComponent<Button>().onClick.RemoveAllListeners();
                skill_button[SprList.IndexOf(mySprite)].transform.GetComponent<Image>().sprite = null;
                SprList.Remove(mySprite);
                hasChoose = false;
                UseSkill();
            }
        }
    }

    public void UseSkill()
    {
        int i = 0;
        foreach(Sprite myspr in SprList){
            skill_texture[i].transform.GetComponent<Image>().sprite = myspr;
            i++;
        }
        while (i < 3) 
        { 
            skill_texture[i].transform.GetComponent<Image>().sprite = null;
            i++;
        }
        int p1=0;
        int p2=2;
        foreach (GameObject go in skill_button)
        {
            if (go.GetComponent<Image>().sprite != null)
            {
                go.transform.localPosition = skill_pos[p1];
                p1++;
            }
            else
            {
                go.transform.localPosition = skill_pos[p2];
                p2--;
            }
        }
    }
    public virtual void  myAbility(){

    }

	
}
