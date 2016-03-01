<Query Kind="Statements">
  <Connection>
    <ID>fa2bb04d-ad5f-4147-9b14-62a965a84c88</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPathEncoded>&lt;MyDocuments&gt;\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll</CustomAssemblyPathEncoded>
    <CustomAssemblyPath>C:\Users\Ron\Documents\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll</CustomAssemblyPath>
    <CustomTypeName>DataLibrary.OrderIT.OrderITEntities</CustomTypeName>
    <AppConfigPath>C:\Users\Ron\Documents\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll.config</AppConfigPath>
  </Connection>
</Query>

// ========================================================================================
// OrderIT is a sample database taken from the book 'Entity Framework 4 In Action'.
// Most of these examples come from Chapter 4 - Querying with LINQ to Entities.
// Comments that include a number e.g. 4.1.3 refer to chapter sections.
// e.g. Chapter 4 Section 1.3
// Chapter 2 explains how the OrderIT database was designed to handle inheritance classes.
// ========================================================================================

// =====================================================
// Restriction 
// =====================================================
// Orders in 1 ShippingCity
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.ShippingCity == "New York")
		.Dump();
}

// Orders in 2 ShippingCities
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.ShippingCity == "New York" || o.ShippingCity == "Seattle")
		.Dump();
}

// Filtering with a single association
using (var ctx = new OrderITEntities()) {
//	ctx.Orders
//		.Include("Customer")
//		.Dump();
	ctx.Orders
		.Include("Customer")
		.Where (o => o.Customer.BillingCity == "paris")
		.Dump();
}

// Filtering with collection associations using method chaining (i.e. all orders with only 1 OrderDetail line)
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Where (o => o.OrderDetails
					.Select (od => od.Product.ProductId)
					.Distinct()
					.Count () == 1)
		.Dump();
}

// 4.1.3 - Applying filters dynamically
using (var ctx = new OrderITEntities()) {
	var date = DateTime.Today;
	string city = "Washington";
	var orders = ctx.Orders.AsQueryable();
	if (date != DateTime.MinValue) {
		orders = orders.Where (o => o.OrderDate < date);
	}
	if (!String.IsNullOrEmpty(city)) {
		orders = orders.Where (o => o.ShippingCity == city);
	}
	orders.Dump();
}

// 4.6.0 - Querying with inheritance


// =====================================================
// 4.2.0 - Projection
// =====================================================
// Simple projection
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {OrderId=o.OrderId, OrderDate=o.OrderDate, ShippingAddress=o.ShippingAddress})
		.Dump();
}

// Group address info into a single property
// Note: Not working. Error Message: Linq to Entities does not recognize String.Format. Can't believe this. Must be doing something wrong.
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {OrderId=o.OrderId, OrderDate=o.OrderDate, AddressInfo=string.Format("{0}-{1}",o.ShippingAddress, o.ShippingCity)})
		.Dump();
}

// Nested anonymous type to group properties
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.Select (o => new {OrderId=o.OrderId, OrderDate=o.OrderDate, AddressInfo = new {ShippingAddress=o.ShippingAddress, ShippingCity=o.ShippingCity}})
		.Dump();
}

// 4.2.1 - Projection that includes a single association
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {OrderId=o.OrderId, OrderDate=o.OrderDate, ShippingAddress=o.ShippingAddress, Customer=o.Customer})
		.Dump();
}

// Flatten single association into projection
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {OrderId=o.OrderId, OrderDate=o.OrderDate, ShippingAddress=o.ShippingAddress, CustomerName=o.Customer.Name})
		.Dump();
}

// Projections into a DTO (data transfer object)
// Note: Requires a program to create the OrderDTO class. 
// See C:\Dev.2014\Apps\OrderIT\OrderITConsoleClient\Program.cs
////class Program
////{
////   static void Main(string[] args) {
////       
////       using (var context = new OrderITEntities()) {
////           var result = context.Orders.Select(o => new OrderDTO { Id = o.OrderId, OrderDate = o.OrderDate, ShippingAddress = o.ShippingAddress, CustomerName = o.Customer.Name });
////           foreach (OrderDTO dto in result) {
////               Console.WriteLine(string.Format("{0}-{1}-{2}-{3}", dto.Id.ToString(), dto.OrderDate.ToShortDateString(), dto.ShippingAddress, dto.CustomerName));
////           }
////       }
////
////       Console.Read();
////
////       //var order = new Order() { OrderDate = DateTime.Now, CustomerId = 1, ShippingAddress = "18 Dunham Ave", ShippingCity = "Somerset", ShippingCountry = "USA", ShippingZipCode = "00876" };
////       //var newOrderDetail = new OrderDetail() { OrderId = 50, ProductId = 1, Quantity = 2, UnitPrice = 100.00M, Discount = 10.00M };
////   }
////}
////public class OrderDTO
////{
////   public int Id { get; set; }
////   public DateTime OrderDate { get; set; }
////   public string ShippingAddress { get; set; }
////   public string CustomerName { get; set; }
////}

