using System.Linq;
using Company.Project.Content;
using Company.Project.ContentData;
using Company.Project.Features.Inventory;
using Game.Trail;
using Profile;
using Tools;
using Company.Project.Features.Shed;
using UnityEngine;
using Company.Project.Features.Items;
using System.Collections.Generic;

namespace Ui
{
	public class MainMenuController : BaseController
	{
		#region Fields

		private readonly ProfilePlayer _profilePlayer;
		private readonly MainMenuView _view;

		private readonly ShedController _shedController;

		#endregion

		#region Life cycle

		public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
		{
			_profilePlayer = profilePlayer;
			_view = ResourceLoader.LoadAndInstantiateObject<MainMenuView>(new ResourcePath { PathResource = "Prefabs/MainMenu" }, placeForUi, false);
			AddGameObjects(_view.gameObject);
			_view.Init(StartGame, DailyRewardGame);

			// можно внедрить как зависимость для другого контроллера
			var cursorTrailController = ConfigureCursorTrail();
			_shedController = ConfigureShedController(placeForUi, profilePlayer);

			_shedController.Enter();
		}

		#endregion

		#region Methods

		public IReadOnlyList<IItem> GetEquipedItems()
		{
			return _shedController.GetEquipedItems();
		}

		private void DailyRewardGame()
		{
			_profilePlayer.CurrentState.Value = GameState.DailyReward;
		}


		private BaseController ConfigureCursorTrail()
		{
			var cursorTrailController = new CursorTrailController();
			AddController(cursorTrailController);
			return cursorTrailController;
		}

		private ShedController ConfigureShedController(Transform placeForUi, ProfilePlayer profilePlayer)
		{
			var upgradeItemsConfigCollection = ContentDataSourceLoader.LoadUpgradeItemConfigs(new ResourcePath { PathResource = "DataSource/Upgrade/UpgradeItemConfigDataSource" });
			var upgradeItemsRepository = new UpgradeHandlersRepository(upgradeItemsConfigCollection);

			var itemsRepository = new ItemsRepository(upgradeItemsConfigCollection.Select(value => value.itemConfig).ToList());
			var inventoryModel = new InventoryModel();
			var inventoryViewPath = new ResourcePath { PathResource = $"Prefabs/{nameof(InventoryView)}" };

			var inventoryView = ResourceLoader.LoadAndInstantiateObject<InventoryView>(inventoryViewPath, placeForUi, false);
			AddGameObjects(inventoryView.gameObject);

			var inventoryController = new InventoryController(itemsRepository, inventoryModel, inventoryView);
			AddController(inventoryController);

			var shedController = new ShedController(upgradeItemsRepository, inventoryController, profilePlayer.CurrentCar);
			AddController(shedController);

			return shedController;
		}

		private void StartGame()
		{
			_profilePlayer.CurrentState.Value = GameState.Game;
			_profilePlayer.AnalyticTools.SendMessage("start_game");
		}

		#endregion
	}
}

