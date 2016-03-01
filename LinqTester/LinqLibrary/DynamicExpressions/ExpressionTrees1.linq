<Query Kind="Program" />

// Dynamic Expressions can also be built with PredicateBuilder.
// See https://github.com/ronlemire2/UWP-Testers/blob/master/LinqTester/LinqLibrary/DynamicExpressions/PredicateBuilder.linq

void Main()
{
	UseExpressionTreesToBuildDynamicQueries();
	PersonExpressionBuilderTest();
	ExpressionBuilderTest();
}
// Define other methods and classes here

//******************************************************************************************************************
// http://msdn.microsoft.com/en-us/library/bb882637.aspx
//******************************************************************************************************************
public void UseExpressionTreesToBuildDynamicQueries() {
       string[] companies = { "Consolidated Messenger", "Alpine Ski House", "Southridge Video", "City Power & Light",
                          "Coho Winery", "Wide World Importers", "Graphic Design Institute", "Adventure Works",
                          "Humongous Insurance", "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
                          "Blue Yonder Airlines", "Trey Research", "The Phone Company",
                          "Wingtip Toys", "Lucerne Publishing", "Fourth Coffee" };

       // The IQueryable data to query.
       IQueryable<String> queryableData = companies.AsQueryable<string>();

       // Compose the expression tree that represents the parameter to the predicate.
       ParameterExpression pe = Expression.Parameter(typeof(string), "company");

       // ***** Where(company => (company.ToLower() == "coho winery" || company.Length > 16)) *****
       // Create an expression tree that represents the expression 'company.ToLower() == "coho winery"'.
       Expression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
       Expression right = Expression.Constant("coho winery");
       Expression e1 = Expression.Equal(left, right);

       // Create an expression tree that represents the expression 'company.Length > 16'.
       left = Expression.Property(pe, typeof(string).GetProperty("Length"));
       right = Expression.Constant(16, typeof(int));
       Expression e2 = Expression.GreaterThan(left, right);

       // Combine the expression trees to create an expression tree that represents the 
       // expression '(company.ToLower() == "coho winery" || company.Length > 16)'.
       Expression predicateBody = Expression.OrElse(e1, e2);

       // Create an expression tree that represents the expression 
       // 'queryableData.Where(company => (company.ToLower() == "coho winery" || company.Length > 16))'
       MethodCallExpression whereCallExpression = Expression.Call(
           typeof(Queryable),
           "Where",
           new Type[] { queryableData.ElementType },
           queryableData.Expression,
           Expression.Lambda<Func<string, bool>>(predicateBody, new ParameterExpression[] { pe }));
       // ***** End Where ***** 

       // ***** OrderBy(company => company) ***** 
       // Create an expression tree that represents the expression 
       // 'whereCallExpression.OrderBy(company => company)'
       MethodCallExpression orderByCallExpression = Expression.Call(
           typeof(Queryable),
           "OrderBy",
           new Type[] { queryableData.ElementType, queryableData.ElementType },
           whereCallExpression,
           Expression.Lambda<Func<string, string>>(pe, new ParameterExpression[] { pe }));
       // ***** End OrderBy ***** 

       // Create an executable query from the expression tree.
       IQueryable<string> results = queryableData.Provider.CreateQuery<string>(orderByCallExpression);

       // Enumerate the results. 
       foreach (string company in results)
           Console.WriteLine(company);
}

//******************************************************************************************************************
// http://www.codeproject.com/Tips/582450/Build-Where-Clause-Dynamically-in-Linq  - PersonExpressionBuilder
//******************************************************************************************************************
public void PersonExpressionBuilderTest() {
	List<Filter> filters = new  List<Filter>
	{
		new  Filter  { PropertyName = "City" , Value = "Mitrovice"  },
		new  Filter  { PropertyName = "IsHomeOwner" , Value = false  }
//		,new  Filter  { PropertyName = "Age" , Value = 25  }
//		,new  Filter  { PropertyName = "Salary" , Value = 9000.0  }
	};
	
	var  deleg = PersonExpressionBuilder.Build(filters);
	var  filteredCollection = persons.Where(deleg).ToList();
	filteredCollection.Dump();
}

