using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public List<Question2> questions;
    public bool isShow;

    private string json;

    private static PopupManager instance;

    public static PopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PopupManager>();
            }

            return instance;
        }
    }

    public static bool Exist => instance != null;

    private void Awake()
    {
        // StreamReader r = new StreamReader(@"Assets\Scripts\QuestionPopup\data.json");
        isShow = false;
        // json = File.ReadAllText(@"C:\Project\computer-graphic-unity\Assets\Scripts\QuestionPopup\data.json.txt");
        json = File.ReadAllText(@"D:\workspace\Endless-Runner-Game-master\Assets\Scripts\QuestionPopup\sample (1).json");
        Debug.Log(json);
        questions = JsonConvert.DeserializeObject<List<Question2>>(json);
    }

    public class Question
    {
        public string description;
        public List<string> options;
        public string answer;
    }

    public class Question2
    {
        public string description { get; set; }
        public List<string> options { get; set; }
        public int answer { get; set; }
        public string type { get; set; }
        public string operation { get; set; }
    }
}