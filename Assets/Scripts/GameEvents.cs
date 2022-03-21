using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    private class CardUnityEvent : UnityEvent<Card>
    {
    }
    private CardUnityEvent RightCardClickEvent = new CardUnityEvent();
    private CardUnityEvent WrongCardClickEvent = new CardUnityEvent();

    private GameTable gameTable; 

    public void AddCardEventHandler(GameObject card)
    {
        card.GetComponent<Card>().OnCardClick += HandleCardClick;
    }


    public void HandleCardClick(Card card)
    {
        if (card.Name == gameTable.GetCurrentQuestion())
        {
            RightCardClickEvent.Invoke(card);
        }
        if (card.Name != gameTable.GetCurrentQuestion())
        {
            WrongCardClickEvent.Invoke(card);
        }
    }

    private void Start()
    {
        gameTable = GetComponent<GameTable>();
        RightCardClickEvent.AddListener(gameTable.OnRightAnswer);
        WrongCardClickEvent.AddListener(gameTable.OnWrongAnswer);
    }
}
