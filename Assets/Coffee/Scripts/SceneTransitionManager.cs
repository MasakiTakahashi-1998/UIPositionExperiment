using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    
    public List<string> nowActiveScene = new List<string>();
    
    private string nowPage = "";

    #region SceneMetaDatas
    /// <summary>
    /// 実際の運用となった場合、AssetBundleの中身を参照して実装するのが適切かと思われる。
    /// したがって、AssetBundleの挙動を再現するためのシステムの構築は必要と考える。
    /// (Scenes以下のフォルダ構成を持ってAssetBundleの構成とするとか)
    /// </summary>

    List<string> commonSceneNameList = new List<string>()
    {
        "TouchEffect",
        "BackGround"
    };
    
    Dictionary<string,List<string>> pageSceneNameList = new Dictionary<string,List<string>>()
    {
        {"Title",null},
        {"Main",mainPageSceneNameList},
        {"Character",characterPageSceneNameList}
    };

    static readonly List<string> mainPageSceneNameList = new List<string>()
    {
        "Header",
        "Footer",
        "MainButton"
    };
    
    static readonly List<string> characterPageSceneNameList = new List<string>()
    {
        "Header",
        "Footer"
    };
    
    #endregion
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
        foreach (var VARIABLE in commonSceneNameList)
        {
            SceneManager.LoadSceneAsync(VARIABLE,LoadSceneMode.Additive);
        }
        
        LoadPage("Title",false);
    }

    public void LoadPage(string nextPagaName,bool unLoadPageScene = true)
    {
        if (nextPagaName == nowPage) return;
        
        if (unLoadPageScene)
        {
            UnLoadPage(nowPage,nextPagaName);
        }

        nowPage = nextPagaName;

        SceneManager.LoadSceneAsync(nextPagaName,LoadSceneMode.Additive);
        nowActiveScene.Add(nextPagaName);

        if (pageSceneNameList[nextPagaName] != null)
        {
            foreach (var VARIABLE in pageSceneNameList[nextPagaName])
            {
                if (!nowActiveScene.Contains(VARIABLE))
                {
                    SceneManager.LoadSceneAsync(VARIABLE,LoadSceneMode.Additive);
                    nowActiveScene.Add(VARIABLE);
                }
            }
        }
    }

    public void UnLoadPage(string nowPagaName)
    {
        SceneManager.UnloadSceneAsync(nowPagaName);
        nowActiveScene.Remove(nowPagaName);

        if (pageSceneNameList[nowPagaName] != null)
        {
            foreach (var VARIABLE in pageSceneNameList[nowPagaName])
            {
                SceneManager.UnloadSceneAsync(VARIABLE);
                nowActiveScene.Remove(VARIABLE);
            }
        }
    }
    
    public void UnLoadPage(string nowPagaName,string nextPageName)
    {
        SceneManager.UnloadSceneAsync(nowPagaName);
        nowActiveScene.Remove(nowPagaName);

        if (pageSceneNameList[nowPagaName] != null)
        {
            if (pageSceneNameList[nextPageName] == null)
            {
                foreach (var VARIABLE in pageSceneNameList[nowPagaName])
                {
                    SceneManager.UnloadSceneAsync(VARIABLE);
                    nowActiveScene.Remove(VARIABLE);
                }
                return;
            }

            for (int i = 0; i < nowActiveScene.Count; i++)
            {
                if (!pageSceneNameList[nextPageName].Contains(nowActiveScene[i]))
                {
                    SceneManager.UnloadSceneAsync(nowActiveScene[i]);
                    nowActiveScene.Remove(nowActiveScene[i]);
                    i -= 1;
                }
            }
        }
    }
}
