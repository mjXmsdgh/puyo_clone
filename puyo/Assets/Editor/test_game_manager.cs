using game_field;
using game_manager;
using NUnit.Framework;
using puyopuyo_space;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_game_manager {

	[Test]
	public void test_init () {
		GameManager test_target = new GameManager ();
		test_target.init ();
	}

	[Test]
	public void test_move () {
		GameManager test_target = new GameManager ();
		test_target.init ();

		puyopuyo temp = test_target.get_temp_puyo ();

		//-----set-----
		temp.set_position (0, 1, 2);
		temp.set_position (1, 1, 3);

		//move
		test_target.move (1, 2);

		//check 0
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_x (), 2);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_y (), 4);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_x (), 2);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_y (), 5);

		//-----set-----
		temp.set_position (0, 0, 0);
		temp.set_position (1, 0, 1);

		//move
		test_target.move (-1, -1);

		//check 0
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_x (), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_y (), 0);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_x (), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_y (), 1);
	}

	[Test]
	public void test_rotate () {
		GameManager test_target = new GameManager ();
		test_target.init ();

		puyopuyo temp = test_target.get_temp_puyo ();

		//-----set-----
		temp.set_position (0, 1, 2);
		temp.set_position (1, 1, 3);

		//move
		test_target.rotate (true);

		//check 0
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_x (), 1);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_y (), 2);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_x (), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_y (), 2);

		//-----set-----
		temp.set_position (0, 0, 0);
		temp.set_position (1, 0, 1);

		//move
		test_target.rotate (true);

		//check 0
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_x (), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (0).get_y (), 0);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_x (), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position (1).get_y (), 1);
	}

	[Test]
	public void test_fix () {

	}

	[Test]
	public void test_next2temp () {
		GameManager test_target = new GameManager ();
		test_target.init ();

		test_target.next2temp ();

		puyopuyo test = test_target.get_temp_puyo ();

		Assert.AreEqual (3, test.get_position (0).get_x ());
		Assert.AreEqual (10, test.get_position (0).get_y ());
		Assert.AreEqual (3, test.get_position (1).get_x ());
		Assert.AreEqual (11, test.get_position (1).get_y ());
	}
}