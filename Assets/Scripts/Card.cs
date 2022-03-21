using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public string Name;
    private Sprite Sprite;
    private bool isAnimating;
    [SerializeField]
    public event Action<Card> OnCardClick;

    public bool IsAnimating()
    {
        return isAnimating;
    }

    public void SetAnimating(bool isAnimating)
    {
        this.isAnimating = isAnimating;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetSprite(Sprite sprite)
    {
        Sprite = sprite;
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite = Sprite;
    }

    public void OnPointerClick(PointerEventData data)
    {
        OnCardClick?.Invoke(this);
    }
}
