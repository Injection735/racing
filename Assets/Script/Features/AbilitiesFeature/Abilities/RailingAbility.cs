using JetBrains.Annotations;
using Company.Project.Content;
using UnityEngine;

namespace Company.Project.Features.Abilities
{
	public class RailingAbility : IAbility
	{
		#region Fields

		private readonly AbilityItemConfig _config;

		#endregion

		#region Life cycle

		public RailingAbility([NotNull] AbilityItemConfig config)
		{
			_config = config;
		}

		#endregion

		#region IAbility

		public void Apply(IAbilityActivator activator)
		{
			activator.GetProfilePlayer().CurrentCar.Speed.Value += _config.value;
		}

		#endregion
	}
}