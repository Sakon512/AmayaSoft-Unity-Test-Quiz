using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Gameplay/New CardData")]

public class CardData : ScriptableObject
{
    [SerializeField]
    private string _cardName;
    [SerializeField]
    private Sprite _sprite;

    public string cardName => this._cardName;
    public Sprite sprite => this._sprite;
    
}
