using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWindowObserver : MonoBehaviour
{
	[SerializeField] private Text _countMoneyText;
	[SerializeField] private Text _countHealthText;
	[SerializeField] private Text _countPowerText;
	[SerializeField] private Text _countCrimeText;
	[SerializeField] private Text _countPowerEnemyText;

	[SerializeField] private Button _addCoinsButton;
	[SerializeField] private Button _minusCoinsButton;

	[SerializeField] private Button _addHealthButton;
	[SerializeField] private Button _minusHealthButton;

	[SerializeField] private Button _addPowerButton;
	[SerializeField] private Button _minusPowerButton;

	[SerializeField] private Button _addCrimeButton;
	[SerializeField] private Button _minusCrimeButton;

	[SerializeField] private Button _fightButton;
	[SerializeField] private Button _escapeButton;

	private Dictionary<DataType, DataProperty> _properties = new Dictionary<DataType, DataProperty>
	{
		{ DataType.Money,	new DefaultDataProperty(DataType.Money) },
		{ DataType.Health,	new DefaultDataProperty(DataType.Health) },
		{ DataType.Force,	new DefaultDataProperty(DataType.Force) },
		{ DataType.Crime,	new DefaultDataProperty(DataType.Crime) },
	};

	private Money _money;
	private Health _heath;
	private Force _power;

	private Enemy _enemy;

	private void Start()
	{
		_enemy = new Enemy("Enemy Flappy");

		_money = new Money(nameof(Money));
		_money.Attach(_enemy);

		_heath = new Health(nameof(Health));
		_heath.Attach(_enemy);

		_power = new Force(nameof(Force));
		_power.Attach(_enemy);

		_properties[DataType.Money].SubscribeButton(_minusCoinsButton, _addCoinsButton);
		_properties[DataType.Health].SubscribeButton(_minusHealthButton, _addHealthButton);
		_properties[DataType.Force].SubscribeButton(_minusPowerButton, _addPowerButton);
		_properties[DataType.Crime].SubscribeButton(_minusCrimeButton, _addCrimeButton);

		foreach(DataType type in System.Enum.GetValues(typeof(DataType)))
			_properties[type].OnClickObserver += ChangeDataWindow;

		_fightButton.onClick.AddListener(Fight);
		_escapeButton.onClick.AddListener(Escape);
	}

	private void OnDestroy()
	{
		_addCoinsButton.onClick.RemoveAllListeners();
		_minusCoinsButton.onClick.RemoveAllListeners();

		_addHealthButton.onClick.RemoveAllListeners();
		_minusHealthButton.onClick.RemoveAllListeners();

		_addPowerButton.onClick.RemoveAllListeners();
		_minusPowerButton.onClick.RemoveAllListeners();

		_fightButton.onClick.RemoveAllListeners();

		_money.Detach(_enemy);
		_heath.Detach(_enemy);
		_power.Detach(_enemy);
	}

	private void Escape()
	{
		Debug.Log(_allCountCrimePlayer <= CrimeDataProperty.MIN_CRIME_TO_ESCAPE ? "<color=#07FF00>Escaped!!!</color>" : "<color=#FF0000>Can't escape!!!</color>");
	}

	private void Fight()
	{
		Debug.Log(_allCountPowerPlayer >= _enemy.Force ? "<color=#07FF00>Win!!!</color>" : "<color=#FF0000>Lose!!!</color>");
	}

	private void ChangeDataWindow(int countChangeData, DataType dataType)
	{
		switch (dataType)
		{
			case DataType.Money:
				_countMoneyText.text = $"Player Money {countChangeData.ToString()}";
				_money.Money = countChangeData;
				break;

			case DataType.Health:
				_countHealthText.text = $"Player Health {countChangeData.ToString()}";
				_heath.Health = countChangeData;
				break;

			case DataType.Force:
				_countPowerText.text = $"Player Power {countChangeData.ToString()}";
				_power.Force = countChangeData;
				break;

			case DataType.Crime:
				_countCrimeText.text = $"Player Crime {countChangeData.ToString()}";
				_power.Crime = countChangeData;
				_escapeButton.gameObject.SetActive(_power.Crime <= CrimeDataProperty.MIN_CRIME_TO_ESCAPE);
				break;
		}

		_countPowerEnemyText.text = $"Enemy Power {_enemy.Force}";
	}
}
