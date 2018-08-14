using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShadowLenghtAdapter : MonoBehaviour {

    public Text textReference;
    public Image imageReference;

    public RectTransform rectTransform;

    public float fontToWidthConverter;
    public float textOffset = 10;
    public float ImageOffset = 30;

    private void Awake()
    {
        MenuUIManager.ChangeShadow += Adapt;
        HomeUIManager.ChangeShadow += Adapt;
        GameUIManager.ChangeShadow += Adapt;

    }

    private void Start()
    {
        Adapt();
    }

    public void Adapt() {
        if (textReference != null)
        {
            float newWidth = textReference.text.Length * textReference.fontSize * fontToWidthConverter - textOffset*2;
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        }
        else if (imageReference != null) {
            rectTransform.sizeDelta = new Vector2(imageReference.rectTransform.rect.width - ImageOffset * 2, rectTransform.sizeDelta.y);
        }
    }

    private void OnDisable()
    {
        MenuUIManager.ChangeShadow -= Adapt;
        HomeUIManager.ChangeShadow -= Adapt;
        GameUIManager.ChangeShadow -= Adapt;

    }
}