// Projection that includes a Collection Association (i.e. show OrderDetails with Order)
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {OrderId=o.OrderId, 
			OrderDate=o.OrderDate, 
			ShippingAddress=o.ShippingAddress, 
			OrderDetails=o.OrderDetails})
		.Dump();
}

// Projection that includes a Collection Association but with only a subset of collection properties (i.e. show some of OrderDetail properties)
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {OrderId=o.OrderId, 
			OrderDate=o.OrderDate, 
			ShippingAddress=o.ShippingAddress,	
			OrderDetails=o.OrderDetails
				.Select (od => new {OrderDetailId=od.OrderDetailId,ProductId=od.Product.ProductId,od.Quantity})})
		.Dump();
}

// Retrieving projected orders with an OrderDetails total.
// Note: I tried to do Sum without the .Where (o => o.OrderId < 20) and got InvalidOperationException. The cast to value type
// type 'Decimal' failed because the materialized value is null. Not sure how to add check for nulls.
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.OrderId < 20)
		.Select (o => new {OrderId=o.OrderId, 
			OrderDate=o.OrderDate, 
			ShippingAddress=o.ShippingAddress, 
			Total=o.OrderDetails.Sum (od => od.Quantity * (od.UnitPrice - od.Discount))})
		.Dump();
}

// Project Sum of OrderDetails after Filtering them by UnitPrice
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.OrderId == 11)
		.Select (o => new {
			OrderId=o.OrderId, 
			OrderDate=o.OrderDate, 
			ShippingAddress=o.ShippingAddress, 
			Details=o.OrderDetails
				.Where (od => od.UnitPrice > 5.00M)
				.Sum (od => od.Quantity * (od.UnitPrice - od.Discount) )})
		.Dump();
}

// Conditionally include OrderDetails (only OrderDetails with UnitPrice > 5.00M)
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {
			OrderId=o.OrderId, 
			OrderDate=o.OrderDate, 
			ShippingAddress=o.ShippingAddress, 
			Details=o.OrderDetails.Where (od => od.UnitPrice > 5.00M)})
		.Dump();
}
// =====================================================
// Partitioning
// =====================================================
// 4.1.2
// Take
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.OrderBy (o => o.OrderDate)
		.Take(3)
		.Dump();
}

// Skip then Take
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.OrderBy (o => o.OrderDate)
		.Skip(3)
		.Take(3)
		.Dump();
}

// =====================================================
// Grouping
// =====================================================
// 4.3.0 - Iterating over the results of a grouping query
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.GroupBy (o => o.ShippingCity);
	//result.Dump();
	
	// GroupBy returns a sequence of IGrouping<K,T> elements.
	// IGrouping<K,T> is a sequence of type T with a key of type K.
	// Said another way, the return value of the GroupBy method is a sequence
	// 	of IGrouping objects, each containing a key and a sequence of the 
	// 	elements from the input sequence having that same key.
	foreach (var iGrouping in result) { // Enumerate the groups
		Console.WriteLine("===>" + iGrouping.Key); // iGrouping.Key is the key of type K
		foreach (var item in iGrouping) {  // Enumerate the items in the IGrouping (i.e. Orders)
			Console.WriteLine(item.OrderId);
			//Console.WriteLine(item); // Displays all Order properties
		}
	}
}

// Changing the names of grouped data
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.GroupBy (o => o.ShippingCity)
		.Select (g => new {CityName=g.Key, Items = g});
	//result.Dump();
	foreach (var key in result) {
		Console.WriteLine("===>" + key.CityName);
		foreach (var item in key.Items) {
			Console.WriteLine(item.OrderId);
		}
	}
}

// Using multiple properties for grouping Key
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.GroupBy (o => new {o.ShippingCity, o.ShippingZipCode});
	//result.Dump();
	foreach (var key in result) {
		Console.WriteLine("===>" + key.Key.ShippingCity + "-" + key.Key.ShippingZipCode);
		foreach (var item in key) {
			Console.WriteLine(item.OrderId);
		}
	}
}

