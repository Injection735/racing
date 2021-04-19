
using System;
using System.Collections;
using UnityEngine;

public class VisitingRewardController
{
	private const string RewardLastTimestamp = nameof(RewardLastTimestamp);
	private const string CurrentStreak = nameof(CurrentStreak);

	private VisitingRewardView _visitingRewardView;
	private SlotRewardView _dailySlot;
	private SlotRewardView _weeklySlot;

	public VisitingRewardController(VisitingRewardView generateLevelView)
	{
		_visitingRewardView = generateLevelView;
	}

	public void RefreshView()
	{
		if (IsStreakInterrupted())
			Reset();

		InitSlots();
		SubscribeButtons();

		_visitingRewardView.StartCoroutine(RewardsStateUpdater());
	}

	public bool IsStreakInterrupted() => (int) DateTimeOffset.Now.ToUnixTimeSeconds() - PlayerPrefs.GetInt(RewardLastTimestamp) > _visitingRewardView.DayRewardDelay * 2;

	public bool CanGetReward() => ((int) DateTimeOffset.Now.ToUnixTimeSeconds() - PlayerPrefs.GetInt(RewardLastTimestamp) > _visitingRewardView.DayRewardDelay) ||
		PlayerPrefs.GetInt(RewardLastTimestamp) == 0;

	public bool IsWeeklyAvailable() => CanGetReward() && PlayerPrefs.GetInt(CurrentStreak) == 7;

	private IEnumerator RewardsStateUpdater()
	{
		while (true)
		{
			RefreshRewardsState();
			yield return new WaitForSeconds(1);
		}
	}

	private void RefreshRewardsState()
	{
		_visitingRewardView.GetDailyRewardButton.interactable = !IsWeeklyAvailable() && CanGetReward();
		_visitingRewardView.GetWeeklyRewardButton.interactable = IsWeeklyAvailable();

		_dailySlot.SetData(_visitingRewardView.DailyReward, _visitingRewardView.GetDailyRewardButton.interactable);
		_weeklySlot.SetData(_visitingRewardView.WeeklyReward, _visitingRewardView.GetWeeklyRewardButton.interactable);
	}

	private void SubscribeButtons()
	{
		_visitingRewardView.GetDailyRewardButton.onClick.AddListener(ClaimDailyReward);
		_visitingRewardView.GetWeeklyRewardButton.onClick.AddListener(ClaimWeeklyReward);
	}

	private void ClaimDailyReward()
	{
		if (!CanGetReward())
			return;

		ClaimReward(_visitingRewardView.WeeklyReward, false);
	}

	private void ClaimWeeklyReward()
	{
		if (!CanGetReward())
			return;

		ClaimReward(_visitingRewardView.WeeklyReward, true);
	}

	private void ClaimReward(Reward reward, bool isWeekly)
	{
		switch (reward.RewardType)
		{
			case RewardType.Wood:
				CurrencyView.Instance.AddWood(reward.CountCurrency);
				break;
			case RewardType.Diamond:
				CurrencyView.Instance.AddDiamonds(reward.CountCurrency);
				break;
		}

		if (isWeekly)
			Reset();
		else
			PlayerPrefs.SetInt(CurrentStreak, PlayerPrefs.GetInt(CurrentStreak) + 1);

		PlayerPrefs.SetInt(RewardLastTimestamp, (int) DateTimeOffset.Now.ToUnixTimeSeconds());
	}

	private void InitSlots()
	{
		_dailySlot = GameObject.Instantiate(_visitingRewardView.Slot, _visitingRewardView.Root, false);
		_weeklySlot = GameObject.Instantiate(_visitingRewardView.Slot, _visitingRewardView.Root, false);
	}

	private void Reset()
	{
		PlayerPrefs.SetInt(CurrentStreak, 0);
		PlayerPrefs.SetInt(RewardLastTimestamp, 0);
	}
}