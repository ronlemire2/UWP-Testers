<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

// Dynamic Expressions can also be built with PredicateBuilder.
// See https://github.com/ronlemire2/UWP-Testers/blob/master/LinqTester/LinqLibrary/DynamicExpressions/PredicateBuilder.linq

public delegate int mult (int x, int y);

void Main()
{
	CreateLambdaExpression();
	ReturnLambdaExpression();
	CreateExpressionTreeFromLambdaUsingPreDefinedDelegate();
	CreateExpressionTreeFromLambdaUsingExpressionTDelegate();
	CreateExpressionTreeFromLambdaUsingExpressionTDelegate2();
	CreateExpressionTreeFromTheAPIAndExpressionTDelegate();
	CreateExpressionTreeFromTheAPIWithControlFlowExpressions();
	ExecuteExpressionTreeFromTheAPIThatRaisesANumberToAPower(2D, 16D);
	ModifyExpressionTreeThatRepresentConditionalANDToConditionalOR();
}
// Define other methods and classes here

// http://msdn.microsoft.com/en-us/library/bb397687.aspx (To create a lambda expression)
public void CreateLambdaExpression() {
	mult mult_xy = (x, y) => x * y;
	int product = mult_xy(5, 9);
	product.Dump();
}

public void ReturnLambdaExpression() {
	// my own code to return a lambda
	mult the_MultDelegateLamda = get_MultDelegateLambda();
	the_MultDelegateLamda(4,3).Dump();
}
public mult get_MultDelegateLambda () {
	return  (x, y) => x * y;
}

// http://msdn.microsoft.com/en-us/library/bb397687.aspx (To create an expression tree type:)
public void CreateExpressionTreeFromLambdaUsingPreDefinedDelegate() {
	// Create ExpressionTree from Lambda Expression
	Expression<mult> myET = (x, y) => x * y;
	
	// http://msdn.microsoft.com/en-us/library/bb397951.aspx (Compiling Expression Trees)
	// Compile ExpressionTree into a delegate.
	mult result = myET.Compile();
	
	// Run ExpressionTree
	result(2,2).Dump();
}

// http://msdn.microsoft.com/en-us/library/bb397687.aspx (To create an expression tree type:)
public void CreateExpressionTreeFromLambdaUsingExpressionTDelegate() {
	// Create ExpressionTree from Lambda Expression
	Expression<Func<int,int,int>> myET = (x, y) => x * y;
	
	// http://msdn.microsoft.com/en-us/library/bb397951.aspx (Compiling Expression Trees)
	// Compile ExpressionTree into a delegate.
	Func<int,int,int> result = myET.Compile();

	// Run ExpressionTree
	result(4,4).Dump();
	
	// Another example
	// http://msdn.microsoft.com/en-us/library/bb397951.aspx
	//	Expression<Func<int, bool>> lambda = num => num < 5;
	//	Func<int,bool> result = lambda.Compile();
	//	result(11).Dump();
}

// http://msdn.microsoft.com/en-us/library/bb397951.aspx
public void CreateExpressionTreeFromLambdaUsingExpressionTDelegate2() {
	Expression<Func<int, bool>> lambda = num => num < 5;
	Func<int,bool> result = lambda.Compile();
	result(1).Dump();
}

// http://msdn.microsoft.com/en-us/library/bb397951.aspx
public void CreateExpressionTreeFromTheAPIAndExpressionTDelegate() {
	// Manually build the expression tree for the lambda expression num => num < 5.
	// Needs 'using System.Linq.Expression'.
	// 1) Create expression tree to execute.
	ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
	ConstantExpression five = Expression.Constant(5, typeof(int));
	BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
	// 2) Create a lambda expression.
	Expression<Func<int, bool>> lambda1 =
		Expression.Lambda<Func<int, bool>>(
			numLessThanFive,
			new ParameterExpression[] { numParam });
	// 3) Compile the lambda expression.
	Func<int, bool> compiledLambda = lambda1.Compile();
	// 4) Execute the lambda expression.
	bool result = compiledLambda(10);
	// 5) Display result
	result.Dump();
}

