using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    
    public List<string> nowActiveScene = new List<string>();
    public List<AsyncOperation> asyncOperations = new List<AsyncOperation>();
    
    private string _nowPage = "";

    private int nowCommonSceneLoadedint = 0;

    #region SceneMetaDatas
    /// <summary>
    /// 実際の運用となった場合、AssetBundleの中身を参照して実装するのが適切かと思われる。
    /// したがって、AssetBundleの挙動を再現するためのシステムの構築は必要と考える。
    /// (Scenes以下のフォルダ構成を持ってAssetBundleの構成とするとか)
    /// </summary>

    List<string> _commonSceneNameList = new List<string>()
    {
        "TouchEffect",
        "BackGround",
        "Footer"
    };
    
    Dictionary<string,List<string>> _pageSceneNameList = new Dictionary<string,List<string>>()
    {
        {"Title",null},
        {"Main",MainPageSceneNameList},
        {"Character",CharacterPageSceneNameList}
    };

    static readonly List<string> MainPageSceneNameList = new List<string>()
    {
        "Header",
        "MainButton"
    };
    
    static readonly List<string> CharacterPageSceneNameList = new List<string>()
    {
        "Header"
    };
    
    #endregion
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
        SceneTransition("Title",(int)PageScene.Title, false);
    }

    public void SceneTransition(string nextPagaName,int selectSceneFlagInt,bool unLoadPageScene = true)
    {
        if (nextPagaName == _nowPage) return;
        if (asyncOperations.Count != 0) return;
        
        asyncOperations = new List<AsyncOperation>();

        if (unLoadPageScene)
        {
            UnLoadPage(_nowPage);
        }

        CommonSceneSelector(selectSceneFlagInt);
        //UnLoadPage(_nowPage);
        LoadPage(nextPagaName);
        
        _nowPage = nextPagaName;

        StartCoroutine(SceneTransitionCoroutine());
    }

    public void LoadPage(string nowPagaName)
    {
        asyncOperations.Add(SceneManager.LoadSceneAsync(nowPagaName,LoadSceneMode.Additive));
        asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
        nowActiveScene.Add(nowPagaName);

        if (_pageSceneNameList[nowPagaName] != null)
        {
            foreach (var variable in _pageSceneNameList[nowPagaName])
            {
                asyncOperations.Add(SceneManager.LoadSceneAsync(variable,LoadSceneMode.Additive));
                asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
                nowActiveScene.Add(variable);
            }
        }
    }
    

    public void UnLoadPage(string nowPagaName)
    {
        asyncOperations.Add(SceneManager.UnloadSceneAsync(nowPagaName));
        asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
        nowActiveScene.Remove(nowPagaName);

        if (_pageSceneNameList[nowPagaName] != null)
        {
            foreach (var variable in _pageSceneNameList[nowPagaName])
            {
                asyncOperations.Add(SceneManager.UnloadSceneAsync(variable));
                asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
                nowActiveScene.Remove(variable);
            }
        }
    }
    
    public void UnLoadPage(string nowPagaName,string nextPageName)
    {
        asyncOperations.Add(SceneManager.UnloadSceneAsync(nowPagaName));
        asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
        nowActiveScene.Remove(nowPagaName);

        if (_pageSceneNameList[nowPagaName] != null)
        {
            if (_pageSceneNameList[nextPageName] == null)
            {
                foreach (var variable in _pageSceneNameList[nowPagaName])
                {
                    asyncOperations.Add(SceneManager.UnloadSceneAsync(variable));
                    asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
                    nowActiveScene.Remove(variable);
                }
                return;
            }

            for (int i = 0; i < nowActiveScene.Count; i++)
            {
                if (!_pageSceneNameList[nextPageName].Contains(nowActiveScene[i]))
                {
                    asyncOperations.Add(SceneManager.UnloadSceneAsync(nowActiveScene[i]));
                    asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
                    nowActiveScene.Remove(nowActiveScene[i]);
                    i -= 1;
                }
            }
        }
    }

    IEnumerator SceneTransitionCoroutine()
    {
        while (!DoneTransitionProcess())
        {
            yield return null;
        }

        asyncOperations.ConvertAll(x => x.allowSceneActivation = true);
        
        yield return null;
        
        asyncOperations = new List<AsyncOperation>();
    }

    public bool DoneTransitionProcess()
    {
        if (asyncOperations.Count(x => x.progress < 0.88f) == 0)
        {
            return true;
        }

        return false;
    }

    public void CommonSceneSelector(int selectSceneFlagInt)
    {
        foreach (CommonScene Value in Enum.GetValues(typeof(CommonScene)))
        {
            if (((int)Value & (byte) selectSceneFlagInt) != 0)
            {
                if (((int) Value & (byte) nowCommonSceneLoadedint) == 0)
                {
                    Debug.Log($"{(int)Value}:{(byte)nowCommonSceneLoadedint}");
                    asyncOperations.Add(SceneManager.LoadSceneAsync(Value.ToString(),LoadSceneMode.Additive));
                    asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
                    nowActiveScene.Add(Value.ToString());
                }
            }
            else
            {
                if (((int) Value & (byte) nowCommonSceneLoadedint) != 0)
                {
                    asyncOperations.Add(SceneManager.UnloadSceneAsync(Value.ToString()));
                    asyncOperations[asyncOperations.Count - 1].allowSceneActivation = false;
                    nowActiveScene.Remove(Value.ToString());
                }
            }
        }

        nowCommonSceneLoadedint = selectSceneFlagInt;
    }
}

public enum CommonScene
{
    TouchEffect = 1,
    BackGround = 2,
    Footer = 4
}

public enum PageScene
{
    Title = 1,
    Main = 7,
    Character = 7,
}