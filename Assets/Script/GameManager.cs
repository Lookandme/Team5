using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public GameObject matchBoard;
    public SpriteRenderer matchingImage;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip MatchClip;
    public AudioClip FailClip;
    public AudioClip GameOverClip;
    public AudioClip ClearClip;




    public int cardCount = 0;
    float time = 0.0f;
    float maxTime = 0.0f;
    float showTime = 0.0f;
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
            nameTxt.text = "김경구";
        }
        else if (savename == "Lee")
        {
            nameTxt.text = "이승환";
        }
        else if (savename == "Son")
        {
            nameTxt.text = "손대오";
        }
        else if (savename == "Park")
        {
            nameTxt.text = "박상규";
        }
        else if (savename == "Hidden")
        {
            nameTxt.text = "튜트리얼";
        }
        if (savename == "Hidden") { maxTime = 60.0f; showTime = 0.7f;}
        else { maxTime = 30.0f; showTime = 1f;}

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "NameScene")
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        
            if (time >= maxTime)
            {
                audioSource.PlayOneShot(GameOverClip);
                Debug.Log("끝났어요");
                timeTxt.text = "30.0";
                Time.timeScale = 0.0f;
                Fail.SetActive(true);
            }
         }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(MatchClip);
            Debug.Log("잘했어요");
            matchingImage.sprite = Resources.Load<Sprite>(ImageChange(firstCard.idx));
            matchBoard.SetActive(true);
            Invoke("CloseMatchingImage", showTime);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;
            if (cardCount == 0)
            {
                audioSource.PlayOneShot(ClearClip);
                Debug.Log("이겼어요");
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
                else if(savename == "Hidden")
                {
                    SceneManager.LoadScene("EndScene");
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(FailClip);
            Debug.Log("졌어요");
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public void CloseMatchingImage()
    {
        matchBoard.SetActive(false);
    }

    public string ImageChange(string imageName)
    {
        char[] imageNameChar = imageName.ToCharArray();
        char[] strTester = imageNameChar;
        int x = 0;
        for (int i = 0;i<strTester.Length; i++)
        {
            if (int.TryParse(strTester[i].ToString(), out x))
            {
                strTester[i] = '8';
            }
        }
        string resultImageName = new string(strTester);
        return resultImageName;
    }
}