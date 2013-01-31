/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 10:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Ants
{
	abstract class Worldable
	{
		private int x, y;
		public string type = "";
		public bool hasObjects;
		
		public Worldable(int v, int w, string s)
		{
			x = v;
			y = w;
			type = s;
			hasObjects = false;
		}
		
		abstract public List<Worldable> getObjects();
		
		public int getX()
		{
			return x;
		}
		
		public int getY()
		{
			return y;
		}
		
		public void incX(int i)
		{
			x += i;
		}
		
		public void incY(int i)
		{
			y += i;
		}
		
		protected void moveTo(int new_x, int new_y)
		{
			if(new_x < getX())
			{
				incX(-1);
			}
			else if(new_x > getX())
			{
				incX(1);
			}
			
			if(new_y < getY())
			{
				incY(-1);
			}
			else if(new_y > getY())
			{
				incY(1);
			}
		}	
		
		protected bool atLoc(int new_x, int new_y)
		{
			if(new_x == getX() && new_y == getY())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public static double distance(int x1, int y1, int x2, int y2)
		{
			return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
		}
	}
}
