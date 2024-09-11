using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MasterBtn : MonoBehaviour
{
    public Text NameTxt;
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
        //
    }
    //Challenge
    public void NameSceneBtn()
    {
        SceneManager.LoadScene("NameScene");
    }
    public void EndBtn()
    {
        SceneManager.LoadScene("EndScene");
    }

}
