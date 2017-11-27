using UnityEngine;
using System.Collections;

public class AutoSize : MonoBehaviour
{
    public bool isUpdate;
    public Transform originalTrans;
    public RectTransform originalSizeTrans;
    private RectTransform rectTrans;
    private Rect originalRect;
    private bool isDone = false;
    private void Start()
    {

        rectTrans = gameObject.GetComponent<RectTransform>();

        if (originalSizeTrans == null)
        {
            originalSizeTrans = rectTrans.FindChild("OriginalSize").GetComponent<RectTransform>();
        }
        if (originalTrans == null)
        {
            originalTrans = transform.FindChild("OriginalTrans");
        }
        AutoToSize();
    }

    private void Update()
    {
        if (isUpdate)
        {
            AutoToSize();
        }

    }
    private void AutoToSize()
    {
        if (originalSizeTrans == null || originalTrans == null)
        {
            return;
        }
        originalRect = rectTrans.rect;
        originalRect = originalSizeTrans.rect;
        //Debug.Log("originalRect:" + originalRect);
        var scaleRect = new Vector3(rectTrans.rect.width / originalRect.width, rectTrans.rect.height / originalRect.height, 1);
        //Debug.Log("ScaleRect:" + scaleRect);
        originalTrans.localScale = scaleRect;
    }
#if UNITY_EDITOR
    [ContextMenu("设置原始大小参照物")]
    private void SetOriginalSizeTrans()
    {
        var halfVector2 = new Vector2(0.5f, 0.5f);
        var origTrans = transform.FindChild("OriginalSize");
        GameObject originalSize;
        RectTransform rectTrans;
        if (origTrans == null)
        {
            originalSize = new GameObject("OriginalSize");
            originalSize.transform.SetParent(transform);
            originalSize.transform.localScale = Vector3.one;
            originalSize.transform.localPosition = Vector3.zero;
             rectTrans = originalSize.AddComponent<RectTransform>();
        }
        else
        {
            originalSize = origTrans.gameObject;
            rectTrans = originalSize.GetComponent<RectTransform>();
        }
        rectTrans.anchorMax = Vector2.one;
        rectTrans.anchorMin = Vector2.zero;
        rectTrans.sizeDelta = Vector2.zero;
        var rectSize = rectTrans.rect;
        rectTrans.anchorMax = halfVector2;
        rectTrans.anchorMin = halfVector2;
        rectTrans.sizeDelta = new Vector2(rectSize.width, rectSize.height);
        originalSizeTrans = rectTrans;
    }
#endif
}
