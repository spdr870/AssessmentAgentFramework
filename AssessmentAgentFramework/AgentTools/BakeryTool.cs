using System.ComponentModel;
using System.Net;

namespace AssessmentAgentFramework.AgentTools
{
	public class BakeryTool
	{
		[Description(@"Bake cookies")]
		public void BakeCookies([Description("How many cookies to bake")] int howMany)
		{
			string cookie = "🍪";
			Console.WriteLine($"#OVEN# {string.Concat(Enumerable.Repeat(cookie, howMany))}");
		}


		[Description(@"Bake pizza")]
		public void BakePizza([Description("How many pizzas to make")] int howMany)
		{
			string pizza = "🍕";
			Console.WriteLine($"#OVEN# {string.Concat(Enumerable.Repeat(pizza, howMany))}");
		}
	}
}
