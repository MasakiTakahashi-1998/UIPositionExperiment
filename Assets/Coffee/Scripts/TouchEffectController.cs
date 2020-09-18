using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectController : MonoBehaviour
{
    [SerializeField] private Camera renderCamera;
    [SerializeField] private GameObject touchEffectPrefab;
    [SerializeField] private RectTransform canvasRectTransform;
    
    // クリックした位置座標
    private Vector3 clickPosition;

    private int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
            // Vector3でマウスがクリックした位置座標を取得する
            clickPosition = Input.mousePosition;
            clickPosition.x = clickPosition.x / Screen.width * canvasRectTransform.sizeDelta.x;
            clickPosition.y = clickPosition.y / Screen.height * canvasRectTransform.sizeDelta.y;
            // Z軸修正
            clickPosition.z = 10f;
            
            // Debug.Log(clickPosition);
            // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
            // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
            Instantiate(touchEffectPrefab, renderCamera.ScreenToWorldPoint(clickPosition), touchEffectPrefab.transform.rotation,transform);
        }
    }
}
