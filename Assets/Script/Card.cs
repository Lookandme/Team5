using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string idx = null;

    public GameObject front;
    public GameObject back;

    public Animator anim;
    public SpriteRenderer frontImage;

    AudioSource audioSource;
    public AudioClip clip;

    public ParticleSystem Pt;

    //Button button1;
    //Button button2;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setting(string name)
    {
        idx = name;
        frontImage.sprite = Resources.Load<Sprite>(idx);
    }

    public void OpenCard()
    {
        if (GameObject.Find("Canvas").transform.Find("Clear").gameObject.activeSelf == false && GameObject.Find("Canvas").transform.Find("Fail").gameObject.activeSelf == false && anim.GetBool("isOpen") == false)
        {
            audioSource.PlayOneShot(clip);
            anim.SetBool("isOpen", true);
            Invoke("CardState", 0.4f);

            if (GameManager.Instance.firstCard == null)
            {
                GameManager.Instance.firstCard = this;
                //button1 = FindButton();
            }
            else
            {
                GameManager.Instance.secondCard = this;
                //button2 = FindButton();
                GameManager.Instance.Matched();
            }
        }
    }
    public Button FindButton()
    {
        Button button = this.gameObject.transform.
            Find("Back").
            GetChild(0).
            GetChild(0).
            GetComponent<Button>();
        button.enabled = false;


        return button;
    }

    public void DestoryCard()
    {
        Invoke("PlayPt", 0.2f);
        Invoke("DestroyCardInvoke", 1.0f);
    }

    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);

        //if (button1 != null) button1.enabled = true;
        //if (button2 != null) button2.enabled = true;
    }
    public void CardState()
    {
        front.SetActive(true);
        back.SetActive(false);
    }

    public void PlayPt()
    {
        Pt.Play();
    }
}

