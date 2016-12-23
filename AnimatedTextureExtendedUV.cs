using UnityEngine;
using System.Collections;

public class AnimatedTextureExtendedUV : MonoBehaviour {
		
		//vars for animation
		public int colNumber = 0; //Zero Indexed
		public int totalCells = 4;
		public int  fps     = 10;
		public bool noBump;
		//Update
		void Update () { SetSpriteAnimation(colNumber,totalCells,fps);  }
		
		//SetSpriteAnimation
		void SetSpriteAnimation(int colNumber,int totalCells,int fps ){
			
			// Calculate index
			int index  = (int)(Time.time * fps);
			// Repeat when exhausting all cells
			index = index % totalCells;
			
			// Size of every cell
			float sizeX = 1.0f / totalCells;
			float sizeY = 1;
			Vector2 size =  new Vector2(sizeX,sizeY);
			
			// split into horizontal and vertical index
			var uIndex = index % totalCells;
			var vIndex = index / totalCells;
			
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			float offsetX = (uIndex+colNumber) * size.x;
			float offsetY = (1.0f - size.y) - (vIndex) * size.y;
			Vector2 offset = new Vector2(offsetX,offsetY);
			
			GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
			GetComponent<Renderer>().material.SetTextureScale  ("_MainTex", size);
		if (!noBump) {
			GetComponent<Renderer>().material.SetTextureOffset ("_BumpMap", offset);
			GetComponent<Renderer>().material.SetTextureScale ("_BumpMap", size);
		}
		}
	}

