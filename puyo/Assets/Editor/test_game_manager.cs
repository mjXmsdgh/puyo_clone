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
	public void test_get_gamefield () {
		GameManager test_target = new GameManager ();
		test_target.init ();

		GameField gf = test_target.get_gamefield ();
		Assert.AreNotEqual (null, gf);
	}

	[Test]
	public void test_get_next_puyo () {
		GameManager test_target = new GameManager ();
		test_target.init ();

		puyopuyo p = test_target.get_next_puyo ();
		Assert.AreNotEqual (null, p);
	}

	[Test]
	public void test_get_temp_puyo () {
		GameManager test_target = new GameManager ();
		test_target.init ();

		puyopuyo p = test_target.get_temp_puyo ();
		Assert.AreNotEqual (null, p);
	}

	[Test]
	public void test_getWidth () {
		GameManager test_target = new GameManager ();
		test_target.init ();
		Assert.AreEqual (6, test_target.getWidth ());
	}

	[Test]
	public void test_getHeight () {
		GameManager test_target = new GameManager ();
		test_target.init ();
		Assert.AreEqual (12, test_target.getHeight ());
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
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (0), 2);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (0), 4);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (1), 2);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (1), 5);

		//-----set-----
		temp.set_position (0, 0, 0);
		temp.set_position (1, 0, 1);

		//move
		test_target.move (-1, -1);

		//check 0
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (0), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (0), 0);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (1), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (1), 1);
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
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (0), 1);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (0), 2);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (1), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (1), 2);

		//-----set-----
		temp.set_position (0, 0, 0);
		temp.set_position (1, 0, 1);

		//move
		test_target.rotate (true);

		//check 0
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (0), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (0), 0);

		//check 1
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_x (1), 0);
		Assert.AreEqual (test_target.get_temp_puyo ().get_position_y (1), 1);
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

		Assert.AreEqual (3, test.get_position_x (0));
		Assert.AreEqual (10, test.get_position_y (0));
		Assert.AreEqual (3, test.get_position_x (1));
		Assert.AreEqual (11, test.get_position_y (1));
	}

	[Test]
	public void test_get_state () {

	}

	[Test]
	public void test_fall () {

	}

	[Test]
	public void test_check_delete () {

	}

	[Test]
	public void test_delete () {

	}

	[Test]
	public void test_get_rensa () {

	}
}