// http://msdn.microsoft.com/en-us/library/bb397951.aspx
public void CreateExpressionTreeFromTheAPIWithControlFlowExpressions() {
	// Creating a parameter expression.
	ParameterExpression value = Expression.Parameter(typeof(int), "value");
	
	// Creating an expression to hold a local variable. 
	ParameterExpression result = Expression.Parameter(typeof(int), "result");
	
	// Creating a label to jump to from a loop.
	LabelTarget label = Expression.Label(typeof(int));
	
	// Creating a method body.
	BlockExpression block = Expression.Block(
		// Adding a local variable. 
		new[] { result },
		// Assigning a constant to a local variable: result = 1
		Expression.Assign(result, Expression.Constant(1)),
			// Adding a loop.
			Expression.Loop(
				// Adding a conditional block into the loop.
				Expression.IfThenElse(
					// Condition: value > 1
					Expression.GreaterThan(value, Expression.Constant(1)),
					// If true: result *= value --
					Expression.MultiplyAssign(result,
						Expression.PostDecrementAssign(value)),
					// If false, exit the loop and go to the label.
					Expression.Break(label, result)
				),
		// Label to jump to.
		label
		)
	);
	
	// Compile and execute an expression tree. 
	int factorial = Expression.Lambda<Func<int, int>>(block, value).Compile()(5);
	
	Console.WriteLine(factorial);
	// Prints 120.
}

// http://msdn.microsoft.com/en-us/library/bb882536.aspx
public void ExecuteExpressionTreeFromTheAPIThatRaisesANumberToAPower(double num, double power) {
	// The expression tree to execute.
	BinaryExpression be = Expression.Power(Expression.Constant(num), Expression.Constant(power));
	
	// Create a lambda expression.
	Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);
	
	// Compile the lambda expression.
	Func<double> compiledExpression = le.Compile();
	
	// Execute the lambda expression. 
	double result = compiledExpression();
	
	// Display the result.
	Console.WriteLine(result);
	
	// This code produces the following output: 
	// 8
}
// C# Factorial code
//void Main()
//{
//	Factorial(5).Dump();	
//
//}
//public int Factorial(int value) {
//	int result = 1;
//	while (true) {
//		if (value > 1) {
//			result = result * value--;
//		}
//		else {
//			break;
//		}
//	}
//	return result;
//}

//http://msdn.microsoft.com/en-us/library/bb546136.aspx
public void ModifyExpressionTreeThatRepresentConditionalANDToConditionalOR() {
	Expression<Func<string, bool>> expr = name => name.Length > 10 && name.StartsWith("G");
	Console.WriteLine(expr);
	
	AndAlsoModifier treeModifier = new AndAlsoModifier();
	Expression modifiedExpr = treeModifier.Modify((Expression) expr);
	
	Console.WriteLine(modifiedExpr);
	
	/*  This code produces the following output:
	
	name => ((name.Length > 10) && name.StartsWith("G"))
	name => ((name.Length > 10) || name.StartsWith("G"))
	*/
}

public class AndAlsoModifier : ExpressionVisitor
{
    public Expression Modify(Expression expression)
    {
        return Visit(expression);
    }

    protected override Expression VisitBinary(BinaryExpression b)
    {
        if (b.NodeType == ExpressionType.AndAlso)
        {
            Expression left = this.Visit(b.Left);
            Expression right = this.Visit(b.Right);

            // Make this binary expression an OrElse operation instead of an AndAlso operation. 
            return Expression.MakeBinary(ExpressionType.OrElse, left, right, b.IsLiftedToNull, b.Method);
        }

        return base.VisitBinary(b);
    }
}

// http://msdn.microsoft.com/en-us/library/ee725345.aspx
public void DebuggingExpressionTrees() {
	int num = 10;
	ConstantExpression expr = Expression.Constant(num);
}
