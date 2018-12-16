using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public enum UIState
{
    Main = 0,
    Producter = 1,
    Upgrade = 2,
    Skilling = 3,
    Petting = 4,
    Choosing = 5,
    Gaming=6,
    Gold=7,
}

public class UI_Manger : MonoBehaviour {

    public static UI_Manger Instance;
    public  List<GameObject> pages;
    public List<GameObject> petpages;               //宠物升级界面
    public List<GameObject> petchoosepages;          //宠物选择界面
    private UIState initstate = UIState.Main;
    private int initpet = 0;                        //初始的宠物升级界面
    private int initpetchoose = 0;                  //初始的宠物选择界面

    public UIState currentState;
    private int currentPet;                          //当前的宠物升级界面
    private int currentPetChoose;                       //当前的宠物选择界面

	void Start () {
        Instance = this;
        for (int i = 0; i < pages.Count; i++)               //初始化UI界面
        {

            if (i == (int)initstate||i==(int)UIState.Gold)
                pages[i].SetActive(true);
            else
                pages[i].SetActive (false);
        }
        currentState = initstate;

        for (int i = 0; i < petpages.Count; i++)            //初始化宠物UI界面
        {
            if (i == initpet)
                petpages[i].SetActive(true);
            else
                petpages[i].SetActive(false);
        }
        currentPet = initpet;

        for (int i = 0; i < petchoosepages.Count; i++)            //初始化宠物UI界面
        {
            if (i == initpetchoose)
                petchoosepages[i].SetActive(true);
            else
                petchoosepages[i].SetActive(false);
        }
        currentPetChoose = initpetchoose;

        //foreach (var item in pages)
        //{
        //    UI_SetResolution.SetAResolution(item.transform);
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PageTransition(UIState newState , bool preserve=false)            //主UI界面的转化
    {
        if (newState == currentState)
            return;
        pages[(int)currentState].SetActive(preserve);
        pages[(int)newState].SetActive(true);
        currentState = newState;
        if ((int)currentState !=7)           //设置金币是否显示
            pages[(int)UIState.Gold].SetActive(true);
        else
            pages[(int)UIState.Gold].SetActive(false);
    }

    public void PetPageTransition(string direction)            //升级宠物UI界面的转化
    {
        if (direction == "left")
        {
            petpages[(currentPet+petpages.Count-1)%petpages.Count].SetActive(true);
            petpages[currentPet].SetActive(false);
            currentPet = (currentPet +petpages.Count- 1) % petpages.Count;
        }
        if (direction == "right")
        {
            petpages[(currentPet + 1) % petpages.Count].SetActive(true);
            petpages[currentPet].SetActive(false);
            currentPet = (currentPet + 1) % petpages.Count;
        }
    }

    public void PetPageChooseTransition(string direction)            //选择宠物UI界面的转化
    {
        if (direction == "left")
        {
            petchoosepages[(currentPetChoose + petchoosepages.Count - 1) % petchoosepages.Count].SetActive(true);
            petchoosepages[currentPetChoose].SetActive(false);
            currentPetChoose = (currentPetChoose + petchoosepages.Count - 1) % petchoosepages.Count;
        }
        if (direction == "right")
        {
            petchoosepages[(currentPetChoose + 1) % petchoosepages.Count].SetActive(true);
            petchoosepages[currentPetChoose].SetActive(false);
            currentPetChoose = (currentPetChoose + 1) % petchoosepages.Count;
        }
    }
}
