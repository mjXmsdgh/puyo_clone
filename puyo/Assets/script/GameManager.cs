using System;
using System.Collections.Generic;
using game_field;
using next_field;
using point_space;
using puyopuyo_space;

namespace game_manager {

	public class GameManager {
		private GameField m_game_field;
		private next_puyo m_next;
		private puyopuyo m_temp_puyo;

		public void init () {

			//フィールド
			m_game_field = new GameField ();
			m_game_field.init ();

			//現在のぷよ生成
			m_temp_puyo = new puyopuyo ();
			m_temp_puyo.init ();

			//nextの生成と初期化
			m_next = new next_puyo ();
			m_next.init ();
		}
		public GameField get_gamefield () {
			return m_game_field;
		}

		public puyopuyo get_next_puyo () {
			return m_next.get ();
		}

		public puyopuyo get_temp_puyo () {
			return m_temp_puyo;
		}

		public int getWidth () {
			return m_game_field.GetWidth ();
		}

		public int getHeight () {
			return m_game_field.GetHeight ();
		}

		public bool move (int move_x, int move_y) {
			m_temp_puyo.move (new Point (move_x, move_y));

			m_game_field.force_to_range (ref m_temp_puyo);

			//m_game_field
			return false;
		}

		public void fix () {
			m_game_field.fix ();
		}

		public void rotate (bool isRight) {
			m_game_field.rotate (isRight);
		}

		public void next2temp () {
			m_temp_puyo.copy_color (m_next.get ());
			m_temp_puyo.set_position (0, 3, 10);
			m_temp_puyo.set_position (1, 3, 11);

			m_next.update_next ();
		}

		public int get_state () {
			return m_game_field.get_state ();
		}

		public void fall () {
			m_game_field.fall ();
		}

		public void delete () {
			m_game_field.delete ();
		}

	}
}