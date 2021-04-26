using Company.Project.Features.Items;
using Game;
using Profile;
using System.Collections.Generic;
using Ui;
using UnityEngine;

public class MainController : BaseController
{
	public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
	    DailyRewardView dailyRewardView, CurrencyView currencyView,
	    FightWindowView fightWindowView, StartFightView startFightView)
	{
		_profilePlayer = profilePlayer;
		_placeForUi = placeForUi;
		_dailyRewardView = dailyRewardView;
		_currencyView = currencyView;
		_fightWindowView = fightWindowView;
		_startFightView = startFightView;

		OnChangeGameState(_profilePlayer.CurrentState.Value);
		profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
	}

	private MainMenuController _mainMenuController;
	private DailyRewardController _dailyRewardController;
	private FightWindowController _fightWindowController;
	private CurrencyController _currencyController;
	private StartFightController _startFightController;
	private GameController _gameController;

	private readonly Transform _placeForUi;
	private readonly ProfilePlayer _profilePlayer;
	private readonly DailyRewardView _dailyRewardView;
	private readonly CurrencyView _currencyView;
	private readonly FightWindowView _fightWindowView;
	private readonly StartFightView _startFightView;

	protected override void OnDispose()
	{
		DisposeAllControllers();

		_profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
		base.OnDispose();
	}

	private void OnChangeGameState(GameState state)
	{
		switch (state)
		{
			case GameState.Start:
				_mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
				_gameController?.Dispose();
				break;
			case GameState.Game:
				_gameController = new GameController(_placeForUi, _profilePlayer, new List<IItem>());

				_startFightController = new StartFightController(_placeForUi, _startFightView, _profilePlayer);
				_startFightController.RefreshView();

				_mainMenuController?.Dispose();
				_fightWindowController?.Dispose();
				break;
			case GameState.DailyReward:
				_dailyRewardController = new DailyRewardController(_placeForUi, _dailyRewardView, _currencyView);
				_dailyRewardController.RefreshView();
				break;
			case GameState.Fight:
				_fightWindowController = new FightWindowController(_placeForUi, _fightWindowView, _profilePlayer);
				_fightWindowController.RefreshView();

				_mainMenuController?.Dispose();
				_startFightController?.Dispose();
				_gameController?.Dispose();
				break;
			default:
				DisposeAllControllers();
				break;
		}
	}

	private void DisposeAllControllers()
	{
		_mainMenuController?.Dispose();
		_gameController?.Dispose();
		_fightWindowController?.Dispose();
		_dailyRewardController?.Dispose();
		_startFightController?.Dispose();
	}
}
