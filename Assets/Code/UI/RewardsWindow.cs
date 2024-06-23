using System.Collections.Generic;
using System.Linq;
using Code.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardsWindow : BaseWindow
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _rewardsText;

    [SerializeField] private Button _buttonPrefab;

    List<Button> _buttons;

    [SerializeField] private GameObject _scrollViewContent;

    float verticalSpacing = 10f;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private ScrollRect _scrollRect;

    public List<InventoryItem> _items;
    protected override void Awake()
    {
        _closeButton.onClick.AddListener(Hide);
    }

    protected override void OnShow(object[] args)
    {
        _items = new List<InventoryItem>();
        _items = (List<InventoryItem>)args[1];

        RectTransform rectTransform = _scrollViewContent.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        _buttons = new List<Button>();
        float totalWidth = _items.Count * (_buttonPrefab.GetComponent<RectTransform>().rect.width + verticalSpacing);
        float viewportWidth = _scrollViewContent.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < _items.Count; i++)
        {
            Button newButton = Instantiate(_buttonPrefab, _scrollViewContent.transform);
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * (_buttonPrefab.GetComponent<RectTransform>().rect.width + verticalSpacing), 0f);
            GameObject img = newButton.transform.GetChild(0).gameObject;
            img.GetComponent<Image>().sprite = _items[i].Icon;
            _buttons.Add(newButton);
        }

        if (totalWidth < viewportWidth)
        {
            float leftPadding = (viewportWidth - totalWidth) / 2;
            _horizontalLayoutGroup.padding.left = (int)leftPadding;
            _scrollRect.horizontal = false;
        }
        else
        {
            _horizontalLayoutGroup.padding.left = 0;
            _scrollRect.horizontal = true;
        }
    }

    protected override void OnHide()
    {
        foreach (var item in _buttons)
        {
            Destroy(item.gameObject);
        }
        _horizontalLayoutGroup.padding.left = 0;
    }
}