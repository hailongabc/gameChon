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
    [SerializeField] private List<GameObject> ListAns;
    [SerializeField] private GameObject tempGO;
    private string JsonData;
    public class Info
    {
        public List<string> answers;
        public string title;
        public string question;
        public List<string> right_answer;
    }
    void Start()
    {
        
        JsonData = File.ReadAllText(@"C:\Users\USER\AppData\LocalLow\KBF3\DGPCNL\Playables\toankntt\quyen1\tuan25\onluyen1\cau21hihi\TPL6_02.PL21\data.json");

        Info info = JsonUtility.FromJson<Info>(JsonData);

        Debug.Log("cxcxcx  " + info.answers.Count);

        Instantiate(top, canvas.transform);

        for(int i = 0; i < info.answers.Count; i++)
        {
            GameObject go = Instantiate(ansItem, answer.transform);
            ListAns.Add(go);
            Debug.Log("zxczx  " + ListAns[i]);
            Texture2D spritesa = LoadPNG(@"C:\Users\USER\AppData\LocalLow\KBF3\DGPCNL\Playables\toankntt\quyen1\tuan25\onluyen1\cau21hihi\TPL6_02.PL21\" + info.answers[i] + ".png");
            Rect reca = new Rect(0, 0, spritesa.width, spritesa.height);
            Sprite.Create(spritesa, reca, new Vector2(0, 0), 1);
            ListAns[i].GetComponent<Image>().sprite = Sprite.Create(spritesa, reca, new Vector2(0, 0), .01f);
            Shuffle();
        
        }
        Texture2D spritesQ = LoadPNG(@"C:\Users\USER\AppData\LocalLow\KBF3\DGPCNL\Playables\toankntt\quyen1\tuan25\onluyen1\cau21hihi\TPL6_02.PL21\" + info.question + ".png");
        Rect recQ = new Rect(0, 0, spritesQ.width, spritesQ.height);
        Sprite.Create(spritesQ, recQ, new Vector2(0, 0), 1);
        img.sprite = Sprite.Create(spritesQ, recQ, new Vector2(0, 0), .01f);

        for(int i = 0; i < ListAns.Count; i++)
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

    // Update is called once per frame
    void Update()
    {

    }
}
