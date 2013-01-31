/*
 * Created by SharpDevelop.
 * User: Elemental
 * Date: 1/3/2013
 * Time: 8:07 PM
 * 
 * To change this template use Tools > Options > Coding > Edit Standard Headers.
 */
using System;

namespace Ants
{	
	class Program
	{
		public static void Main(string[] args)
		{
			World bob = new World(60, 60);
			bob.run();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}