using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    public string buttonImageLocationFloder;
    public string[] buttonImageName;
    public GameObject[] panels;
    public Image[] panelButtonImage;

    // Use this for initialization
    void Start () {
        //CHANGE PANEL TO THE FIRST OF THE LIST
        ChangePanel(0);
    }

    public void ChangePanel(int index)
    {
        CloseAllPanel();
        OpenPanel(index);
        ChangeButton(index);
    }

    void CloseAllPanel()
    {
        foreach (GameObject g in panels)
        {
            g.SetActive(false);
        }
    }

    void OpenPanel(int index)
    {   
         panels[index].SetActive(true);     
    }

    void ChangeButton(int index)
    {
        ChangeAllButtonToNormal();      
        panelButtonImage[index].overrideSprite = Resources.Load<Sprite>(buttonImageLocationFloder+"/" + buttonImageName[index]);
       
        ChangeButtonImageDimensionToNormal();
    }

    void ChangeAllButtonToNormal()
    {
        foreach (Image i in panelButtonImage)
        {
            i.overrideSprite = null;
        }
    }

    void ChangeButtonImageDimensionToNormal()
    {
        foreach (Image i in panelButtonImage)
        {
            i.SetNativeSize();
        }
    }


}
