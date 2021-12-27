using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class QuestionPopup : MonoBehaviour
{
    #region Instance
    
    private static QuestionPopup instance;

    public static QuestionPopup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestionPopup>();
            }
            return instance;
        }
    }
    
    public static bool Exist => instance != null;
    
    #endregion
    
    #region Inspectors

    public RectTransform trans;

    public TMP_Text description;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public TMP_Text option1;
    public TMP_Text option2;
    public TMP_Text option3;
    public TMP_Text option4;
    public TMP_Text operation;
    #endregion

    #region Variables

    private PopupManager.Question2 question;
    private List<string> options;
    private int answer;
    private string desc;
    System.Random rnd = new System.Random();
    #endregion

    #region Unity Methods

    

    #endregion

    #region Public Methods

    public void Show()
    {
        if (PopupManager.Instance.questions.Count == 0)
        {
            gameObject.GetComponent<RectTransform>().Translate(Vector3.up*600f); 
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            return;
        }
        PopupManager.Instance.isShow = true;
        Init();
        btn1.onClick.AddListener(() => OnClickOption(btn1));
        btn2.onClick.AddListener(() => OnClickOption(btn2));
        btn3.onClick.AddListener(() => OnClickOption(btn3));
        btn4.onClick.AddListener(() => OnClickOption(btn4));

        trans.DOAnchorPosY(0, 1f);
    }

    public void Hide()
    {
        trans.DOAnchorPosY(600, 1f);
    }
    
    public void OnClickOption(object sender)
    {
        Button btn = sender as Button;
        PopupManager.Instance.isShow = false;
        Hide();
        if (btn.name == $"Option{answer}")
        {
            
        }

        // if ((btn == btn1 && answer == 1) || (btn == btn2 && answer == 2) || (btn == btn3 && answer == 3) ||
        //     (btn == btn4 && answer == 4))
        // {
        //     Hide();
        // }
        else
        {
            gameObject.GetComponent<RectTransform>().Translate(Vector3.up*600f); 
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    #endregion

    #region Private Methods

    private void Init()
    {
        int index = rnd.Next(0, PopupManager.Instance.questions.Count-1);
        question = PopupManager.Instance.questions[index];
        PopupManager.Instance.questions.RemoveAt(index);

        description.text = question.description;
        option1.text = question.options[0];
        option2.text = question.options[1];
        option3.text = question.options[2];
        option4.text = question.options[3];
        operation.text = "";
        answer = question.answer;

        switch (question.type)
        {
            case "DEFAULT":
                break;
            case "FRACTION":
                operation.text = question.operation;
                break;
            case "SYMBOL":
                desc = question.description;
                description.text = GetSymbol(desc);
                option1.text = GetSymbol(question.options[0]);
                option2.text = GetSymbol(question.options[1]);
                option3.text = GetSymbol(question.options[2]);
                option4.text = GetSymbol(question.options[3]);
                break;
            default:
                return;
        }

        

        // for (int i = 0; i < question.options.Count; ++i)
        // {
        //     if (question.answer == question.options[i])
        //     {
        //         answer = i + 1;
        //         break;
        //     }
        // }

    }

    private string GetSymbol(string str)
    {
        string result = str;
        Debug.Log(result.IndexOf("x"));
        result = result.Replace("x", "<sprite=\"round\" name=\"round\">");
        result = result.Replace("y", "<sprite=\"triangle\" name=\"triangle\">");
        result = result.Replace("z", "<sprite=\"square\" name=\"square\">");

        return result;
    }

    #endregion
}