public class Person 
{
     public string Name        { get ; set ; }
     public string Surname     { get ; set ; }
     public int    Age         { get ; set ; }
     public string City        { get ; set ; }
     public double Salary      { get ; set ; }
     public bool   IsHomeOwner { get ; set ; }
}
List <Person> persons = new List <Person>
 {
     new  Person  { Name = "Flamur" , Surname = "Dauti" ,    Age = 39, 
                    City = "Prishtine" , IsHomeOwner = true ,  Salary = 12000.0 },

     new  Person  { Name = "Blerta" , Surname = "Frasheri" , Age = 25, 
                    City = "Mitrovice" , IsHomeOwner = false , Salary = 9000.0 },

     new  Person  { Name = "Berat" ,  Surname = "Dajti" ,    Age = 45, 
                    City = "Peje" ,      IsHomeOwner = true ,  Salary = 10000.0 },

     new  Person  { Name = "Laura" ,  Surname = "Morina" ,   Age = 23, 
                    City = "Mitrovice" , IsHomeOwner = true ,  Salary = 25000.0 },

     new  Person  { Name = "Olti" ,   Surname = "Kodra" ,    Age = 19, 
                    City = "Prishtine" , IsHomeOwner = false , Salary = 8000.0 },

     new  Person  { Name = "Xhenis" , Surname = "Berisha" ,  Age = 26, 
                    City = "Gjakove" ,   IsHomeOwner = false , Salary = 7000.0 },

     new  Person  { Name = "Fatos" ,  Surname = "Gashi" ,    Age = 32, 
                    City = "Peje" ,      IsHomeOwner = true ,  Salary = 6000.0 },

 };
 public  class  Filter 
 {
     public  string  PropertyName { get ; set ; }
     public  object  Value { get ; set ; }
 }
 public  class  PersonExpressionBuilder 
 {
     public static Func<Person, bool> Build(IList<Filter> filters)
     {
         ParameterExpression param = Expression.Parameter(typeof(Person), "t" );
         Expression  exp = null ;
 
         if  (filters.Count == 1)
             exp = GetExpression(param, filters[0]);
         else  if  (filters.Count == 2)
             exp = GetExpression(param, filters[0], filters[1]);
         else 
         {
             while  (filters.Count > 0)
             {
                 var  f1 = filters[0];
                 var  f2 = filters[1];
 
                 if  (exp == null )
                     exp = GetExpression(param, filters[0], filters[1]);
                 else 
                     exp = Expression.AndAlso(exp, GetExpression(param, filters[0], filters[1]));
 
                 filters.Remove(f1);
                 filters.Remove(f2);
 
                 if (filters.Count == 1)
                 {
                     exp = Expression.AndAlso(exp, GetExpression(param, filters[0]));
                     filters.RemoveAt(0);
                 }
             }
         }
 
         return Expression.Lambda<Func<Person, bool>>(exp, param).Compile();
     }
 
     private static Expression GetExpression(ParameterExpression param, Filter filter)
     {
         MemberExpression member = Expression.Property(param, filter.PropertyName);
         ConstantExpression constant = Expression.Constant(filter.Value);
         return Expression.Equal(member, constant);
     }
 
     private static BinaryExpression GetExpression(ParameterExpression param, Filter filter1, Filter filter2)
     {
         Expression bin1 = GetExpression(param, filter1);
         Expression bin2 = GetExpression(param, filter2);
 
         return  Expression.AndAlso(bin1, bin2);
     }
 }
 
