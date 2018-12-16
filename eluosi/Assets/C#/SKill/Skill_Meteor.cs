using UnityEngine;
using System.Collections;

public class Skill_Meteor : Skill
{
    public static Skill_Meteor instance;
    public GameObject star;
    public int StartNum = 5;
    void Start()
    {
        instance = this;
        level = Date_Manger.mydate_Instance.skill_meteor_lv;
        upCost = 10000 * (level + 1);
        lighting();
    }

    public override void levelup()
    {
        base.levelup();
        Date_Manger.mydate_Instance.skill_meteor_lv = level;
        Date_Manger.Save();
    }
    public override void myAbility()
    {
        Spawner.canDrop = false;
    }

    public  void DropMeteor()
    {
        StartCoroutine(doingAbility());
    }

    IEnumerator doingAbility()
    {
        for (int i = 0; i < level; i++)
        {
            for (int j = 0; j < StartNum; j++)
            {
                yield return new WaitForSeconds(0.1f);
                Instantiate(star, new Vector2(Random.Range(0, 10), 22), Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);
        }
        Spawner.canDrop = true;
        Spawner.instance.spawnNext();
    }
}