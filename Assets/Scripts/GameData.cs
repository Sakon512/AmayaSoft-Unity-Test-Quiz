using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private string BUNDLESPATH = "Scriptable Objects/Bundles";
    private BundleData[] bundles;
    private BundleData currentBundle;
    private List<CardData> _unpassedCardsList = new List<CardData>();
    public List<string> _questionsList = new List<string>();
    private List<List<CardData>> _listOfSets = new List<List<CardData>>();
    private List<CardData> currentLvlSet = new List<CardData>();
    private List<CardData> currentLvlPool = new List<CardData>();
    
    private CardData randomUnpassedCard;

    public List<List<CardData>> GetListOfSets()
    {
        return _listOfSets;
    }

    public List<string> GetQuestionsList()
    {
        return _questionsList;
    }

    public List<CardData> GetUnpassedCardList()
    {
        return _unpassedCardsList;
    }

    public void MakeUnpassedCardList()
    {
        _unpassedCardsList.Clear();
        foreach (BundleData bundle in bundles)
        {
            foreach (CardData cardData in bundle.cardDatas)
            {
                   _unpassedCardsList.Add(cardData);
            }
        }
    }

    public void MakeCardSetsAndQuestionList()
    {
        _questionsList.Clear();
        _listOfSets.Clear();
        int totalCardsInSet = 0;
        for (int currentLvlCount = 0; currentLvlCount < 3; currentLvlCount++)
        {
            currentLvlSet.Clear();
            switch (currentLvlCount)
            {
                case 0:
                    totalCardsInSet = 3;
                    break;
                case 1:
                    totalCardsInSet = 6;
                    break;
                case 2:
                    totalCardsInSet = 9;
                    break;
            }
            if (_unpassedCardsList.Count > 0)
            {
                TakeRandomUnpassedCard();
            }
            else
            {
                _questionsList.Add("");
                break;
            }
            MakeCurrentLvlPullFromCurrentBundle();
            while (currentLvlSet.Count < totalCardsInSet)
            {
                if (currentLvlPool.Count <= 0) break;
                CardData currentPickedCard;
                currentPickedCard = PickRandomCardFromCurrentPool();
                currentLvlSet.Add(currentPickedCard);
                currentLvlPool.Remove(currentPickedCard);
            }
            _listOfSets.Add(new List<CardData>(currentLvlSet));
        }
    }

    private void TakeRandomUnpassedCard()
    {
        randomUnpassedCard = _unpassedCardsList[Random.Range(0, _unpassedCardsList.Count)];
        currentLvlSet.Add(randomUnpassedCard);
        _questionsList.Add(randomUnpassedCard.cardName);
        _unpassedCardsList.Remove(randomUnpassedCard);

    }

    private void CurrentUnpassedCardBundleDetector()
    {
        foreach (BundleData bundleData in bundles)
        {
            foreach (CardData cardData in bundleData.cardDatas)
            {
                if (cardData == randomUnpassedCard)
                {
                    currentBundle = bundleData;
                    return;
                }
            }
        }
    }

    private void MakeCurrentLvlPullFromCurrentBundle()
    {
        currentLvlPool.Clear();
        CurrentUnpassedCardBundleDetector();
        foreach (CardData card in currentBundle.cardDatas)
        {
            currentLvlPool.Add(card);
            if (card == randomUnpassedCard)
            {
                currentLvlPool.Remove(card);
            }
        }
    }

    private CardData PickRandomCardFromCurrentPool()
    {
        return currentLvlPool[Random.Range(0, currentLvlPool.Count)];
    }

    public void LoadGameCards()
    {
        LoadBundles(BUNDLESPATH, bundles);
    }

    private void LoadBundles(string path, BundleData[] bundles)
    {
        this.bundles = Resources.LoadAll<BundleData>(path);
    }
}
