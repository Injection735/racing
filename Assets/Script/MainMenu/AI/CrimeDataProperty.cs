public class CrimeDataProperty : DataProperty
{
	public const int MAX_CRIME = 5;
	public const int MIN_CRIME_TO_ESCAPE = 2;

	public CrimeDataProperty(DataType type) : base(type)
	{ }

	public override void Change(bool isAddCount)
	{
		value += isAddCount ? 1 : -1;

		if (value < 0)
			value = 0;

		if (value > MAX_CRIME)
			value = MAX_CRIME;

		OnClickObserver(value, _type);
	}
}