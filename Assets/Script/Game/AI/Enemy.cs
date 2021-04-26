using UnityEngine;

interface IEnemy
{
	void Update(DataPlayer dataPlayer, DataType dataType);
}

class Enemy : IEnemy
{
	private const int KCoins = 5;
	private const float KPower = 1.5f;
	private const int MaxHealthPlayer = 20;

	private string _name;
	private int _moneyPlayer;
	private int _healthPlayer;
	private int _powerPlayer;
	private int _crimePlayer;

	public Enemy(string name)
	{
		_name = name;
	}

	public void Update(DataPlayer dataPlayer, DataType dataType)
	{
		switch (dataType)
		{
			case DataType.Money:
				_moneyPlayer = dataPlayer.Money;
				break;

			case DataType.Health:
				_healthPlayer = dataPlayer.Health;
				break;

			case DataType.Force:
				_powerPlayer = dataPlayer.Force;
				break;

			case DataType.Crime:
				_crimePlayer = dataPlayer.Crime;
				break;
		}

		Debug.Log($"Notified {_name} change to {dataPlayer}");
	}

	public int Force
	{
		get
		{
			var kHealth = _healthPlayer > MaxHealthPlayer ? 100 : 5;
			var power = (int) (_moneyPlayer / KCoins + kHealth + _powerPlayer / KPower + _crimePlayer);

			return power;
		}
	}
}
