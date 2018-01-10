using System.Collections;
using game_field;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_gamefiled {

	[Test]
	public void test_init () {
		//GameField test_target = new GameField ();
		//Assert.AreEqual (0, 0);
	}

	[Test]
	public void test_next2temp () {

	}

	[Test]
	public void test_GetWidth () {
		GameField test_target = new GameField ();
		Assert.AreEqual (6, test_target.GetWidth ());
	}

	[Test]
	public void test_GetHeight () {
		GameField test_target = new GameField ();
		Assert.AreEqual (12, test_target.GetHeight ());
	}

	[Test]
	public void test_getState () {
		GameField test_target = new GameField ();
		Assert.AreEqual (0, test_target.get_state ());
	}

	[Test]
	public void test_setgetvalue () {
		GameField test_target = new GameField ();

		for (int i = 0; i < 6; i++) {
			test_target.set_value (i, i + 1, i);
			Assert.AreEqual (i, test_target.get_value (i, i + 1));
		}
	}

	[Test]
	public void test_get_next () {

	}

	[Test]
	public void test_get_temp () {

	}

	[Test]
	public void test_move () {

	}

	[Test]
	public void test_rotate () {

	}

	[Test]
	public void test_fix () {

	}

	[Test]
	public void test_fall () {

	}

	[Test]
	public void test_delete () {

	}
}