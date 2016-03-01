<Query Kind="Statements">
  <Connection>
    <ID>cf23c0b6-042d-4693-835e-7cf37f599653</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPathEncoded>&lt;MyDocuments&gt;\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll</CustomAssemblyPathEncoded>
    <CustomAssemblyPath>C:\Users\Ron\Documents\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll</CustomAssemblyPath>
    <CustomTypeName>DataLibrary.OrderIT.OrderITEntities</CustomTypeName>
    <AppConfigPath>C:\Users\Ron\Documents\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll.config</AppConfigPath>
  </Connection>
  <NuGetReference>LinqKit</NuGetReference>
  <Namespace>LinqKit</Namespace>
</Query>

// OrderIT is a sample database taken from the book 'Entity Framework 4 In Action' by Stefano Mostarda.
// PredicateBuilder was written by Joseph Albahari creator of LinqPad. (http://www.albahari.com/nutshell/predicatebuilder.aspx)

// OrderIT - Shoe Description containing "Green" or "Basket"
using (var context = new OrderITEntities()) {
	string[] keywords = new string[]{"Green", "Basket"};
	var descPred = PredicateBuilder.False<Product>();	// Or starts with .False
	foreach (string keyword in keywords)
	{
		string temp = keyword;
		descPred = descPred.Or (p => p.Description.Contains (temp));
	}
	var shoes = context.Products.OfType<Shoe>().AsExpandable().Where(descPred);
	foreach (var shoe in shoes) {
		Console.WriteLine("Name: {0}", shoe.Name);
		Console.WriteLine("Description: {0}", shoe.Description);
		Console.WriteLine("");
	}
}

// OrderIT - Shirt Price > 35 and < 100
using (var context = new OrderITEntities()) {
	var pricePred = PredicateBuilder.True<Product>();	// And starts with .True
	pricePred = pricePred.And (p => p.Price > 35M);
	pricePred = pricePred.And (p => p.Price < 100M);
	var shirts = context.Products.OfType<Shirt>().AsExpandable().Where(pricePred);
	foreach (var shirt in shirts) {
		Console.WriteLine("Name: {0}", shirt.Name);
		Console.WriteLine("Price: {0}", shirt.Price);
		Console.WriteLine("");
	}
}

// OrderIT - (Product Description containing "Green" or "Basket") or (Product Price > 35 and < 100)
using (var context = new OrderITEntities()) {
	var shoePred = PredicateBuilder.False<Product>();	// Or starts with .False
	shoePred = shoePred.Or (p => p.Description.Contains("Green"));
	shoePred = shoePred.Or (p => p.Description.Contains("Basket"));
	
	var pricePred = PredicateBuilder.True<Product>();	// And starts with .True
	pricePred = pricePred.And (p => p.Price > 35M);
	pricePred = pricePred.And (p => p.Price < 100M);
	
	var totalPred = pricePred.Or(shoePred.Expand()); 	
	// ***************************************************************************************************************************
	// Needed .Expand() here. Not sure why.
	// http://stackoverflow.com/questions/2947820/c-sharp-predicatebuilder-entities-the-parameter-f-was-not-bound-in-the-specif
	// ***************************************************************************************************************************
	
	var products = context.Products.AsExpandable().Where(totalPred);
	foreach (var product in products) {
		Console.WriteLine("Name: {0}", product.Name);
		Console.WriteLine("Price: {0}", product.Price);
		Console.WriteLine("");
	}
}

// OrderIT - myGreen or yourPink or under180
using (var context = new OrderITEntities()) {
	var myGreenPred = PredicateBuilder.True<Product>();	// And starts with .True
	myGreenPred = myGreenPred.And (p => p.Brand == "MyBrand");
	myGreenPred = myGreenPred.And (p => p.Description.Contains("Green"));
	
	var yourPinkPred = PredicateBuilder.True<Product>();	// And starts with .True
	yourPinkPred = yourPinkPred.And (p => p.Brand == "YourBrand");
	yourPinkPred = yourPinkPred.And (p => p.Description.Contains("Pink"));
	
	var under180Pred = PredicateBuilder.True<Product>();	// And starts with .True
	under180Pred = under180Pred.And (p => p.Price < 180);
	
	var totalPred = PredicateBuilder.False<Product>();	// Or starts with .False
	totalPred = totalPred.Or (myGreenPred.Expand());	// Expand() needed
	totalPred = totalPred.Or (yourPinkPred.Expand());	// Expand() needed
	totalPred = totalPred.Or (under180Pred.Expand());	// Expand() needed
	
	var products = context.Products.AsExpandable().Where(totalPred);
	foreach (var product in products) {
		Console.WriteLine("Name: {0}", product.Name);
		Console.WriteLine("Description: {0}", product.Description);
		Console.WriteLine("Brand: {0}", product.Brand);
		Console.WriteLine("Price: {0}", product.Price);
		Console.WriteLine("");
	}
}

