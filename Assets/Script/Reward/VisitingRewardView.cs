using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitingRewardView : MonoBehaviour
{
	[Header("Settings Time Get Reward")]
	[SerializeField] private long _dayRewardDelay = 86400;

	[Header("Settings Rewards")]
	[SerializeField] private Reward _dailyReward;
	[SerializeField] private Reward _weeklyReward;
	
	[Header("Prefabs")]
	[SerializeField] private SlotRewardView _slot;
	[SerializeField] private Transform _root;

	[SerializeField] private Button _getDailyReward;
	[SerializeField] private Button _getWeeklyReward;

	public SlotRewardView Slot => _slot;

	public Button GetDailyRewardButton => _getDailyReward;
	public Button GetWeeklyRewardButton => _getWeeklyReward;

	public Reward DailyReward => _dailyReward;
	public Reward WeeklyReward => _weeklyReward;

	public Transform Root => _root;

	public float DayRewardDelay => _dayRewardDelay;

	private void OnDestroy()
	{
		_getDailyReward.onClick.RemoveAllListeners();
		_getWeeklyReward.onClick.RemoveAllListeners();
	}
}
