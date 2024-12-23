using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wintext: MonoBehaviour
{
    public float speed = 1.0f;
    private float time;
    private Text text;
    public float Win_or_Lose; // 勝ったかどうかの引数をここに持ってきてもらう


    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        if (Win_or_Lose == 0){
            text.text = "Draw";
        }
        else if (Win_or_Lose == 1){
            text.text = "Win";
        }
        else if (Win_or_Lose == 2){
            text.text = "Lose";
        }
        else{
            text.text = "No Contest";
        }
    }

    void Update()
    {
        text.color = GetTextColorAlpha(text.color);
    }

    Color GetTextColorAlpha(Color color)
    {
        time += Time.deltaTime * speed * 5.0f;
        color.a = Mathf.Sin(time);

        return color;
    }
}