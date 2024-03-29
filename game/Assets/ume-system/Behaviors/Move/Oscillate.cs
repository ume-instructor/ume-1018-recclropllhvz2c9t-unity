using UnityEngine;
using System;
using System.Diagnostics;

namespace UME{
    [AddComponentMenu("UME/Move/Oscillate")]
    public class Oscillate : MonoBehaviour {
		public Vector2 direction = Vector2.right;
		[Range(0.1f, 10.0f)] public float amplitude = 10;
		[Range(0.1f, 10.0f)] public float frequency = 1;
		private Vector3 pos;
		private Rigidbody2D m_Rigidbody2D;
		private float startTime;
		private bool m_FacingRight = true;
		public bool flip = false;

		void Start () {
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
			//register initial position to oscillate around
			pos = this.transform.position;
			if (m_Rigidbody2D != null) {
				pos = m_Rigidbody2D.position;
			}
		}

		void FixedUpdate () {
			float oscillation = amplitude * (float)Math.Sin ( (float)Time.fixedTime * frequency); 
			Vector3 oscillation_pos = pos+(oscillation * new Vector3(direction.x,direction.y,0.0f));
			Vector3 move;
			if (m_Rigidbody2D != null){
				move = Vector3.MoveTowards (m_Rigidbody2D.position, oscillation_pos, 1.0f);
				if (flip) checkFlip (move, m_Rigidbody2D.position);
				m_Rigidbody2D.MovePosition (move);
			}else{
				move = Vector3.MoveTowards (this.transform.position, oscillation_pos,frequency);
				if (flip) checkFlip (move, this.transform.position);
				this.transform.position = move; 
			}
		}

		private void checkFlip(Vector3 move, Vector3 position){
			if (move.x > position.x && !m_FacingRight) {
				Flip ();
			}else if (move.x < position.x && m_FacingRight) {
				Flip ();
			}	

		}
		private void Flip (){
			m_FacingRight = !m_FacingRight;
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}
