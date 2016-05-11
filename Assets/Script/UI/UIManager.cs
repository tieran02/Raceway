using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager
{
    public static GameObject CreatePanel(Transform parent)
    {
        GameObject panelObject = new GameObject("Panel");
        panelObject.transform.SetParent(parent);

        RectTransform trans = panelObject.AddComponent<RectTransform>();
        trans.anchorMin = new Vector2(0, 0);
        trans.anchorMax = new Vector2(1, 1);
        trans.anchoredPosition3D = new Vector3(0, 0, 0);
        trans.anchoredPosition = new Vector2(0, 0);
        trans.offsetMin = new Vector2(0, 0);
        trans.offsetMax = new Vector2(0, 0);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.sizeDelta = new Vector2(0, 0);
        trans.localScale = new Vector3(0.8f, 0.8f, 1.0f);

        CanvasRenderer renderer = panelObject.AddComponent<CanvasRenderer>();

        Image image = panelObject.AddComponent<Image>();
        image.sprite = Resources.Load<Sprite>("UI/Canvas");

        return panelObject;
    }

    public static GameObject CreateText(Transform parent, float x, float y, float w, float h, string message, int fontSize)
    {
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(parent);

        RectTransform trans = textObject.AddComponent<RectTransform>();
        trans.sizeDelta.Set(w, h);
        trans.anchoredPosition3D = new Vector3(0, 0, 0);
        trans.anchoredPosition = new Vector2(x, y);
        trans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        trans.localPosition.Set(0, 0, 0);

        CanvasRenderer renderer = textObject.AddComponent<CanvasRenderer>();

        Text text = textObject.AddComponent<Text>();
        text.supportRichText = true;
        text.text = message;
        text.fontSize = fontSize;
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.color = new Color(0, 0, 1);

        return textObject;
    }

    public static GameObject CreateButton(Transform parent, float x, float y, float w, float h, string message, UnityAction eventListner)
    {
        GameObject buttonObject = new GameObject("Button");
        buttonObject.transform.SetParent(parent);

        RectTransform trans = buttonObject.AddComponent<RectTransform>();
        SetSize(trans, new Vector2(w, h));
        trans.anchoredPosition3D = new Vector3(0, 0, 0);
        trans.anchoredPosition = new Vector2(x, y);
        trans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        trans.localPosition.Set(0, 0, 0);

        CanvasRenderer renderer = buttonObject.AddComponent<CanvasRenderer>();

        Image image = buttonObject.AddComponent<Image>();

        image.sprite = Resources.Load<Sprite>("UI/Button");

        Button button = buttonObject.AddComponent<Button>();
        button.interactable = true;
        button.onClick.AddListener(eventListner);

        GameObject textObject = CreateText(buttonObject.transform, 0, 0, 0, 0, message, 24);

        return buttonObject;
    }

    public static GameObject CreateInputField(Transform parent, float x, float y, float w, float h, string message)
    {
        GameObject inputObject = new GameObject("Button");
        inputObject.transform.SetParent(parent);

        RectTransform trans = inputObject.AddComponent<RectTransform>();
        SetSize(trans, new Vector2(w, h));
        trans.anchoredPosition3D = new Vector3(0, 0, 0);
        trans.anchoredPosition = new Vector2(x, y);
        trans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        trans.localPosition.Set(0, 0, 0);

        CanvasRenderer renderer = inputObject.AddComponent<CanvasRenderer>();

        Image image = inputObject.AddComponent<Image>();

        image.sprite = Resources.Load<Sprite>("UI/Button");

        InputField input = inputObject.AddComponent<InputField>();
        input.interactable = true;

        GameObject textObject = CreateText(inputObject.transform, 0, 0, 0, 0, message, 24);

        return inputObject;
    }

    private static void SetSize(RectTransform trans, Vector2 size)
    {
        Vector2 currSize = trans.rect.size;
        Vector2 sizeDiff = size - currSize;
        trans.offsetMin = trans.offsetMin - new Vector2(sizeDiff.x * trans.pivot.x, sizeDiff.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(sizeDiff.x * (1.0f - trans.pivot.x), sizeDiff.y * (1.0f - trans.pivot.y));
    }

}
