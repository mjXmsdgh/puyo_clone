using System.Collections;
using next_field;
using NUnit.Framework;
using puyopuyo_space;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Next {

	[Test] //initのテスト
	public void test_init () {
		next_puyo test_target = new next_puyo ();
		test_target.init ();

		puyopuyo ret_puyo = test_target.get ();

		//puyo
		Assert.AreNotEqual (ret_puyo, null);

		//puyo color
		for (int i = 0; i < 2; i++) {
			int color = ret_puyo.get_color (i);
			Assert.AreEqual (3, color, 3);
		}

		//puyo pos
		for (int i = 0; i < 2; i++) {
			int pos_x = ret_puyo.get_position_x (i);
			int pos_y = ret_puyo.get_position_y (i);
			Assert.AreEqual (0, pos_x);
			Assert.AreEqual (0, pos_y);
		}
	}

	[Test] //update_nextのテスト
	public void test_update_next () {
		next_puyo test_target = new next_puyo ();
		test_target.init ();

		int[] color = new int[2];

		bool flag = false;

		color[0] = test_target.get ().get_color (0);
		color[1] = test_target.get ().get_color (1);

		for (int i = 0; i < 10; i++) {
			puyopuyo ret_puyo = test_target.get ();

			if ((color[0] != ret_puyo.get_color (0)) || (color[1] != ret_puyo.get_color (1))) {
				flag = true;
				break;
			}

			test_target.update_next ();
		}

		Assert.AreEqual (true, flag);
	}

	[Test] //getのテスト
	public void test_get () {
		next_puyo test_target = new next_puyo ();
		test_target.init ();

		puyopuyo obj = test_target.get ();

		Assert.AreNotEqual (null, obj);
	}
}