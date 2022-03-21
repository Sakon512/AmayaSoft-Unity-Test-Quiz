using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    private GameTable gameTable;
    private GameData gameData;
    private GameUI gameUI;
    private DOTweenEffects DOTEffects;
    private int _step;

    public int GetStep()
    {
        return _step;
    }

    private bool allCardBundlesPassed()
    {
        return (gameData.GetUnpassedCardList().Count == 0 && gameData.GetListOfSets().Count == 0) ? true : false;
    }

    public void OnGameCycleStart()
    {
        if (allCardBundlesPassed())
        {
            OnGameOver();                                              
            return;
        }
        _step = 0;
        gameTable.SpawnSetOfCards(_step);
    }

    public void Next()
    {
        _step++;
        if (_step == gameData.GetListOfSets().Count || allCardBundlesPassed())
        {
            SetIsOver();
        }
        else
        {
            gameTable.ClearLvlTable();
            gameUI.SetQuestionText("Find " + gameData.GetQuestionsList()[_step]);
            gameTable.SpawnSetOfCards(_step);
        }
    }

    private void SetIsOver()
    {
        if (gameData.GetUnpassedCardList().Count == 0 && gameData.GetQuestionsList().Contains(""))
        {
            OnGameOver();
        }
        else
        {
            OnGameCycleComplete();
        }
    }

    private void OnGameCycleComplete()
    {
        DOTEffects.TextFadeOut(gameUI.GetQuestionField());
        gameUI.ButtonPopUp(gameUI.GetRestartButton());
        gameUI.GetFadeWinImg().gameObject.SetActive(true);
        DOTEffects.ImageHalfFadeOut(gameUI.GetFadeWinImg());
    }

    public void OnGameOver()
    {
        gameUI.SetQuestionText("WELL DONE!");
        gameUI.SetTextAlpha(gameUI.GetQuestionField(), 0);
        DOTEffects.TextFadeIn(gameUI.GetQuestionField());
        gameUI.ButtonPopUp(gameUI.GetBigRestartButton());
        gameUI.GetFadeWinImg().gameObject.SetActive(true);
        DOTEffects.ImageHalfFadeOut(gameUI.GetFadeWinImg());
    }

    private void Awake()
    {
        gameTable = GetComponent<GameTable>();
        gameData = GetComponent<GameData>();
        gameUI = GetComponent<GameUI>();
        DOTEffects = GetComponent<DOTweenEffects>();
        gameData.LoadGameCards();
        gameData.MakeUnpassedCardList();
        gameData.MakeCardSetsAndQuestionList();
    }

    void Start()
    {
        OnGameCycleStart();
    }
}
