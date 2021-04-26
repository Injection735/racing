public class DefaultDataProperty : DataProperty
{
	public DefaultDataProperty(DataType type) : base(type)
	{ }

	public override void Change(bool isAddCount) => OnClickObserver(Value += isAddCount ? 1 : -1, _type);
}

