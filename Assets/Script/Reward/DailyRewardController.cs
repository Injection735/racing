using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardController : BaseController
{
	private DailyRewardView _dailyRewardView;
	private List<ContainerSlotRewardView> _slots;
	private CurrencyController _currencyController;
	private bool _canGetReward;

	public DailyRewardController(Transform placeForUi, DailyRewardView dailyRewardView, CurrencyView currencyView)
	{
		_dailyRewardView = GameObject.Instantiate(dailyRewardView, placeForUi);
		_currencyController = new CurrencyController(placeForUi, currencyView);
	}

	public void RefreshView()
	{
		InitSlots();
		_dailyRewardView.StartCoroutine(RewardsStateUpdater());
		RefreshUi();
		SubscribeButtons();
	}

	private void InitSlots()
	{
		_slots = new List<ContainerSlotRewardView>();

		for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
			_slots.Add(GameObject.Instantiate(_dailyRewardView.ContainerSlotRewardView, _dailyRewardView.MountRootSlotsReward, false));
	}

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
		_canGetReward = true;

		if (_dailyRewardView.TimeGetReward.HasValue)
		{
			var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

			if (timeSpan.TotalSeconds > _dailyRewardView.TimeDeadline)
			{
				_dailyRewardView.TimeGetReward = null;
				_dailyRewardView.CurrentSlotInActive = 0;
			}
			else if (timeSpan.TotalSeconds < _dailyRewardView.TimeCooldown)
				_canGetReward = false;
		}

		RefreshUi();
	}

	private void RefreshUi()
	{
		_dailyRewardView.GetRewardButton.interactable = _canGetReward;
		_dailyRewardView.ProgressBar.Total = _dailyRewardView.TimeCooldown;

		if (_dailyRewardView.TimeGetReward != null)
		{
			var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);

			TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

			if (currentClaimCooldown.TotalSeconds < 0)
			{
				_dailyRewardView.ProgressBar.CurrentValue = _dailyRewardView.ProgressBar.Total;
				_dailyRewardView.TimerNewReward.text = $"Time recieve reward";
				_dailyRewardView.GetRewardButton.interactable = true;
			}
			else
			{
				var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{ currentClaimCooldown.Seconds:D2}";
				_dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward} ";
				_dailyRewardView.ProgressBar.CurrentValue = _dailyRewardView.ProgressBar.Total - (float) currentClaimCooldown.TotalSeconds;
			}
		}

		for (var i = 0; i < _slots.Count; i++)
			_slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentSlotInActive);
	}

	private void SubscribeButtons()
	{
		_dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
		_dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
		_dailyRewardView.CloseWindow.onClick.AddListener(CloseWindow);
	}

	private void ClaimReward()
	{
		if (!_canGetReward)
			return;

		var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];
		
		switch (reward.RewardType)
		{
			case RewardType.Wood:
				CurrencyView.Instance.AddWood(reward.CountCurrency);
				break;
			case RewardType.Diamond:
				CurrencyView.Instance.AddDiamonds(reward.CountCurrency);
				break;
		}
		
		_dailyRewardView.TimeGetReward = DateTime.UtcNow;
		_dailyRewardView.CurrentSlotInActive = (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;
		RefreshRewardsState();
	}

	private void ResetTimer()
	{
		PlayerPrefs.DeleteAll();
		CurrencyView.Instance.AddWood(0);
		CurrencyView.Instance.AddDiamonds(0);
	}

	private void CloseWindow()
	{
		GameObject.Destroy(_dailyRewardView.gameObject);
		_currencyController.CloseWindow();
	}

	protected override void OnDispose()
	{
		_dailyRewardView.GetRewardButton.onClick.RemoveAllListeners();
		_dailyRewardView.ResetButton.onClick.RemoveAllListeners();
		_dailyRewardView.CloseWindow.onClick.RemoveAllListeners();

		base.OnDispose();
	}
}