// OrderIT - (myGreen or yourPink) and under100
using (var context = new OrderITEntities()) {
	var myGreenPred = PredicateBuilder.True<Product>();	// And starts with .True
	myGreenPred = myGreenPred.And (p => p.Brand == "MyBrand");
	myGreenPred = myGreenPred.And (p => p.Description.Contains("Green"));
	
	var yourPinkPred = PredicateBuilder.True<Product>();	// And starts with .True
	yourPinkPred = yourPinkPred.And (p => p.Brand == "YourBrand");
	yourPinkPred = yourPinkPred.And (p => p.Description.Contains("Pink"));
	
	var under100Pred = PredicateBuilder.True<Product>();	// And starts with .True
	under100Pred = under100Pred.And (p => p.Price < 100);
	
	var sub1Pred = PredicateBuilder.False<Product>();	// Or starts with .False
	sub1Pred = sub1Pred.Or (myGreenPred.Expand());	// Expand() needed
	sub1Pred = sub1Pred.Or (yourPinkPred.Expand());	// Expand() needed
	
	var totalPred = PredicateBuilder.True<Product>();	// And starts with .True
	totalPred = totalPred.And (sub1Pred.Expand());
	totalPred = totalPred.And (under100Pred.Expand());	// Expand() needed
	
	var products = context.Products.AsExpandable().Where(totalPred);
	foreach (var product in products) {
		Console.WriteLine("Name: {0}", product.Name);
		Console.WriteLine("Description: {0}", product.Description);
		Console.WriteLine("Brand: {0}", product.Brand);
		Console.WriteLine("Price: {0}", product.Price);
		Console.WriteLine("");
	}
}


// OrderIT - ((myGreen or yourPink) and under100) or (ReorderLevel < 10)
using (var context = new OrderITEntities()) {
	var myGreenPred = PredicateBuilder.True<Product>();	// And starts with .True
	myGreenPred = myGreenPred.And (p => p.Brand == "MyBrand");
	myGreenPred = myGreenPred.And (p => p.Description.Contains("Green"));
	
	var yourPinkPred = PredicateBuilder.True<Product>();	// And starts with .True
	yourPinkPred = yourPinkPred.And (p => p.Brand == "YourBrand");
	yourPinkPred = yourPinkPred.And (p => p.Description.Contains("Pink"));
	
	var priceUnder100Pred = PredicateBuilder.True<Product>();	// And starts with .True
	priceUnder100Pred = priceUnder100Pred.And (p => p.Price < 100);
	
	var reorderUnder10 = PredicateBuilder.True<Product>();	// And starts with .True
	reorderUnder10 = reorderUnder10.And (p => p.ReorderLevel < 10);
	
	// Group1  = myGreen or yourPink
	var leftInnerPred = PredicateBuilder.False<Product>();	// Or starts with .False
	leftInnerPred = leftInnerPred.Or (myGreenPred.Expand());	// Expand() needed
	leftInnerPred = leftInnerPred.Or (yourPinkPred.Expand());	// Expand() needed
	
	// Group2 = Group1 plus priceUnder100
	var leftPred = PredicateBuilder.True<Product>();	// And starts with .True
	leftPred = leftPred.And (leftInnerPred.Expand());
	leftPred = leftPred.And (priceUnder100Pred.Expand());	// Expand() needed
	
	// Total = Group2 plus reorderUnder10
	var totalPred = PredicateBuilder.False<Product>();	// Or starts with .False
	totalPred = totalPred.Or (leftPred.Expand());
	totalPred = totalPred.Or (reorderUnder10.Expand());	// Expand() needed
	
	var products = context.Products.AsExpandable().Where(totalPred);
	foreach (var product in products) {
		Console.WriteLine("Name: {0}", product.Name);
		Console.WriteLine("Description: {0}", product.Description);
		Console.WriteLine("Brand: {0}", product.Brand);
		Console.WriteLine("Price: {0}", product.Price);
		Console.WriteLine("");
	}
}


// OrderIT - ((myGreen or yourPink) and under100) or (ReorderLevel < 10)
using (var context = new OrderITEntities()) {
	var myGreenPred = PredicateBuilder.True<Product>();	// And starts with .True
	myGreenPred = myGreenPred.And (p => p.Brand == "MyBrand");
	myGreenPred = myGreenPred.And (p => p.Description.Contains("Green"));
	
	var yourPinkPred = PredicateBuilder.True<Product>();	// And starts with .True
	yourPinkPred = yourPinkPred.And (p => p.Brand == "YourBrand");
	yourPinkPred = yourPinkPred.And (p => p.Description.Contains("Pink"));
	
	var priceUnder100Pred = PredicateBuilder.True<Product>();	// And starts with .True
	priceUnder100Pred = priceUnder100Pred.And (p => p.Price < 100);
	
	var reorderUnder10 = PredicateBuilder.True<Product>();	// And starts with .True
	reorderUnder10 = reorderUnder10.And (p => p.ReorderLevel < 10);
	
	// Group1  = myGreen or yourPink
	var leftInnerPred = PredicateBuilder.False<Product>();	// Or starts with .False
	leftInnerPred = leftInnerPred.Or (myGreenPred.Expand());	// Expand() needed
	leftInnerPred = leftInnerPred.Or (yourPinkPred.Expand());	// Expand() needed
	
	// Group2 = Group1 plus priceUnder100
	var leftPred = PredicateBuilder.True<Product>();	// And starts with .True
	leftPred = leftPred.And (leftInnerPred.Expand());
	leftPred = leftPred.And (priceUnder100Pred.Expand());	// Expand() needed
	
	// Total = Group2 plus reorderUnder10
	var totalPred = PredicateBuilder.False<Product>();	// Or starts with .False
	totalPred = totalPred.Or (leftPred.Expand());
	totalPred = totalPred.Or (reorderUnder10.Expand());	// Expand() needed
	
	var products = context.Products.AsExpandable().Where(totalPred);
	foreach (var product in products) {
		Console.WriteLine("Name: {0}", product.Name);
		Console.WriteLine("Description: {0}", product.Description);
		Console.WriteLine("Brand: {0}", product.Brand);
		Console.WriteLine("Price: {0}", product.Price);
		Console.WriteLine("");
	}
}