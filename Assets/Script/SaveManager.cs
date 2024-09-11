using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public string name;

    public bool Kim = false;
    public bool Son = false;
    public bool Park = false;
    public bool Lee = false;

    public GameObject hidden;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        { 
            if(Kim && Park && Son && Lee)
            {
                GameObject.Find("Canvas").transform.Find("Hidden").gameObject.SetActive(true);
            }
        }        
    }
    
    public void Debuglog()
    {
        Debug.Log(Kim);
    }
}
