using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{

    private Button button;
    [SerializeField] private RawImage buttonImage;


    private int _itemId;
    private Sprite _buttonTexture;

    // emcapsulate the 2 above fields
    public int ItemID
    {
        set
        {
            _itemId = value;
        }
    }

    public Sprite ButtonTexture
    {
        set
        {
            _buttonTexture = value;
            buttonImage.texture = _buttonTexture.texture;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // when button is clicked the object should change
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Sample.OnEntered(gameObject))
        {
            // scale up with method from DOTweening plugin
            transform.DOScale(Vector3.one * 1.5f, 0.3f);
            //transform.localScale = Vector3.one * 2;
        }
        else
        {
            transform.DOScale(Vector3.one, 0.3f);
            //transform.localScale = Vector3.one;
        }
    }

    void SelectObject()
    {
        dataHandler.Instance.SetFurniture(_itemId);
    }
}
