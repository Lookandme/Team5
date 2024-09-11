using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    string[] namearr = { "Kim", "Park", "Son", "Lee" };
    int[] numarr = { 0, 1, 2, 3, 4, 5, 6, 7 };
    string[] arr = new string[16];
    int saveRandom = 0;
    // Start is called before the first frame update
    void Start()
    {
        string savenameBoard = SaveManager.instance.name.ToString();
        if (savenameBoard == "Hidden")
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 4 == 0) { numarr[i / 2] = Random.Range(0, 8); }
                else if (i % 4 == 2)
                {
                    saveRandom = Random.Range(numarr[i / 2 - 1] + 1, numarr[i / 2 - 1] + 8);
                    numarr[i / 2] = saveRandom % 7 - saveRandom / 7;
                }
                arr[i] = namearr[i / 4] + numarr[i / 2].ToString();
            }
        }
        else
        {
            for (int i = 0; i < arr.Length; i++) { arr[i] = savenameBoard + numarr[i / 2].ToString(); }
        }
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for(int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4)*1.4f -2.1f;
            float y = (i / 4)*1.4f -4.0f;

            go.transform.position = new Vector2(x,y);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount = arr.Length;
    }
}
