using UnityEngine;
using UnityEngine.UI;

public class set_wpnpg : MonoBehaviour
{
    [SerializeField] GameObject target_page;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject fps_page;
    [SerializeField] GameObject target_object;
    void Start()
    {
        target_page.SetActive(true);
        fps_page.SetActive(false);
        practicecmra scriptA = target_object.GetComponent<practicecmra>();
        if(scriptA != null){
            scriptA.unlock();
        }
        else{
            Debug.LogError(scriptA + "が見つかりません");
        }
    }

    // Update is called once per frame
    public void close_tab()
    {
        target_page.SetActive(false);
        fps_page.SetActive(true);
        practicecmra scriptA = target_object.GetComponent<practicecmra>();
        if(scriptA != null){
            scriptA.set_again();
        }
        else{
            Debug.LogError(scriptA + "が見つかりません");
        }
    }
    // リスポーンを感知してこのタブがまた開かれるようにする
    public void OnWeaponSelected(int weaponID)
    {
        // 武器選択の処理（武器IDに応じた処理を実装）
        Debug.Log("選択された武器ID: " + weaponID);

        // UIを閉じる
        close_tab();
    }
}
