using UnityEngine;
using UnityEngine.UI;

public class home_script : MonoBehaviour
{
    
    [SerializeField] GameObject Home;
    [SerializeField] GameObject Wepon;
    [SerializeField] GameObject Skinn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(this + "ホーム画面用意" + Home);

        if(Home != true){
            Home.SetActive(true);
        } // 最初の画面を表示させる
        Wepon.SetActive(false);
        Skinn.SetActive(false);
        
    }
    
    public void set_home()
    {
        Debug.Log( this + "押されているよ");
        Home.SetActive(true);
        Wepon.SetActive(false);
        Skinn.SetActive(false);
    }

    public void set_wepon()
    {
        Debug.Log(this + "押されているよ");
        Home.SetActive(false);
        Wepon.SetActive(true);
        Skinn.SetActive(false);
    }

    public void set_skinn()
    {
        Debug.Log(this + "押されているよ");
        Home.SetActive(false);
        Wepon.SetActive(false);
        Skinn.SetActive(true);
    }

}
