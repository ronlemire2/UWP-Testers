<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.dll</Reference>
</Query>

// This sample was taken from Pro LINQ by Adam Freeman and Joseph C. Rattz, Jr.
// pg 21 - 27
void Main()
{
	int[] nums = {1,2,3,4,5,6,7,8,9,10};
	
	"".Dump();
	"Using named method".Dump();
	int[] oddNums1 = Common.FilterArrayOfInts(nums, Application.IsOdd);
	oddNums1.Dump();
	
	"".Dump();
	"Using anonymous method".Dump();
	int[] oddNums2 = Common.FilterArrayOfInts(nums, delegate(int i) {
								return ((i & 1) == 1);
							});
	oddNums2.Dump();
	
	"".Dump();
	"Using lambda expression".Dump();
	int[] oddNums3 = Common.FilterArrayOfInts(nums, (i) => ((i & 1) == 1));
	oddNums3.Dump();
}

// Int Filter Utility that takes a delegate
public class Common {
	public delegate bool IntFilter(int i);
	
	public static int[] FilterArrayOfInts(int[] ints, IntFilter filter) {
		ArrayList aList = new ArrayList();
		foreach (int i in ints) {
			if (filter(i)) {
				aList.Add(i);
			}
		}
		return ((int[])aList.ToArray(typeof(int)));
	}
}

// Named delegate method
public class Application {
	public static bool IsOdd(int i) {
		return (i & 1) == 1;
	}
}