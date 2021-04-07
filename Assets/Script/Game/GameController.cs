using Company.Project.ContentData;
using Company.Project.Features.Abilities;
using Company.Project.Features.Inventory;
using Company.Project.Features.Items;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Game
{
	public class GameController : BaseController
	{
		#region Life cycle

		public GameController(Transform placeForUi, ProfilePlayer profilePlayer, IReadOnlyList<IItem> items)
		{
			var leftMoveDiff = new SubscriptionProperty<float>();
			var rightMoveDiff = new SubscriptionProperty<float>();
			var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
			AddController(tapeBackgroundController);

			var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
			AddController(inputGameController);

			var carController = new CarController(profilePlayer);
			AddController(carController);

			// можно внедрить как зависимость для другого контроллера
			var abilityController = ConfigureAbilityController(placeForUi, carController, items);
			abilityController.ShowAbilities();
		}

		#endregion

		#region Methods

		private IAbilitiesController ConfigureAbilityController(Transform placeForUi, IAbilityActivator abilityActivator, IReadOnlyList<IItem> items)
		{
			var abilityItemsConfigCollection = ContentDataSourceLoader.LoadAbilityItemConfigs(new ResourcePath { PathResource = "DataSource/Abilities/AbilityItemConfigDataSource" });
			var abilityRepository = new AbilityRepository(abilityItemsConfigCollection);
			var abilityCollectionViewPath = new ResourcePath { PathResource = $"Prefabs/{nameof(AbilityCollectionView)}" };
			var abilityCollectionView = ResourceLoader.LoadAndInstantiateObject<AbilityCollectionView>(abilityCollectionViewPath, placeForUi, false);
			AddGameObjects(abilityCollectionView.gameObject);

			// загрузить в модель экипированные предметы можно любым способом
			var inventoryModel = new InventoryModel();
			inventoryModel.EquipItems(items);

			var abilitiesController = new AbilitiesController(abilityRepository, inventoryModel, abilityCollectionView, abilityActivator);
			AddController(abilitiesController);

			return abilitiesController;
		}

		#endregion
	}
}

