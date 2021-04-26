using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWindowObserver : MonoBehaviour
{
	private const int MAX_CRIME = 5;
	private const int MIN_CRIME_TO_ESCAPE = 2;

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

	private int _allCountMoneyPlayer;
	private int _allCountHealthPlayer;
	private int _allCountPowerPlayer;
	private int _allCountCrimePlayer;

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

		_addCoinsButton.onClick.AddListener(() => ChangeMoney(true));
		_minusCoinsButton.onClick.AddListener(() => ChangeMoney(false));

		_addHealthButton.onClick.AddListener(() => ChangeHealth(true));
		_minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

		_addPowerButton.onClick.AddListener(() => ChangePower(true));
		_minusPowerButton.onClick.AddListener(() => ChangePower(false));

		_addCrimeButton.onClick.AddListener(() => ChangeCrime(true));
		_minusCrimeButton.onClick.AddListener(() => ChangeCrime(false));

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

	private void ChangeMoney(bool isAddCount)
	{
		if (isAddCount)
			_allCountMoneyPlayer++;
		else
			_allCountMoneyPlayer--;

		ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
	}

	private void ChangeHealth(bool isAddCount)
	{
		if (isAddCount)
			_allCountHealthPlayer++;
		else
			_allCountHealthPlayer--;

		ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
	}

	private void ChangePower(bool isAddCount)
	{
		if (isAddCount)
			_allCountPowerPlayer++;
		else
			_allCountPowerPlayer--;

		ChangeDataWindow(_allCountPowerPlayer, DataType.Force);
	}

	private void ChangeCrime(bool isAddCount)
	{
		if (isAddCount)
			_allCountCrimePlayer++;
		else
			_allCountCrimePlayer--;

		if (_allCountCrimePlayer < 0)
			_allCountCrimePlayer = 0;

		if (_allCountCrimePlayer > MAX_CRIME)
			_allCountCrimePlayer = MAX_CRIME;

		ChangeDataWindow(_allCountCrimePlayer, DataType.Crime);
	}

	private void Escape()
	{
		Debug.Log(_allCountCrimePlayer <= MIN_CRIME_TO_ESCAPE
		   ? "<color=#07FF00>Escaped!!!</color>"
		   : "<color=#FF0000>Can't escape!!!</color>");
	}

	private void Fight()
	{
		Debug.Log(_allCountPowerPlayer >= _enemy.Force
		   ? "<color=#07FF00>Win!!!</color>"
		   : "<color=#FF0000>Lose!!!</color>");
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
				_escapeButton.gameObject.SetActive(_power.Crime <= MIN_CRIME_TO_ESCAPE);
				break;
		}

		_countPowerEnemyText.text = $"Enemy Power {_enemy.Force}";
	}
}
