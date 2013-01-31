/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 9:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ants
{
	class Ant : Worldable
	{
		private int food_capacity = 5;
		
		
		bool hasFood;
		string status;
		
		private static int init_id = 0;
		private int ant_id = 0;
		
		private Job current_job;
		private AntHill anthill;
		
		private Food nearestFood;
		
		public Ant(int x, int y, AntHill a) : base(x,y, "Ant")
		{
			hasFood = false;
			status = "";
			anthill = a;
			ant_id = init_id++;
			nearestFood = null;
			
			hasObjects = false;
			
			current_job = new Job();
		}
		
		override public List<Worldable> getObjects()
		{
			return new List<Worldable>();
		}
		
		public void run()
		{
			if(current_job.getName() == "Idle")
			{
				current_job = assignJob();
			}
			doJob();
		}
		
		public Job getJob()
		{
			return current_job;
		}
		
		private Job assignJob()
		{
			Job temp = new Job();
			int food_ants = 0;
			int water_ants = 0;
			int stick_ants = 0;
			int idle_ants = 0;
			
			foreach(Ant bob in anthill.antz)
			{
				if(bob.getJob().getName() == "Idle")
				{
					idle_ants++;
				}
				else if(bob.getJob().getName() == "Food")
				{
					food_ants++;
				}
				else if(bob.getJob().getName() == "Water")
				{
					water_ants++;
				}
				else if(bob.getJob().getName() == "Sticks")
				{
					stick_ants++;
				}
			}
			
			if(food_ants < anthill.getNumFoodAnts())
			{
				temp = new Job("Food");
			}
			
			return temp;
		}
		
		private void doJob()
		{
			if(current_job.getName() == "Food")
			{
				if(hasFood)
				{
					moveTo(anthill.getX(), anthill.getY());
					status = "Moving to anthill";
					if(atLoc(anthill.getX(), anthill.getY()))
					{
						hasFood = false;
						anthill.incFood(food_capacity);
						current_job = new Job();
						status = "";
					}
				}
				else
				{
					findnearestfood();
					status = "Moving to nearest food";
					nearestFood = findnearestfood();
					moveTo(nearestFood.getX(), nearestFood.getY());
				}
				if(atLoc(nearestFood.getX(), nearestFood.getY()) && !hasFood)
				{
					gatherFood();
				}
			}
		}
		
		//TODO make it so ants dont have infina vision
		private Food findnearestfood()
		{
			double dist = 0;
			double min_dist = 100000;
			Food close = World.foodz[0];
			foreach(Food f in World.foodz)
			{
				dist = Worldable.distance(getX(), getY(), f.getX(), f.getY());
				if(dist < min_dist)
				{
					min_dist = dist;
					close = f;
				}
			}
			return close;
		}
		
		private void gatherFood()
		{
			nearestFood.gatherFood(food_capacity);
			status = "Gathered food";
			hasFood = true;
		}
	}
}
