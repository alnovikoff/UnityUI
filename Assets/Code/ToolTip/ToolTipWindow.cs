using Code.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ToolTipWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject panel;
    private bool isPointerDown = false;
    private float holdTime = 1.5f;
    private float pointerDownTimer;
    [SerializeField] private RewardsWindow rewardsWindow;

    private Vector2 bgSize;
    private Vector2 tmpSize;

    public Image bgPanel;
    public float maxWidth = 300f;
    public float maxHeight = 200f;

    public TMP_Text _title;
    public TMP_Text _description;
    public Image _icon;

    private void Start()
    {
        bgSize = bgPanel.rectTransform.sizeDelta;
        tmpSize = _description.rectTransform.sizeDelta;

        Debug.Log(bgSize);
    }

    void Update()
    {
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= holdTime)
            {
                OpenPanel(rewardsWindow._items);
            }
        }
        if (_description.isTextOverflowing)
        {
            Vector2 newSize = _description.rectTransform.sizeDelta;
            Vector2 newSize2 = bgPanel.rectTransform.sizeDelta;
            if (newSize.x < 2000)
            {
                newSize.x += 10;
                newSize.x += 2;
                newSize2.x += 10;
                newSize2.y += 2;
            }
            _description.rectTransform.sizeDelta = newSize;
            bgPanel.rectTransform.sizeDelta = newSize2;
            if (newSize.x >= 2000)
            {
                _description.enableAutoSizing = true;
                _title.enableAutoSizing = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pointerDownTimer = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        ClosePanel();
    }

    private void OpenPanel(List<InventoryItem> items)
    {
        panel.SetActive(true);
        if (string.IsNullOrEmpty(_description.text))
        {

            var random = Random.Range(0, items.Count);

            _title.text = items[random].Title;
            _description.text = items[random].Description;
            _icon.sprite = items[random].Icon;
        }
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
        _title.text = "";
        _description.text = "";
        _icon.sprite = null;

        _description.enableAutoSizing = false;
        _title.enableAutoSizing = false;

        _description.fontSize = 60;
        _title.fontSize = 60;

        RevertWindow();
    }

    private void RevertWindow()
    {
        _description.rectTransform.sizeDelta = tmpSize;
        bgPanel.rectTransform.sizeDelta = bgSize;
    }
}
