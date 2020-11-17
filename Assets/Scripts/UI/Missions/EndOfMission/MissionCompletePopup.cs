using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MissionCompletePopup : MonoBehaviour {

    public TMP_Text MissionTimeText;
    public TMP_Text RewardText;
    public TMP_Text BonusText;
    public TMP_Text RepairText;
    public TMP_Text TotalText;

    public TextCounterInt RewardAmount;
    public TextCounterInt BonusAmount;
    public TextCounterInt RepairAmount;
    public TextCounterInt TotalAmount;

    public TMP_Text Separator;

    public GameObject ReplayButton;
    public GameObject GarageButton;

    public int RewardValue;
    public int BonusValue;
    public int RepairValue;
    public int TotalValue;

    private void Start()
    {
        RewardText.gameObject.SetActive(false);
        BonusText.gameObject.SetActive(false);
        RepairText.gameObject.SetActive(false);
        TotalText.gameObject.SetActive(false);

        RewardAmount.gameObject.SetActive(false);
        BonusAmount.gameObject.SetActive(false);
        RepairAmount.gameObject.SetActive(false);
        TotalAmount.gameObject.SetActive(false);

        Separator.gameObject.SetActive(false);

        ReplayButton.SetActive(false);
        GarageButton.SetActive(false);

        ShowRewardText();
    }

    private void ShowRewardText()
    {
        RewardText.gameObject.SetActive(true);
        LTDescr desc = LeanTween.move(RewardText.gameObject, RewardText.transform.position, 0.5f);
        desc.setOnComplete(SetRewardAmount);
    }

    private void SetRewardAmount()
    {
        RewardAmount.gameObject.SetActive(true);
        RewardAmount.Count(0, RewardValue, 1.0f, ShowBonusText);
    }

    private void ShowBonusText()
    {
        BonusText.gameObject.SetActive(true);
        LTDescr desc = LeanTween.move(BonusText.gameObject, BonusText.transform.position, 0.5f);
        desc.setOnComplete(SetBonusAmount);
    }

    private void SetBonusAmount()
    {
        BonusAmount.gameObject.SetActive(true);
        BonusAmount.Count(0, BonusValue, 1.0f, ShowRepairText);
    }

    private void ShowRepairText()
    {
        RepairText.gameObject.SetActive(true);
        LTDescr desc = LeanTween.move(RepairText.gameObject, RepairText.transform.position, 0.5f);
        desc.setOnComplete(SetRepairAmount);
    }

    private void SetRepairAmount()
    {
        RepairAmount.gameObject.SetActive(true);
        RepairAmount.Count(0, RepairValue, 1.0f, ShowTotalText);
    }
    private void ShowTotalText()
    {
        TotalText.gameObject.SetActive(true);
        Separator.gameObject.SetActive(true);
        LTDescr desc = LeanTween.move(TotalText.gameObject, TotalText.transform.position, 0.5f);
        desc.setOnComplete(SetTotalAmount);
    }

    private void SetTotalAmount()
    {
        TotalAmount.gameObject.SetActive(true);
        TotalAmount.Count(0, TotalValue, 1.0f, ShowButtons);
    }

    private void ShowButtons()
    {
        GarageButton.SetActive(true);
        ReplayButton.SetActive(ApplicationContext.Game.CurrentState == GameContext.Gamestate.Mission);
    }

}