// Projecting the grouped data
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.GroupBy (o => o.ShippingCity)
		.Select (g => new {ShippingCity=g.Key, Items=g.Select (x => new {OrderId=x.OrderId, OrderDate=x.OrderDate})});
	//result.Dump();
	foreach (var key in result) {
		Console.WriteLine("===>" + key.ShippingCity);
		foreach (var item in key.Items) {
			Console.WriteLine(string.Format("OrderId:{0} - OrderDate:{1}",item.OrderId, item.OrderDate));
		}
	}
}

// 4.3.1 - Grouping orders only for cities that have more than 2 orders
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.GroupBy (o => o.ShippingCity)
		.Where (g => g.Count() > 2); // 'where' applies only to the groups. To filter Orders, place 'where' before .GroupBy.
	//result.Dump();
	foreach (var key in result) {
		Console.WriteLine("===>" + key.Key);
		foreach (var item in key) {
			//Console.WriteLine(item.OrderId);
			Console.WriteLine(item); // Displays all Order properties
		}
	}
}

// Filter Orders before grouping
using (var ctx = new OrderITEntities()) {
	var result = ctx.Orders
		.Where (o => o.ShippingCity.Length > 5) // 'where' applies to orders.
		.GroupBy (o => o.ShippingCity); 
	//result.Dump();
	foreach (var key in result) {
		Console.WriteLine("===>" + key.Key);
		foreach (var item in key) {
			//Console.WriteLine(item.OrderId);
			Console.WriteLine(item); // Displays all Order properties
		}
	}
}

// =====================================================
// Ordering
// =====================================================
// 4.4.0 - Sorting with single and multiple properties, ascending and descending
using (var ctx = new OrderITEntities()) {
	ctx.Orders.OrderBy (o => o.ShippingCity).Dump();
}
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.OrderByDescending (o => o.ShippingCity)
		.Dump();
}
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.OrderBy(o => o.ShippingCity)
		.ThenByDescending(o => o.ShippingZipCode)
		.Dump();
}

// Sorting by an aggregated value of association (i.e. OrderDetails)
// Note: Had to add .Where to get around materialized value is null as above in Projections section
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.OrderId < 20)
		.OrderBy (o => o.OrderDetails.Sum (od => od.Quantity * (od.UnitPrice - od.Discount)))
		.Select (o => new {o.OrderId, o.OrderDate, o.ShippingAddress,Total = o.OrderDetails.Sum (od => od.Quantity * (od.UnitPrice - od.Discount))})
		.Dump();
}

// Retrieving projected orders and details ordered by quantity
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Select (o => new {o.OrderId, o.ShippingCity, Details = o.OrderDetails.OrderByDescending(od => od.Quantity)})
		.Dump();
}

// Orderby single association property
// Note: there's only 2 customers that have orders. CustomerId 1 and CustomerId 2. 
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("Customer")
		.OrderBy (o => o.Customer.ShippingCity)
		.Select (o => new {o.OrderId,o.CustomerId,o.Customer.ShippingCity})
		.Dump();
}

// =====================================================
// Join
// =====================================================
// 4.5.0 - 
// Book says they have never had to resort to using 'join'.
// Also I cannot figure out the .join method syntax.

// =====================================================
// Set
// =====================================================

// =====================================================
// Conversion
// =====================================================
// Filtering products by type using 'OfType<T>'
using (var ctx = new OrderITEntities()) {
	ctx.Products
		.OfType<Shoe>()
		.Dump();
}

// Filtering products by type using 'is'
using (var ctx = new OrderITEntities()) {
	ctx.Products
		.Where (p => p is Shoe)
		.Dump();
}

// Note: 'is' has an advantage if looking for more than one type
// 	because .OfType<T> would require 2 queries plus a merge.
// Filtering multiple subtypes using 'is'
using (var ctx = new OrderITEntities()) {
	ctx.Products
		.Where (p => p is Shoe || p is Shirt)
		.Dump();
}

// Filter on properties in the inherited type.
// Note: .OfType has advantage because it does a cast so 
// 	where knows the output type
using (var ctx = new OrderITEntities()) {
	ctx.Products
		.OfType<Shoe>()
		.Where (s => s.Sport == "Basket")
		.Dump();
}

// Filtering products by inherited type and return them as base types.
// Option 1: (not working)
using (var ctx = new OrderITEntities()) {
	ctx.Products
		.Where (p as Shoe).Sport == "Basket"
		.Dump();
}
// Option 2: 
// Book says this option performs better and is prefered to casting the Where as in Option 1.
using (var ctx = new OrderITEntities()) {
	ctx.Products
		.OfType<Shoe>()
		.Where (s => s.Sport == "Basket")
		.Cast<Product>()
		.Dump();
}

