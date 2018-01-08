using System;
using puyopuyo_space;

namespace next_field {
	public class next_puyo {

		//next puyo
		private puyopuyo m_puyopuyo;
		private System.Random m_random = new System.Random ();

		//init
		public void init () {
			m_puyopuyo = new puyopuyo ();
			m_puyopuyo.init ();
			update_next ();
		}

		//update next
		public void update_next () {
			m_puyopuyo.set_color (0, getRandom ());
			m_puyopuyo.set_color (1, getRandom ());
		}

		//get random value
		int getRandom () {
			return m_random.Next (2, 6);
		}

		//get next puyo
		public puyopuyo get () {
			return m_puyopuyo;
		}
	}
}