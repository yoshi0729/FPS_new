using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        Debug.Log(this + "ok");   
    }

    // Update is called once per frame
    public void SwitchScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