// =====================================================
// Element
// Note: EF4.1 has a .Find method to select 1 entity by its Key value(s).
// =====================================================
// 4.1.3
// Retrieve an order given its ID using First
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.OrderId == 11)
		.FirstOrDefault ()
		.Dump();
}

// Retrieving an entity by its key using context methods 
// Note: this code uses the ObjectContext. Not sure how to get ObjectContext from OrderITEntities.
//using (var ctx = new OrderITEntities()) {
//	var key = new EntityKey("OrderITEntities.Orders", "OrderId", 1);
//	var entity = ((DbContext)ctx as IObjectContextAdapter).GetObjectByKey(key);
//}

// =====================================================
// Quantifiers
// =====================================================
// Filtering with collection associations using Any
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Where (o => o.OrderDetails
					.Any (od => od.Product.Brand == "MyBrand"))
		.Dump();
}

// Filtering with collection associations using All
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Where (o => o.OrderDetails
					.All (od => od.Discount == 0))
		.Dump();
}


// Orders in multiple arbitrary ShippingCities
var cities = new[] {"New York", "Seattle", "Miami"};
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where(o => cities
		.Contains(o.ShippingCity))
		.Dump();
}

// =====================================================
// Aggregate
// =====================================================
// Filtering with collection associations using Sum 'aggregation' mehtod
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Where (o => o.OrderDetails
					.Sum (od => od.Discount * od.Quantity ) > 5)
		.Dump();
}

// =====================================================
// Canonical Functions (4.7.1)
// =====================================================
// LinqToEntities does not recognize DateTime.AddDays so this does not work
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.OrderDate.AddDays(5) < o.ActualShippingDate)
		.Dump();
}

// LinqToEntities does recognize Canonical functions via the EntityFunctions class
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => EntityFunctions.DiffDays(o.OrderDate, o.ActualShippingDate) > 5)
		.Dump();
}
// Note: DateTime and Math methods are not supported by LinqToEntities but
//	EntityFunctions methods: Pow, Round, Ceiling, Floor are supported.

// =====================================================
// Database Functions (4.7.2)
// =====================================================
// EntityFramework contains some SQL Server-specific functions exposed by the 
//	SqlFunctions class: Checksum,CharIndex,Cos,GetDate,Rand plus others.
// 	SqlFunctions has its own .DateDiff function which can be used instead
//	of EntityFunctions.DateDiff function.

// Note: not working. Does LinqPad support SqlFunctions?
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => SqlFunctions.DiffDays("d", o.OrderDate, o.ActualShippingDate) > 5)
		.Dump();
}

// =====================================================
// Manually write t-Sql instead of having EF generate it (4.8)
// =====================================================
// Note: Book uses ctx.ExecuteStoreQuery which is a method on ObjectContext.
// I'm not sure how to get ObjectContext in LinqPad.
// http://blogs.msdn.com/b/adonet/archive/2011/02/04/using-dbcontext-in-ef-feature-ctp5-part-10-raw-sql-queries.aspx 

// For Queries use .SqlQuery method
using (var ctx = new OrderITEntities()) {
	ctx.Database.SqlQuery<Order>("Select * from [Order]").Dump();
}

// Note: EDM is not used to map columns. Can't map complex types. Instance properties must be same name as DB table column
// 	otherwise use the 'AS' SQl Clause.
// Note: Can use a DTO type in the .SqlQuery<T>. (see Listing 4.24)
// Note: Parameters can be be passed either as 'simple values' or 'using SqlParameters'. (see Listing 4.25, 4.26)

// For non Queries use .ExecuteSqlCommand method
using (var ctx = new OrderITEntities()) {
	ctx.Database.ExecuteSqlCommand("update [Order] set ShippingCity = 'San Diego' where OrderId = 11"); 
}

// =====================================================
// Eager Loading 4.9 
// =====================================================
// 'Eager loading' refers to loading entities and their associated data in a single query.
// 'Eager loading' is coded with the .Include method.
//  Books says 'eager loading' is generally better than 'lazy loading' but it depends.
// 	Correct fetching strategy is a matter of testing and case-by-case analysis.

// 'Eager loading' - Loads Order, OrderDetails
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Dump();
}

// 'Eager loading' - Loads Order, OrderDetails, OrderDetails->Product
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails.Product")
		.Dump();
}

// 'Eager loading' - Loads Order, OrderDetails, OrderDetails->Product, Order->Customer
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails.Product")
		.Include("Customer")
		.Dump();
}

