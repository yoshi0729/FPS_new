using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class pressany : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    void Start()
    {
        Debug.Log(this + "ok");   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            SceneManager.LoadScene(sceneName);
    }

}
}