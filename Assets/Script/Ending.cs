using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject Title;
    public GameObject Kim;
    public GameObject Son;
    public GameObject Park;
    public GameObject Lee;
    public GameObject Team;

    

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 6; i++)
        {
            Invoke($"Name{i}",  i);
        }
        
    }

        // Update is called once per frame
    void Update()
    {
        
    }
    void Name0()
    {
        Title.SetActive(true);
    }

    void Name1()
    {
        Kim.SetActive(true);
    }
    void Name2()
    {
        Park.SetActive(true);
    }
    void Name3()
    {
        Son.SetActive(true);
    }
    void Name4()
    {
        Lee.SetActive(true);
    }
    void Name5()
    {
        Team.SetActive(true);
    }


}
