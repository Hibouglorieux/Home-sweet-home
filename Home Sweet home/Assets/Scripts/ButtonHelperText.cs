using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHelperText : ButtonHelper, IDeselectHandler
{
    protected Selectable _selectable;
    protected TextMeshProUGUI _text;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        SelectText();
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        UnSelectText();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!_onSelected)
            SelectText();

        base.OnPointerClick(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
    }

    protected void SelectText()
    {
        if (_selectable == null)
            _selectable = GetComponent<Selectable>();
        if (_text == null)
            _text = GetComponentInChildren<TextMeshProUGUI>();

        _text.color = _selectable.colors.highlightedColor;
    }

    protected void UnSelectText()
    {
        if (_selectable == null)
            _selectable = GetComponent<Selectable>();
        if (_text == null)
            _text = GetComponentInChildren<TextMeshProUGUI>();

        _text.color = _selectable.colors.normalColor;
    }
}
