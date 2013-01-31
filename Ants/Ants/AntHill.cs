/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 10:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Ants
{
	class Water : Worldable
	{
		public Water(int x, int y) : base(x, y, "Water")
		{
			
		}
		
		override public List<Worldable> getObjects()
		{
			return new List<Worldable>();
		}
	}
	
	class AntHill : Worldable
	{
		public List<Worldable> antz = new List<Worldable>();
		
		private int food_storage;
		private int water_storage;
		private static int init_id = 0;
		private int anthill_id = 0;
		private int num_food_ants = 4;
		private int num_ants;
		
		private int score, prev_score;
		private int delta_score;
		private bool score_trend;
		
		private int score_counter = 0, score_max_counter = 5;
		int food_counter;
		
		int choice_a = 0;
		
		public AntHill enemyHill;
		
		public AntHill(int x, int y) : base(x, y, "Anthill")
		{
			food_storage = 150;
			water_storage = 0;
			anthill_id = init_id++;
			hasObjects = true;
			
			score = 0;
			prev_score = 0;
			delta_score = 0;
			score_trend = false;
		}
		
		public void setEnemyHill(AntHill k)
		{
			enemyHill = k;
		}
		
		override public List<Worldable> getObjects()
		{	
			return antz;
		}
		
		public void incFood(int i)
		{
			food_storage += i;
		}
		
		public void incWater(int i)
		{
			water_storage += i;
		}
		
		public int getFood()
		{
			return food_storage;
		}
		
		public void run()
		{
			prev_score = score;
			
			foreach(Ant ant in antz)
			{
				ant.run();
				foreach(Food f in World.foodz)
				{
					if(f.getFoodAmount() == 0)
					{
						if(World.foodz.Remove(f));
						break;
					}
				}
			}
			if(food_counter%2 == 0)
			{
				//food_storage--;
			}
			
			if(food_storage >= 10)
			{
				food_storage -= 10;
				num_food_ants = num_ants++;
				
			}
			
			score = food_storage - num_ants + water_storage;
			
			delta_score = score - prev_score;
			
			if(delta_score < 0)
			{
				score_counter++;
			}
			if(score_counter > score_max_counter)
			{
				adjustAntNum();
				score_counter = 0;
				if(enemyHill.score > score)
				{
					doSomething();
				}
			}
			
			if(num_ants > antz.Count)
			{
				antz.Add(new Ant(getX(), getY(), this));
			}
			else if(num_ants < antz.Count)
			{
				antz.RemoveAt(0);
			}
			food_counter++;
		}
		
		private void adjustAntNum()
		{
			Random joe = new Random();
			
			if(joe.Next()%2 == 0)
			{
				num_food_ants -= 1;
			}
			else
			{
				num_food_ants += 1;
			}
			if(num_food_ants < 0)
			{
				num_food_ants = 0;
			}
			num_ants = num_food_ants;
		}
		
		public void doSomething()
		{
			Random joe = new Random();
			
			//joe.
			
			if(enemyHill.num_food_ants >= num_food_ants)
			{
				num_food_ants++;
			}
			else
			{
				num_food_ants--;
			}
			num_ants = num_food_ants;
		}
		
		public int getNumFoodAnts()
		{
			return num_food_ants;
		}
		
		public string toString()
		{
			string message = "";
			
			message += "AntHill: " + score + " " + score_counter + " F: " + food_storage + ", " + num_ants;
			
			return message;
		}
	}
}
