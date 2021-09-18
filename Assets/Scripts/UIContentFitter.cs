using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContentFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        // find how many child gameObjects in the group
        HorizontalLayoutGroup hg = GetComponent<HorizontalLayoutGroup>();
        int childCount = transform.childCount - 1;
        // get the width of the child gameObject
        float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        // width of the rectangle panel
        float width = hg.spacing * childCount + childCount * childWidth + hg.padding.left;

        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 150);
    }
}
