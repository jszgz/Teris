using UnityEngine;
using System.Collections;

public class Pet_Snake : Pet {

    public static Pet_Snake instance;
    public GameObject LSquare;

	void Start () {
        instance = this;
        name = "Snake";
        curExp = Date_Manger.mydate_Instance.pet_snake_curExp;
        level = Date_Manger.mydate_Instance.pet_snake_lv+1;
        maxExp = level*50;
        eachExp = 10;
        ChangeExpScroll();
        if(level==0)
            CD = 0;
        else
            CD = 20 / level;
        
	}

    public override void UpExp()
    {
        base.UpExp();
        Date_Manger.mydate_Instance.pet_snake_curExp = curExp;
        Date_Manger.mydate_Instance.pet_snake_lv = level;
        Date_Manger.Save();
    }
	
    public override void Ability()
    {
        GameObject tempGO = Instantiate(LSquare, new Vector2(10,2), Quaternion.identity) as GameObject;
        tempGO.transform.localScale = new Vector2(0.5f, 0.5f);
        tempGO.GetComponent<Group>().enabled = false;
        StartCoroutine(MoveToPosition(tempGO));  
    }

    IEnumerator MoveToPosition(GameObject GO)
    {
        while (GO.transform.position.y < 15.5f)
        {
            GO.transform.position = Vector3.MoveTowards(GO.transform.position, new Vector2(10.5f, 15.5f), 10 * Time.deltaTime);
            yield return 0;
        }
        Spawner.instance.setNextDrop(GO);
    }  
}
