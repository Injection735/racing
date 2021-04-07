using Tools;

namespace Company.Project.Features.Shed
{
	public interface IShedController
	{
		void Enter();
		void Exit();
	}

	public interface IUpgradeHandler
	{
		IUpgradable Upgrade(IUpgradable upgradable);
	}

	public interface IUpgradable
	{
		void Restore();
		SubscriptionProperty<float> Speed { get; set; }
	}
}