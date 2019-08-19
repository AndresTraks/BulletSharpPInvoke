using BulletSharp;
using NUnit.Framework;

namespace BulletSharpTest
{
	[TestFixture]
	public sealed class ObjectTrackerTests
	{
		[Test]
		public void DepencencyDisposeExceptionTest()
		{
			AssertObjectTrackerEnabled();

			Assert.That(BulletObjectTracker.Current.GetUserOwnedObjects(), Is.Empty);
		}

		private void AssertObjectTrackerEnabled()
		{
			if (BulletObjectTracker.Current == null)
			{
				Assert.Inconclusive("Bullet object tracker not enabled");
			}
		}
	}
}
