using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MasterBtn : MonoBehaviour
{
    public Text NameTxt;
    public GameObject OptionCanvas;
    public void StartBtn()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OptionBtn()
    {
        if (OptionCanvas.activeSelf == false)
        {
            OptionCanvas.SetActive(true);
        }
        else
        {
            OptionCanvas.SetActive(false);
        }
    }
    //Challenge
    public void NameSceneBtn(string name)
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            SaveManager.instance.name = name;
            SceneManager.LoadScene("NameScene");
        }
        else
        {
            SceneManager.LoadScene("NameScene");
        }
    }
    public void EndBtn()
    {
        SceneManager.LoadScene("EndScene");
    }

}
