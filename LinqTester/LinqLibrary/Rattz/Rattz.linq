<Query Kind="Statements">
  <Connection>
    <ID>ce7964ce-865f-4e93-8016-6d2a0806db5a</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Dev.2014\DAL\EF\Rattz\Rattz\bin\Debug\Rattz.dll</CustomAssemblyPath>
    <CustomTypeName>Rattz.RattzEntities</CustomTypeName>
    <AppConfigPath>C:\Dev.2014\DAL\EF\Rattz\Rattz\bin\Debug\Rattz.dll.config</AppConfigPath>
  </Connection>
</Query>

// =====================================================
// Join - Equivalents using .Include on the many
// =====================================================

// Listing 4-28
// Join - list the many each on its own line
using (var ctx = new RattzEntities()) {
	Console.WriteLine("{0} : Begin", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
	var employeeOptions = ctx.Employees
		.Include("EmployeeOptions")
		.AsEnumerable()
		.Select (e => new {Id=e.Id, Name=string.Format("{0} {1}",e.FirstName,e.LastName),Options=e.EmployeeOptions.Select (eo => eo.OptionsCount)})
		.Dump();
	// Format as in book
	foreach (var element in employeeOptions) {
		foreach (var option in element.Options)	{
			Console.WriteLine("id: {0}, name: {1}, options: {2}", element.Id, element.Name, option);
		}
	}
	Console.WriteLine("{0} : End", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
}

// Listing 4-29
// GroupJoin - list the sum of the many on 1 line
using (var ctx = new RattzEntities()) {
	Console.WriteLine("{0} : Begin", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
	var employeeOptions = ctx.Employees
		.Include("EmployeeOptions")
		.AsEnumerable()
		.Select (e => new {Id=e.Id, Name=string.Format("{0} {1}",e.FirstName,e.LastName),Options=e.EmployeeOptions
		.Sum (eo => eo.OptionsCount)})
		.Dump();
	Console.WriteLine("{0} : End", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
}

// =====================================================
// Grouping
// =====================================================

// Listing 4-30 (1st GroupBy Prototype)
using (var ctx = new RattzEntities()) {
	Console.WriteLine("{0} : Begin", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
	
	EmployeeOption[] empOptions = ctx.EmployeeOptions.ToArray();
	IEnumerable<IGrouping<int, EmployeeOption>> outerSequence =
		empOptions.GroupBy(o => o.EmployeeId);
	
	//  First enumerate through the outer sequence of IGroupings.
	foreach (IGrouping<int, EmployeeOption> keyGroupSequence in outerSequence)
	{
		Console.WriteLine("Option records for employee: " + keyGroupSequence.Key);
		
		//  Now enumerate through the grouping's sequence of EmployeeOptionEntry elements.
		foreach (EmployeeOption element in keyGroupSequence)
			Console.WriteLine("id={0} : optionsCount={1} : dateAwarded={2:d}",
		element.EmployeeId, element.OptionsCount, element.DateAwarded);
	}
	
	Console.WriteLine("{0} : End", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
}


// Listing 4-31 (2nd GroupBy Prototype - Custom IComparer)
// Needs to be run as C# Program in Linqpad
//void Main()
//{
//      Console.WriteLine("{0} : Begin", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
//      //  Instead of instantiating the comparer on the fly, I am going
//      //  to keep a reference to is because I will use its isFounder()
//      //  method on the group's key for header display purposes.
//      MyFounderNumberComparer comp = new MyFounderNumberComparer();
//
//	using (var ctx = new RattzEntities()) {
//      EmployeeOption[] empOptions = ctx.EmployeeOptions.ToArray();
//      IEnumerable<IGrouping<int, EmployeeOption>> opts = empOptions
//        .GroupBy(o => o.EmployeeId, comp);
//
//      //  First enumerate through the sequence of IGroupings.
//      foreach (IGrouping<int, EmployeeOption> keyGroup in opts)
//      {
//        Console.WriteLine("Option records for: " +
//          (comp.isFounder(keyGroup.Key) ? "founder" : "non-founder"));
//
//        //  Now enumerate through the grouping's sequence of EmployeeOptionEntry elements.
//        foreach (EmployeeOption element in keyGroup)
//          Console.WriteLine("id={0} : optionsCount={1} : dateAwarded={2:d}",
//            element.Id, element.OptionsCount, element.DateAwarded);
//      }
//      Console.WriteLine("{0} : End", new StackTrace(0, true).GetFrame(0).GetMethod().Name);		
//	}
//}
//
//// Define other methods and classes here
//public class MyFounderNumberComparer : IEqualityComparer<int>  {
//	public bool Equals(int x, int y)   {
//		return (isFounder(x) == isFounder(y));
//	}
//		
//	public int GetHashCode(int i)   {
//		int f = 1;
//		int nf = 100;
//		return (isFounder(i) ? f.GetHashCode() : nf.GetHashCode());
//	}
//		
//	public bool isFounder(int id)    {
//		return (id < 100);
//	}
//}

// Listing 4-32 (3rd Prototype - Output part of the Input value)
using (var ctx = new RattzEntities() ) {
	Console.WriteLine("{0} : Begin", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
	
	EmployeeOption[] empOptions = ctx.EmployeeOptions.ToArray();
	IEnumerable<IGrouping<int, DateTime>> opts = empOptions
		.GroupBy(o => o.EmployeeId, o => o.DateAwarded);
	
	//  First enumerate through the sequence of IGroupings.
	foreach (IGrouping<int, DateTime> keyGroup in opts)
	{
		Console.WriteLine("Option records for employee: " + keyGroup.Key);
	
		//  Now enumerate through the grouping's sequence of DateTime elements.
		foreach (DateTime date in keyGroup)
		Console.WriteLine(date.ToShortDateString());
	}
	
	Console.WriteLine("{0} : End", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
}

// Listing 4.33 (4th Prototype - Custom IComparer and Output part of the Input Value)
// Combines 2nd and 3rd Prototypes
// Needs to be run as C# Program in Linqpad
//void Main()
//{
//	using (var ctx = new RattzEntities() ) {
//		Console.WriteLine("{0} : Begin", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
//		//  Instead of instantiating the comparer on the fly, I am going
//		//  to keep a reference to is because I will use its isFounder()
//		//  method on the group's key for header display purposes.
//		MyFounderNumberComparer comp = new MyFounderNumberComparer();
//		EmployeeOption[] empOptions = ctx.EmployeeOptions.ToArray();
//		IEnumerable<IGrouping<int, DateTime>> opts = empOptions
//			.GroupBy(o => o.EmployeeId, o => o.DateAwarded, comp);
//		
//		//  First enumerate through the sequence of IGroupings.
//		foreach (IGrouping<int, DateTime> keyGroup in opts)
//		{
//			Console.WriteLine("Option records for: " +
//				(comp.isFounder(keyGroup.Key) ? "founder" : "non-founder"));
//		
//			//  Now enumerate through the grouping's sequence of EmployeeOptionEntry elements.
//			foreach (DateTime date in keyGroup)
//				Console.WriteLine(date.ToShortDateString());
//		}
//		Console.WriteLine("{0} : End", new StackTrace(0, true).GetFrame(0).GetMethod().Name);
//	}
//}
//
//// Define other methods and classes here
//public class MyFounderNumberComparer : IEqualityComparer<int>  {
//	public bool Equals(int x, int y)   {
//		return (isFounder(x) == isFounder(y));
//	}
//		
//	public int GetHashCode(int i)   {
//		int f = 1;
//		int nf = 100;
//		return (isFounder(i) ? f.GetHashCode() : nf.GetHashCode());
//	}
//		
//	public bool isFounder(int id)    {
//		return (id < 100);
//	}
//}

// ===================================================================================
// Generation - not extension methods but a static methods on System.Linq.Enumerable
// ===================================================================================

// Range - generates a sequence of intergers
IEnumerable<int> ints = Enumerable.Range(1,10).Dump();

// Repeat - generates a sequence by repeating a specified element a specified number of times
IEnumerable<int> ints = Enumerable.Repeat(2,10).Dump();

// Empty - generates an empty sequence of a specified type
IEnumerable<string> str = Enumerable.Empty<string>().Dump();
IEnumerable<int> i = Enumerable.Empty<int>().Dump();
IEnumerable<Employee> i = Enumerable.Empty<Employee>().Dump();

// =====================================================
// Set
// =====================================================

// Distinct
using (var ctx = new RattzEntities()) {
	IQueryable<string> firstNamesWithDupes = ctx.Employees.Select (e => e.FirstName).Concat(ctx.Employees.Select (e => e.FirstName)).Dump();
	IQueryable<string> firstNamesDistinct = ctx.Employees.Select (e => e.FirstName).Concat(ctx.Employees.Select (e => e.FirstName)).Distinct().Dump();
	IQueryable<Employee> employeesWithDupes = ctx.Employees.Concat(ctx.Employees).Dump();
	IQueryable<Employee> employeesDistinct = ctx.Employees.Concat(ctx.Employees).Distinct().Dump();
}

// Union (dups not included)
// Note: Skip requires sorted sequence
using (var ctx = new RattzEntities()) {
	IQueryable<Employee> first = ctx.Employees.OrderBy (e => e.FirstName).Take(3).Dump(); // Joe is 3rd
	IQueryable<Employee> second = ctx.Employees.OrderBy (e => e.FirstName).Skip(2).Dump(); // Joe is 1st
	IQueryable<Employee> concat = first.OrderBy (e => e.FirstName).Concat(second).Dump();  // Concat has 2 Joe
	IQueryable<Employee> union = first.OrderBy (e => e.FirstName).Union(second).OrderBy (e => e.FirstName).Dump();	// Union has 1 Joe. Note: need Union to be sorted.
}

// Intersect (dups are the result)
// Note: Skip requires sorted sequence
using (var ctx = new RattzEntities()) {
	IQueryable<Employee> first = ctx.Employees.OrderBy (e => e.FirstName).Take(3).Dump(); // Joe is 3rd
	IQueryable<Employee> second = ctx.Employees.OrderBy (e => e.FirstName).Skip(2).Dump();	// Joe is 1st
	IQueryable<Employee> concat = first.OrderBy (e => e.FirstName).Intersect(second).Dump();  // Joe should be the only intersection
}

// Except (elements of 1st sequence not in 2nd sequence)
// Note: Skip requires sorted sequence
using (var ctx = new RattzEntities()) {
	IQueryable<Employee> first = ctx.Employees.OrderBy (e => e.FirstName).Take(3).Dump(); // Joe is 3rd 
	IQueryable<Employee> second = ctx.Employees.OrderBy (e => e.FirstName).Skip(2).Dump();	// Joe is 1st
	IQueryable<Employee> concat = first.OrderBy (e => e.FirstName).Except(second).Dump();  // 1st 2 elements of 'first' are not in 'second'
}

// =====================================================
// Quantifiers
// =====================================================

// Any - return true if any element of input sequence matches a 'condition'.
using (var ctx = new RattzEntities()) {
	// returns true
	ctx.Employees.Any (e => e.FirstName.StartsWith("A")).Dump();
	// returns false
	ctx.Employees.Any (e => e.FirstName.StartsWith("B")).Dump();
}

// All - return true if every element of input sequence matches a 'condition'.
using (var ctx = new RattzEntities()) {
	// returns true
	ctx.Employees.All (e => e.FirstName.Length > 2).Dump();
	// returns false
	ctx.Employees.All (e => e.FirstName.Length == 3).Dump();
}

// Contains - return true if any element of input sequence matches the 'specified value'.
using (var ctx = new RattzEntities()) {
	// return true
	ctx.Employees.Select (e => e.FirstName).ToList().Contains("Joe").Dump();
	// return false
	ctx.Employees.Select (e => e.FirstName).ToList().Contains("Joey").Dump();

	// Can be used to compare against variable number of parameters.
	var names = new [] {"Ron", "Clara", "Joe"};
	ctx.Employees.Where(e => names.Contains(e.FirstName)).Dump();
	names = new [] {"Ron", "Clara"};
	ctx.Employees.Where(e => names.Contains(e.FirstName)).Dump();
}

// =====================================================
// Aggregate
// =====================================================

// Count - returns number of elements in the input sequence
// 1st prototype contains no parameters
using (var ctx = new RattzEntities()) {
	ctx.Employees.Count().Dump();
}
// 2nd prototype takes a predicate conditional
using (var ctx = new RattzEntities()) {
	ctx.Employees.Count(e => e.FirstName.StartsWith("J")).Dump();
}

// LongCount - returns number of elements in the input sequence as a long
// 1st prototype contains no parameters
using (var ctx = new RattzEntities()) {
	ctx.Employees.LongCount().Dump();
}
Enumerable.Range(0, int.MaxValue).LongCount().Dump();
// 2nd prototype takes a predicate conditional
using (var ctx = new RattzEntities()) {
	ctx.Employees.LongCount(e => e.FirstName.StartsWith("J")).Dump();
}
Enumerable.Range(0, int.MaxValue).LongCount(n => n > 1 && n < 4).Dump();

// Sum - returns the sum of numeric values contained in the elements of the input sequence.
// 1st prototype contains no parameters
Enumerable.Range(0, 10).Sum().Dump();
// 2nd prototype takes a selector parameter. 
// This prototype makes more sense with objects because selector can pick a property to sum on.
using (var ctx = new RattzEntities()) {
	ctx.EmployeeOptions.Sum (eo => eo.OptionsCount).Dump();
}
using (var ctx = new RattzEntities()) {
	ctx.EmployeeOptions.Where (eo => eo.EmployeeId == 2).Sum (eo => eo.OptionsCount).Dump();
}



// =====================================================
// String array of Presidents
// =====================================================
//      string[] presidents = { 
//        "Adams", "Arthur", "Buchanan", "Bush", "Carter", "Cleveland", 
//        "Clinton", "Coolidge", "Eisenhower", "Fillmore", "Ford",  "Garfield",
//        "Grant", "Harding", "Harrison", "Hayes", "Hoover", "Jackson",
//        "Jefferson", "Johnson", "Kennedy", "Lincoln", "Madison", "McKinley", 
//        "Monroe", "Nixon", "Pierce", "Polk", "Reagan", "Roosevelt", "Taft",
//        "Taylor", "Truman", "Tyler", "Van Buren", "Washington", "Wilson"};

