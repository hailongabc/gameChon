using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject ansItem;
    [SerializeField] private GameObject answer;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image img;
    [SerializeField] private List<string> arrAns = new List<string>(4) { "-1", "-1", "-1", "-1" };
    public Sprite Sp;
    public Sprite SpHover;
    public string AnswerChoose;
    public List<GameObject> ListAns;
    [SerializeField] private GameObject tempGO;
    Answer m_answer;
    private string JsonData;
    [SerializeField] private List<string> right_ans;
    public bool checkClick;
    public class Info
    {
        public List<string> answers;
        public string title;
        public string question;
        public List<string> right_answer;
        public bool click;
    }

    void Start()
    {
        Debug.Log("zxczxczx");
        m_answer = FindObjectOfType<Answer>();
        JsonData = File.ReadAllText(@"C:\Users\USER\AppData\LocalLow\KBF3\DGPCNL\Playables\toankntt\quyen1\tuan25\onluyen1\cau21hihi\TPL6_02.PL21\data.json");

        Info info = JsonUtility.FromJson<Info>(JsonData);
        checkClick = info.click;
        Instantiate(top, canvas.transform);

        right_ans = info.right_answer;

        for (int i = 0; i < info.answers.Count; i++)
        {
            GameObject go = Instantiate(ansItem, answer.transform);
            ListAns.Add(go);
            Texture2D spritesa = LoadPNG(@"C:\Users\USER\AppData\LocalLow\KBF3\DGPCNL\Playables\toankntt\quyen1\tuan25\onluyen1\cau21hihi\TPL6_02.PL21\" + info.answers[i] + ".png");
            Rect reca = new Rect(0, 0, spritesa.width, spritesa.height);
            Sprite.Create(spritesa, reca, new Vector2(0, 0), 1);
            ListAns[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(spritesa, reca, new Vector2(0, 0), .01f);
            ListAns[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name = info.answers[i];

        }
        Texture2D spritesQ = LoadPNG(@"C:\Users\USER\AppData\LocalLow\KBF3\DGPCNL\Playables\toankntt\quyen1\tuan25\onluyen1\cau21hihi\TPL6_02.PL21\" + info.question + ".png");
        Rect recQ = new Rect(0, 0, spritesQ.width, spritesQ.height);
        Sprite.Create(spritesQ, recQ, new Vector2(0, 0), 1);
        img.sprite = Sprite.Create(spritesQ, recQ, new Vector2(0, 0), .01f);

        for (int i = 0; i < ListAns.Count; i++)
        {

            ListAns[i].GetComponent<Answer>().index = i;
        }

    }

    public void Shuffle()
    {
        for (int i = 0; i < ListAns.Count; i++)
        {
            int rnd = Random.Range(0, ListAns.Count);
            tempGO = ListAns[rnd];
            ListAns[rnd] = ListAns[i];
            ListAns[i] = tempGO;
        }
    }
    private static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;
        if (System.IO.File.Exists(filePath))
        {
            fileData = System.IO.File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    public void checkClickAns(int index)
    {
        if (!checkClick)
        {
            for (int i = 0; i < ListAns.Count; i++)
            {
                ListAns[i].transform.GetChild(0).GetComponent<Image>().sprite = Sp;
                arrAns.Clear();
            }
            ListAns[index].transform.GetChild(0).GetComponent<Image>().sprite = SpHover;
            arrAns.Add(ListAns[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name);
            //AnswerChoose = ListAns[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name;
        }
        else
        {

            if (ListAns[index].transform.GetChild(0).GetComponent<Image>().sprite == SpHover)
            {
                ListAns[index].transform.GetChild(0).GetComponent<Image>().sprite = Sp;
                arrAns[index] = "-1";
            }
            else
            {
                ListAns[index].transform.GetChild(0).GetComponent<Image>().sprite = SpHover;
                arrAns[index] = ListAns[index].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite.name;
            }
        }
    }
    public void NopBai()
    {
        if (!checkClick)
        {
            if (arrAns[0] == right_ans[0])
                Debug.Log("chon 1 true ");
            else Debug.Log("chon 1 false ");
        }
        else
        {
            arrAns.RemoveAll(e => e.StartsWith("-1"));
            for (int i = 0; i < right_ans.Count; i++)
            {
                if (arrAns[i] == right_ans[i])
                    Debug.Log("chon nhieu true");
                else Debug.Log("chon nhieu false");
            }
        }
      
    }
}