// =====================================================
// Lazy Loading 4.9 
// =====================================================
// 'Lazy loading' refers to automatic loading of associated entities when they're used in the code.
// Lazy loading is implemented by:
//		1) class which is public and not sealed
//		2) 'virtual' navigation properties
// 		3) EF will automatically create Dynamic Proxy if steps 1) and 2) are implemented. 
//			Dynamic Proxy has code to go to database and get related entities.
//			Dynamic Proxies were instituted to add POCO classes.
//			
// Related entities are obtained by accessing the navigation properties.
// Note: Lazy loading is enabled by default. (at least in EF4 not sure about EF5 or EF6)
// Note: Lazy loading can be disabled by setting ctx.Configuration.LazyLoadingEnabled to false.
// Note: Lazy loading is accomplished via dynamic proxies. 
// Note: (so if using POCO is lazy loading enabled?) 
// Note: book says if proxy generation turned off then no proxy...no lazy loading.
// 		1) context.Configuration.ProxyCreationEnabled = false;
// 		2) context.Configuration.LazyLoadingEnabled = false;
// Note: Manual deferred loading is a way to dynamically retrieve a property without using lazy loading.
//	There is a LoadProperty method on the object context. Not sure how to get ObjectContext.

// Customer and details retrieved on demand (i.e. lazy loaded) when associated property is executed.
using (var ctx = new OrderITEntities()) {
	foreach (var order in ctx.Orders) {
		Console.WriteLine(order.CustomerId + " " + order.Customer.Name);
		foreach (var detail in order.OrderDetails) {
			Console.WriteLine(detail.OrderDetailId + " " + detail.Quantity);
		}
	}
}




// =====================================================
// Queries
// =====================================================
// No restrictions
using (var ctx = new OrderITEntities()) {
	ctx.Orders.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Dump();
}

// Restriction Queries (.Where)
using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.ShippingZipCode=="98765")
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.ShippingZipCode.StartsWith("9") || o.ShippingZipCode.StartsWith("8"))
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Where (o => o.OrderId == 11)
		.FirstOrDefault()
		.Dump();
}

// Contains
using (var ctx = new OrderITEntities()) {
	var zips = new string[] {"98765", "87329", "00100"};	// multiple arbitrary variable
	ctx.Orders
		.Where (o => zips.Contains(o.ShippingZipCode))
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Where (o => o.OrderId == 11).FirstOrDefault()
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Orders
		.Include("OrderDetails")
		.Include("Customer")
		.Where (o => o.OrderId == 11).FirstOrDefault()
		.Dump();
}

// Inheritance Queries
using (var ctx = new OrderITEntities()) {
	ctx.Companies
		.OfType<Customer>()
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Companies
		.OfType<Supplier>()
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Companies
		.Where (c => c is Supplier)
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx.Products
		.OfType<Shirt>()
		.Dump();
}

using (var ctx = new OrderITEntities()) {
	ctx
		.Products
		.OfType<Shoe>()
		.Dump();
}

// =====================================================
// Insert
// =====================================================
// Insert Order
using (var ctx = new OrderITEntities()) {
	var newOrder = new Order() { OrderDate = DateTime.Now, CustomerId = 1, ShippingAddress = "18 Dunham Ave", ShippingCity = "Somerset", ShippingCountry = "USA", ShippingZipCode = "00876" };
	ctx.Orders.Add(newOrder);
	ctx.SaveChanges();
}

// Add OrderDetails to ExistingOrder
using (var ctx = new OrderITEntities()) {
	var existingOrder = ctx.Orders.Where (o => o.OrderId==50).FirstOrDefault();
	var newOrderDetail1 = new OrderDetail() { OrderId = 50, ProductId = 1, Quantity = 1, UnitPrice = 100.00M, Discount = 10.00M };
	var newOrderDetail2 = new OrderDetail() { OrderId = 50, ProductId = 2, Quantity = 2, UnitPrice = 50.00M, Discount = 5.00M };
	existingOrder.OrderDetails.Add(newOrderDetail1);
	existingOrder.OrderDetails.Add(newOrderDetail2);
	ctx.SaveChanges();
}

// =====================================================
// Update
// =====================================================
// Update Order
using (var ctx = new OrderITEntities()) {
	var existingOrder = ctx.Orders.Where (o => o.OrderId==50).FirstOrDefault();
	existingOrder.ShippingAddress = "18 Dober Ave";
	ctx.SaveChanges();
}

// =====================================================
// Delete
// =====================================================
// Delete Order with OrderDetails (DB has Cascade Rule On Delete Order)
using (var ctx = new OrderITEntities()) {
	var existingOrder = ctx.Orders.Where (o => o.OrderId==50).FirstOrDefault();
	ctx.Orders.Remove(existingOrder);
	ctx.SaveChanges();
}