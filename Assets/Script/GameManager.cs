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
    public GameObject matchBoard;
    public SpriteRenderer matchingImage;
    public Text resultNameClear;
    public Text resultNameFail;

    AudioSource audioSource;
    public AudioClip MatchClip;
    public AudioClip FailClip;
    public AudioClip GameOverClip;
    public AudioClip ClearClip;




    public int cardCount = 0;
    bool musicset = true;
    float time = 0.0f;
    float maxTime = 0.0f;
    float showTime = 0.0f;
    string savename = SaveManager.instance.name.ToString();
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "NameScene")
        {
            if (savename == "Kim")
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
            if (savename == "Hidden") { maxTime = 60.0f; showTime = 0.7f; }
            else { maxTime = 30.0f; showTime = 1f; }
            resultNameClear.text = $"Tutor.{nameTxt.text}";
            resultNameFail.text = $"{nameTxt.text}(Unity N수)";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            if (SaveManager.instance.Kim == true)
            {
                GameObject.Find("Kim").gameObject.transform.GetComponent<Image>().color = Color.yellow;
            }
            if (SaveManager.instance.Lee == true)
            {
                GameObject.Find("Lee").gameObject.transform.GetComponent<Image>().color = Color.yellow;
            }
            if (SaveManager.instance.Son == true)
            {
                GameObject.Find("Son").gameObject.transform.GetComponent<Image>().color = Color.yellow;
            }
            if (SaveManager.instance.Park == true)
            {
                GameObject.Find("Park").gameObject.transform.GetComponent<Image>().color = Color.yellow;
            }

        }
        if (SceneManager.GetActiveScene().name == "NameScene")
        {
            if (cardCount != 0)
            {
                time += Time.deltaTime;
                timeTxt.text = time.ToString("N2");
            }

            if (time >= maxTime)
            {
                if (musicset)
                {
                     audioSource.PlayOneShot(GameOverClip);
                    musicset = false;
                }
                timeTxt.text = maxTime.ToString();
                //Time.timeScale = 0.0f;
                Fail.SetActive(true);
            }
        }
    }


    public void Matched()
{
    if (firstCard.idx == secondCard.idx)
    {
        audioSource.PlayOneShot(MatchClip);
        matchingImage.sprite = Resources.Load<Sprite>(ImageChange(firstCard.idx));
        matchBoard.SetActive(true);
        Invoke("CloseMatchingImage", showTime);
        firstCard.DestoryCard();
        secondCard.DestoryCard();
        cardCount -= 2;
        if (cardCount == 0)
        {
            audioSource.PlayOneShot(ClearClip);
            //Time.timeScale = 0.0f;
            if (savename != "Hidden") Clear.SetActive(true);

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
                Invoke("ToEnd", 3f);
            }
        }
    }
        else
        {
            audioSource.PlayOneShot(FailClip);
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

    public void ToEnd()
    {
        SceneManager.LoadScene("EndScene");
    }
}
