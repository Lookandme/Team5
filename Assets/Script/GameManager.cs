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
    public AudioClip MatchClip;
    public AudioClip FailClip;
    public AudioClip GameOverClip;
    public AudioClip ClearClip;




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
            nameTxt.text = "��汸";
        }
        else if (savename == "Lee")
        {
            nameTxt.text = "�̽�ȯ";
        }
        else if (savename == "Son")
        {
            nameTxt.text = "�մ��";
        }
        else if (savename == "Park")
        {
            nameTxt.text = "�ڻ��";
        }
        else if (savename == "Hidden")
        {
            nameTxt.text = "ƩƮ����";
        }


    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time >= 30.0f)
        {
            audioSource.PlayOneShot(GameOverClip);
            Debug.Log("�������");
            timeTxt.text = "30.0";
            Time.timeScale = 0.0f;
            Fail.SetActive(true);
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(MatchClip);
            Debug.Log("���߾��");
            MatchingImage.SetActive(true);
            Invoke("CloseMatchingImage", 1f);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;
            if (cardCount == 0)
            {
                audioSource.PlayOneShot(ClearClip);
                Debug.Log("�̰���");
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
            audioSource.PlayOneShot(FailClip);
            Debug.Log("�����");
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