//******************************************************************************************************************
// http://www.codeproject.com/Tips/582450/Build-Where-Clause-Dynamically-in-Linq - ExpressionBuilder
//******************************************************************************************************************
 public void ExpressionBuilderTest() {
	List<FilterWithOp> filterWithOp = new List<FilterWithOp>()
	{
		new FilterWithOp { PropertyName = "City" , 
			Operation = Op .Equals, Value = "Mitrovice"  },
		new FilterWithOp { PropertyName = "Name" , 
			Operation = Op .StartsWith, Value = "L"  },
		new FilterWithOp { PropertyName = "Salary" , 
			Operation = Op .GreaterThan, Value = 9000.0 }
	};
	
	var deleg = ExpressionBuilder.GetExpression<Person>(filterWithOp).Compile();
	var filteredCollection = persons.Where(deleg).ToList().Dump();
 }
 
 public static class ExpressionBuilder 
 {
     private static MethodInfo containsMethod = typeof(string).GetMethod("Contains" );
     private static MethodInfo startsWithMethod = 
     typeof(string).GetMethod("StartsWith", new Type [] {typeof(string)});
     private static MethodInfo endsWithMethod = 
     typeof(string).GetMethod("EndsWith", new Type [] { typeof(string)});
 
 
     public static Expression<Func<T, 
     bool >> GetExpression<T>(IList<FilterWithOp> filters)
     {            
         if  (filters.Count == 0)
             return null ;
 
         ParameterExpression param = Expression.Parameter(typeof (T), "t" );
         Expression exp = null ;
 
         if  (filters.Count == 1)
             exp = GetExpression<T>(param, filters[0]);
         else  if  (filters.Count == 2)
             exp = GetExpression<T>(param, filters[0], filters[1]);
         else 
         {
             while  (filters.Count > 0)
             {
                 var  f1 = filters[0];
                 var  f2 = filters[1];
 
                 if  (exp == null )
                     exp = GetExpression<T>(param, filters[0], filters[1]);
                 else 
                     exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
 
                 filters.Remove(f1);
                 filters.Remove(f2);
 
                 if  (filters.Count == 1)
                 {
                     exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                     filters.RemoveAt(0);
                 }
             }
         }
 
         return Expression.Lambda<Func<T, bool>>(exp, param);
     }

     private static Expression GetExpression<T>(ParameterExpression param, FilterWithOp filterWithOp)
     {
         MemberExpression member = Expression.Property(param, filterWithOp.PropertyName);
         ConstantExpression constant = Expression.Constant(filterWithOp.Value);
 
         switch (filterWithOp.Operation)
         {
             case  Op.Equals:
                 return Expression.Equal(member, constant);
 
             case  Op.GreaterThan:
                 return Expression.GreaterThan(member, constant);
 
             case Op.GreaterThanOrEqual:
                 return Expression.GreaterThanOrEqual(member, constant);
 
             case Op.LessThan:
                 return Expression.LessThan(member, constant);
 
             case Op.LessThanOrEqual:
                 return Expression.LessThanOrEqual(member, constant);
 
             case Op.Contains:
                 return Expression.Call(member, containsMethod, constant);
 
             case Op.StartsWith:
                 return Expression.Call(member, startsWithMethod, constant);
 
             case Op.EndsWith:
                 return Expression.Call(member, endsWithMethod, constant);
         }
 
         return null ;
     }
 
     private static BinaryExpression GetExpression<T>
     (ParameterExpression param, FilterWithOp filterWithOp1, FilterWithOp  filterWithOp2)
     {
         Expression bin1 = GetExpression<T>(param, filterWithOp1);
         Expression bin2 = GetExpression<T>(param, filterWithOp2);
 
         return  Expression.AndAlso(bin1, bin2);
     }
 }
 
 public class FilterWithOp 
 {
     public string PropertyName { get ; set ; }
     public Op Operation { get ; set ; }
     public object Value { get ; set ; }
 }
 
 public enum Op 
 {
     Equals,
     GreaterThan,
     LessThan,
     GreaterThanOrEqual,
     LessThanOrEqual,
     Contains,
     StartsWith,
     EndsWith
 }
 
//******************************************************************************************************************
// http://stackoverflow.com/questions/14901430/building-dynamic-where-clauses-in-linq-to-ef-queries
// Just a snippet. Not something that can be tested
//******************************************************************************************************************
// Here's an example using System.Linq.Expressions. Although the example here is specific to your Claim class you can make functions like this generic and then use them to build predicates dynamically for all your entities. I've been using recently to provide users with a flexible search for entities on any entity property (or groups of properties) function without having to hard code all the queries.
/*
 public Expression<Func<Claim, Boolean>> GetClaimWherePredicate(String name, String ssn)
{
  //the 'IN' parameter for expression ie claim=> condition
  ParameterExpression pe = Expression.Parameter(typeof(Claim), "Claim");

  //Expression for accessing last name property
  Expression eLastName = Expression.Property(pe, "ClaimantLastName");

  //Expression for accessing ssn property
  Expression eSsn = Expression.Property(pe, "ClaimantSSN");

  //the name constant to match 
  Expression cName = Expression.Constant(name);

  //the ssn constant to match 
  Expression cSsn = Expression.Constant(ssn);

  //the first expression: ClaimantLastName = ?
  Expression e1 = Expression.Equal(eLastName, cName);

  //the second expression:  ClaimantSSN = ?
  Expression e2 = Expression.Equal(eSsn, cSsn);

  //combine them with and
  Expression combined = Expression.And(e1, e2);

  //create and return the predicate
  return Expression.Lambda<Func<Claim, Boolean>>(combined, new ParameterExpression[] { pe });
}
*/