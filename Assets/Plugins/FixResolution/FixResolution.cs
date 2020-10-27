using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FixResolution : MonoBehaviour
{
    [SerializeField] private RectTransform thisRectTransform = default;
    [SerializeField] private RectTransform parentRectTransform;
    
    private void Reset()
    {
        SetReferenceResolution();
    }
    
    private void Start()
    {
        // SetReferenceResolution();
        UpdateRectTransformScale();
    }

#if UNITY_EDITOR
    void Update()
    {
        UpdateRectTransformScale();
    }
#endif

    /// <summary>
    /// 参照するCanvasScalerとRectTransformを指定する
    /// また、ScreenMatchModeをExpendに指定する
    /// </summary>
    private void SetReferenceResolution()
    {
        thisRectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        CanvasScaler canvasScaler = transform.parent.GetComponent<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }
    }
    
    /// <summary>
    /// アスペクト比とセーフエリアに合わせてスケールを変更する
    /// UnityEditorでは、iPhoneXRの解像度を指定した場合、該当端末のセーフエリアを再現する
    /// </summary>
    private void UpdateRectTransformScale()
    {
        var sizeDelta = parentRectTransform.sizeDelta;
        thisRectTransform.offsetMin = new Vector2(Screen.safeArea.x * (sizeDelta.x / Screen.width), Screen.safeArea.y * (sizeDelta.y/ Screen.height));
        thisRectTransform.offsetMax = new Vector2(-Screen.safeArea.x * (sizeDelta.x / Screen.width), 0);
        thisRectTransform.anchoredPosition = new Vector2(0.5f,1);
    }
}