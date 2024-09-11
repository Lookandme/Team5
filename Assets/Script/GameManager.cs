using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public Text nameTxt;
    public GameObject Clear;
    public GameObject Fail;
    public GameObject MatchingImage;

    AudioSource audioSource;
    public AudioClip clip;

    public int cardCount = 0;
    float time = 0.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        string savename = SaveManager.instance.name.ToString();
        if (savename == "Kim" && SceneManager.GetActiveScene().name == "NameScene")
        {
            nameTxt.text = "±è°æ±¸";
        }
        else if (savename == "Lee")
        {
            nameTxt.text = "ÀÌ½ÂÈ¯";
        }
        else if (savename == "Son")
        {
            nameTxt.text = "¼Õ´ë¿À";
        }
        else if (savename == "Park")
        {
            nameTxt.text = "¹Ú»ó±Ô";
        }
        else if (savename == "Hidden")
        {
            nameTxt.text = "Æ©Æ®¸®¾ó";
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "NameScene")
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
            if (time >= 30.0f)
            {
                timeTxt.text = "30.0";
                Time.timeScale = 0.0f;
                Fail.SetActive(true);
            }
        }
    }
    
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            MatchingImage.SetActive(true);
            Invoke("CloseMatchingImage", 1f);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;
            if(cardCount == 0 )
            {
                Time.timeScale = 0.0f;
                Clear.SetActive(true);

                string savename = SaveManager.instance.name;
                if (savename == "Kim")
                {
                    SaveManager.instance.Kim = true;
                }
                else if (savename == "Park")
                {
                    SaveManager.instance.Park = true;
                }
                else if (savename == "Son")
                {
                    SaveManager.instance.Son = true;
                }
                else if (savename == "Lee")
                {
                    SaveManager.instance.Lee = true;
                }
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public void CloseMatchingImage()
    {
        MatchingImage.SetActive(false);
    }
}
