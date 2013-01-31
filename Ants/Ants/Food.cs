/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 9:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Ants
{
	class Food : Worldable
	{
		//TODO not sure where this should be, but
		// make it so that food can only be collected by one ant
		// at a time and so that food takes like 5 iterations or w/e
		// to collect
		private int food_amount;
		
		public Food(int x, int y) : base(x, y, "Food")
		{
			food_amount = 50;	
		}
		
		public Food(int v, int w, int f) : base(v, w, "Food")
		{
			food_amount = f;
		}
		
		override public List<Worldable> getObjects()
		{
			return new List<Worldable>();
		}
		
		public int getFoodAmount()
		{
			return food_amount;
		}
		
		public void setFoodAmount(int i)
		{
			food_amount = i;
		}
		
		public void incFoodAmount(int i)
		{
			food_amount += i;
		}
		
		public void gatherFood(int x)
		{
			if(food_amount - x < 0)
			{
				food_amount = 0;
			}
			if(food_amount > 0)
			{
				food_amount -= x;
			}
		}
		
		public string toString()
		{
			String message = "";
			
			message += getX() + ", " + getY() + " food amount: " + food_amount;
			
			return message;
		}
	}
}
