using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] groups;
    public static bool canDrop = true;
    GameObject isDrop, nextDrop;
    public static Spawner instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame

    public void setNextDrop(GameObject GO)                    //赋值下一个下落的方块
    {
        Destroy(nextDrop);
        nextDrop=GO;
    }

    public void StartGame()
    {
        Grid.clear();
        Game_Manger.getInstance().Scores = 0;
        Game_Manger.getInstance().DisScores();
        Game_Manger.getInstance().CurrentPet();
        if (nextDrop)
            Destroy(nextDrop);
        int i = Random.Range(0, groups.Length);
        isDrop = Instantiate(groups[i], transform.position, Quaternion.identity) as GameObject;
        i = Random.Range(0, groups.Length);
        nextDrop = Instantiate(groups[i], new Vector2(10.5f, 15.5f), Quaternion.identity) as GameObject;
        nextDrop.transform.localScale = new Vector2(0.5f, 0.5f);
        nextDrop.GetComponent<Group>().enabled = false;
    }
    public void spawnNext()
    {
        if (canDrop)
        {
            nextDrop.transform.localScale = new Vector2(1f, 1f);
            isDrop = nextDrop;
            isDrop.transform.position = transform.position;
            isDrop.GetComponent<Group>().enabled = true;
            int i = Random.Range(0, groups.Length);
            nextDrop = Instantiate(groups[i], new Vector2(10.5f, 15.5f), Quaternion.identity) as GameObject;
            nextDrop.transform.localScale = new Vector2(0.5f, 0.5f);
            nextDrop.GetComponent<Group>().enabled = false;
        }
        else
            Skill_Meteor.instance.DropMeteor();
    }


}
