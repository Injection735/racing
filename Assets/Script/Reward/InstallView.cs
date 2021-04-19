using UnityEngine;
public class InstallView : MonoBehaviour
{
	[SerializeField] private DailyRewardView _dailyRewardView;
	[SerializeField] private VisitingRewardView _visitRewardView;

	private DailyRewardController _dailyRewardController;
	private VisitingRewardController _visitRewardController;

	private void Awake()
	{
		_dailyRewardController = new DailyRewardController(_dailyRewardView);
		_visitRewardController = new VisitingRewardController(_visitRewardView);
	}

	private void Start()
	{
		_dailyRewardController.RefreshView();
		_visitRewardController.RefreshView();
	}
}
