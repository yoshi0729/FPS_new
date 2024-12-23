using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hambtn : MonoBehaviour
{
    [SerializeField] GameObject target_hamburger;
    [SerializeField] GameObject target_item;
    [SerializeField] GameObject main_btn;


    private bool ishamburger = false;
    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log(this + "準備可能" + ishamburger);

        if (target_hamburger != false)
        {
            target_hamburger.SetActive(ishamburger);
        }
        // 表示パネルのボタンに同じスクリプトを入れるとこれがもう一度作動してしまう

    }
    
    public void Sethamburger()
    {   
        Debug.Log("押されているよ" + ishamburger);
        ishamburger = !ishamburger;
        target_hamburger.SetActive(ishamburger);
        target_item.SetActive(!ishamburger);
        main_btn.SetActive(!ishamburger);
    }


}
