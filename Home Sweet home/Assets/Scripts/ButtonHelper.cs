using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerClickHandler, IPointerEnterHandler
{
    protected AudioSource _source;

    protected bool _onSelected;

    public virtual void OnSelect(BaseEventData eventData)
    {
        _onSelected = true;
        PlaySound();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (_onSelected)
            _onSelected = false;
        else PlaySound();
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        PlaySound();
    }

    public void PlaySound()
    {
        if (_source == null)
            _source = GetComponentInParent<AudioSource>();

        _source.pitch = Random.Range(0.7f, 1);
        _source.Play();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GetComponent<Selectable>().Select();
    }
}
