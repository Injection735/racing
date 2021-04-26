using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
	public class MainMenuView : MonoBehaviour
	{
		[SerializeField] private Button _buttonStart;

		[SerializeField] private Button _buttonWatchDailyReward;

		[SerializeField] private Button _buttonExitGame;

		public void Init(UnityAction startGame, UnityAction watchDailyReward)
		{
			_buttonStart.onClick.AddListener(startGame);
			_buttonWatchDailyReward.onClick.AddListener(watchDailyReward);
			_buttonExitGame.onClick.AddListener(ExitGame);
		}

		protected void OnDestroy()
		{
			_buttonStart.onClick.RemoveAllListeners();
			_buttonWatchDailyReward.onClick.RemoveAllListeners();
			_buttonExitGame.onClick.RemoveAllListeners();
		}

		private void ExitGame()
		{
			Application.Quit();
		}
	}
}
