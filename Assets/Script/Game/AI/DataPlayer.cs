using System.Collections.Generic;

abstract class DataPlayer
{
	private string _titleData;
	private int _countMoney;
	private int _countHealth;
	private int _countForce;
	private int _countCrime;

	private List<IEnemy> _enemies = new List<IEnemy>();

	protected DataPlayer(string titleData)
	{
		_titleData = titleData;
	}

	public void Attach(IEnemy enemy)
	{
		_enemies.Add(enemy);
	}

	public void Detach(IEnemy enemy)
	{
		_enemies.Remove(enemy);
	}

	protected void Notify(DataType dataType)
	{
		foreach (var investor in _enemies)
			investor.Update(this, dataType);
	}

	public string TitleData => _titleData;

	public int Money
	{
		get => _countMoney;
		set
		{
			if (_countMoney != value)
			{
				_countMoney = value;
				Notify(DataType.Money);
			}
		}
	}

	public int Health
	{
		get => _countHealth;
		set
		{
			if (_countHealth != value)
			{
				_countHealth = value;
				Notify(DataType.Health);
			}
		}
	}

	public int Force
	{
		get => _countForce;
		set
		{
			if (_countForce != value)
			{
				_countForce = value;
				Notify(DataType.Force);
			}
		}
	}

	public int Crime
	{
		get => _countCrime;
		set
		{
			if (_countCrime != value)
			{
				_countCrime = value;
				Notify(DataType.Crime);
			}
		}
	}
}

class Money : DataPlayer
{
	public Money(string titleData)
	    : base(titleData)
	{
	}
}

class Health : DataPlayer
{
	public Health(string titleData)
	    : base(titleData)
	{
	}
}

class Force : DataPlayer
{
	public Force(string titleData)
	    : base(titleData)
	{
	}
}

class Crime : DataPlayer
{
	public Crime(string titleData)
	    : base(titleData)
	{
	}
}
