using UnityEngine;
using System.Collections;

public class Button_Control : MonoBehaviour {

    public void OnPress_Start()
    {
        UI_Manger.Instance.PageTransition(UIState.Upgrade);
    }

    public void OnPress_Producter()
    {
        UI_Manger.Instance.PageTransition(UIState.Producter,true);
    }

    public void OnPress_UpSkill()
    {
        UI_Manger.Instance.PageTransition(UIState.Skilling);
    }

    public void OnPress_UpPet()
    {
        UI_Manger.Instance.PageTransition(UIState.Petting);
    }


    public void OnPress_Choose()
    {
        UI_Manger.Instance.PageTransition(UIState.Choosing);
    }

    public void OnPress_RMain()
    {
        UI_Manger.Instance.PageTransition(UIState.Main);
    }
    public void OnPress_RUpgrade()
    {
        UI_Manger.Instance.PageTransition(UIState.Upgrade);
    }

    public void OnPress_RChoosing()
    {
        UI_Manger.Instance.PageTransition(UIState.Choosing);
        Game_Manger.getInstance().hasStart= false;
    }

    public void OnPress_Gaming()
    {
        UI_Manger.Instance.PageTransition(UIState.Gaming);
    }

    public void OnPress_Quit()
    {
        Application.Quit();
    }

    public void OnPress_PetLeft()
    {
        UI_Manger.Instance.PetPageTransition("left");
            
    }
    public void OnPress_PetRight()
    {
        UI_Manger.Instance.PetPageTransition("right");
    }

    public void OnPress_PetChooseLeft()
    {
        UI_Manger.Instance.PetPageChooseTransition("left");

    }
    public void OnPress_PetChooseRight()
    {
        UI_Manger.Instance.PetPageChooseTransition("right");
    }
}
