using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTable : MonoBehaviour  
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    ParticleEffects rightClickEffect;
    [SerializeField]
    private List<Transform> listOfCardsPosition = new List<Transform>();
    private GameEvents gameEvents;
    private DOTweenEffects DOTEffects;
    private List<GameObject> currentlvlCards = new List<GameObject>();
    private GameData gameData;
    private GameCycle gameCycle;
    private GameUI gameUI;
    private float NEXTLVLDELAY = 0.8f;

    public void ClearLvlTable()
    {
        foreach (GameObject card in currentlvlCards)
        {
            Destroy(card.gameObject);
        }
        currentlvlCards.Clear();
    }

    public string GetCurrentQuestion()
    {
        return GetQuestion(gameCycle.GetStep());
    }

    private string GetQuestion(int step)
    {
        return gameData.GetQuestionsList()[step];
    }

    public void SpawnSetOfCards(int step)
    {
        if (step == 0)
        {
            StartCoroutine(FirstLvlSpawn());
        }
        else
        {
            NextLvlCardSpawn();
        }
    }

    private CardData TakeRandomCardFromListOfSets(List<List<CardData>> listOfSets, int step)
    {
        CardData card = listOfSets[step][Random.Range(0, listOfSets[step].Count)];
        listOfSets[step].Remove(card);
        return card;
    }

    private void SingleCardSpawn(Transform transform, bool animate)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);
        CardData randomCardData = TakeRandomCardFromListOfSets(gameData.GetListOfSets(), gameCycle.GetStep());
        newCard.GetComponent<Card>().SetSprite(randomCardData.sprite);
        newCard.GetComponent<Card>().SetName(randomCardData.cardName);
        gameEvents.AddCardEventHandler(newCard);
        currentlvlCards.Add(newCard);
        if (animate)
        {
            newCard.transform.localScale = Vector2.zero;
            DOTEffects.CardAppearRisingBounseEffect(newCard.transform);
        }
    }

    private IEnumerator FirstLvlSpawn()
    {
        int totalCardsInCurrentSet = gameData.GetListOfSets()[gameCycle.GetStep()].Count;
        for (int i = 0; i < totalCardsInCurrentSet; i++)
        {
            yield return new WaitForSeconds(0.1f);
            SingleCardSpawn(listOfCardsPosition[i], true);
        }
        SetUpQuestion();
    }

    private void NextLvlCardSpawn()
    {
        int totalCardsInCurrentSet = gameData.GetListOfSets()[gameCycle.GetStep()].Count;
        for (int i = 0; i < totalCardsInCurrentSet; i++)
        {
            SingleCardSpawn(listOfCardsPosition[i], false);
        }
    }

    private void SetUpQuestion()
    {
        gameUI.SetQuestionText("Find " + GetQuestion(gameCycle.GetStep()));
        DOTEffects.TextFadeIn(gameUI.GetQuestionField());
    }

    private IEnumerator OnRestart()
    {
        DOTEffects.RisingBounceEffect(gameUI.GetRestartButton().transform);
        gameUI.RestartFadeIn();
        gameData.MakeCardSetsAndQuestionList();
        yield return new WaitForSeconds(DOTEffects.GetImageFadeInDuration());
        gameUI.ResetTableUI();
        gameCycle.OnGameCycleStart();
    }

    private IEnumerator OnBigRestart()
    {
        DOTEffects.RisingBounceEffect(gameUI.GetBigRestartButton().transform);
        gameUI.RestartFadeIn();
        gameData.MakeUnpassedCardList();
        gameData.MakeCardSetsAndQuestionList();
        yield return new WaitForSeconds(DOTEffects.GetImageFadeInDuration());
        gameUI.ResetTableUI();
        gameCycle.OnGameCycleStart();
    }

    public void Restart()
    {
        StartCoroutine(OnRestart());
    }

    public void BigRestart()
    {
        StartCoroutine(OnBigRestart());
    }

    private void NextLvlInvoke()
    {
        if (!IsInvoking("NextLvlInvoke"))
            gameCycle.Next();
    }
    
    public void OnRightAnswer(Card card)
    {
        if (IsInvoking("NextLvlInvoke")) return;
        Invoke("NextLvlInvoke", NEXTLVLDELAY);
        DOTEffects.RisingBounceEffect(card.transform.GetChild(1).GetComponent<Image>().transform);
        rightClickEffect.SetAndPlay(card.gameObject, 0.5f, -1.1f);
    }

    public void OnWrongAnswer(Card card)
    {
        if (IsInvoking("NextLvlInvoke")) return;
        if (card.IsAnimating()) return;
        card.SetAnimating(true);
        DOTEffects.WrongCardBounceEffect(card.transform.GetChild(1).GetComponent<Image>().transform, card);
    }

    private void Awake()
    {
        DOTEffects = GetComponent<DOTweenEffects>();
        gameData = GetComponent<GameData>();
        gameEvents = GetComponent<GameEvents>();
        gameCycle = GetComponent<GameCycle>();
        gameUI = GetComponent<GameUI>();
    }
}
