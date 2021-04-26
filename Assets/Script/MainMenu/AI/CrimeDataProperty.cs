public class CrimeDataProperty : DataProperty
{
	public const int MAX_CRIME = 5;
	public const int MIN_CRIME_TO_ESCAPE = 2;

	public CrimeDataProperty(DataType type) : base(type)
	{ }

	public override void Change(bool isAddCount)
	{
		Value += isAddCount ? 1 : -1;

		if (Value < 0)
			Value = 0;

		if (Value > MAX_CRIME)
			Value = MAX_CRIME;

		OnClickObserver(Value, _type);
	}
}