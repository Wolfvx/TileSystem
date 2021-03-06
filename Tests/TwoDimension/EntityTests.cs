﻿using Moq;

using NUnit.Framework;

using TileSystem.Interfaces.Base;
using TileSystem.Implementation.TwoDimension;

namespace Tests.TwoDimension
{
	[TestFixture]
	[Category("TwoDimension")]
	public class EntityTests
	{
		[Test]
		public void EntityCreate()
		{
			string type = "MyType";
			string variation = "MyVariation";

			// Empty Constructor
			Entity entity = new Entity();

			// Test Type
			Assert.IsNull(entity.Type);
			// Test Variation
			Assert.IsNull(entity.Variation);

			// Initialised Constructor
			Entity entityTwo = new Entity(type, variation);

			// Test Type
			Assert.AreSame(entityTwo.Type, type);
			// Test Variation
			Assert.AreSame(entityTwo.Variation, variation);
		}

		[Test]
		public void EntityDestroy()
		{
			Entity entity = new Entity();

			bool destroyedCalled = false;

			// Register destroyed event and make sure it is called
			entity.Destroyed += (sender, args) => 
			{
				destroyedCalled = true;
			};

			entity.Destroy();

			// Destroy event was fired
			Assert.IsTrue(destroyedCalled);
		}

		[Test]
		public void EntityDestroyTileRemove()
		{
			Entity entity = new Entity();
			var mockTile = new Mock<ITile>();

			// Set Tile Parent
			entity.SetTile(mockTile.Object);

			// Destroy
			entity.Destroy();

			// Make sure the tile remove was called with the entity
			mockTile.Verify(tile => tile.Remove(entity), Times.Exactly(1));
		}

		[Test]
		public void SetTile()
		{
			Entity entity = new Entity();
			var mockTile = new Mock<ITile>();

			// Test Null
			Assert.That(() => entity.SetTile(null), Throws.ArgumentNullException);
			// Test Set Parent Works
			Assert.That(() => entity.SetTile(mockTile.Object), Throws.Nothing);

			Assert.AreSame(entity.Tile, mockTile.Object);
		}
	}
}
