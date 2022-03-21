using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BundleData", menuName = "Gameplay/New BundleData")]

public class BundleData : ScriptableObject
{
    [SerializeField]
    private CardData[] _cardDatas;

    public CardData[] cardDatas => _cardDatas;
}
