<Query Kind="Statements">
  <Connection>
    <ID>ebe3d5e1-74de-4fca-b62c-ddf31598d49d</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPathEncoded>&lt;MyDocuments&gt;\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll</CustomAssemblyPathEncoded>
    <CustomAssemblyPath>C:\Users\Ron\Documents\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll</CustomAssemblyPath>
    <CustomTypeName>DataLibrary.AdventureWorks2012.AdventureWorks2012Entities</CustomTypeName>
    <AppConfigPath>C:\Users\Ron\Documents\GitHubVisualStudio\UWP-Testers\LinqTester\DataLibrary\bin\Debug\DataLibrary.dll.config</AppConfigPath>
  </Connection>
  <NuGetReference>LinqKit</NuGetReference>
  <Namespace>LinqKit</Namespace>
</Query>

// FirstName, LastName and StateProvinceCode pred with Includes
using (var ctx = new AdventureWorks2012Entities()) {
	var personFilters = ctx.PersonFilters.FirstOrDefault();
	string stateProvinceCode = personFilters.StateProvinceCode;
	string firstName = personFilters.FirstName;
	string lastName = personFilters.LastName;
	
	var personPred = PredicateBuilder.True<Person>();	// And starts with .True
	
	if (!string.IsNullOrEmpty(stateProvinceCode)) {
		personPred = personPred.And (p => p.BusinessEntity.BusinessEntityAddresses.Any(bea => bea.Address.StateProvince.StateProvinceCode == stateProvinceCode));
	}
	if (!string.IsNullOrEmpty(firstName)) {
			personPred = personPred.And (p => p.FirstName == firstName);
	}
	if (!string.IsNullOrEmpty(lastName)) {
		personPred = personPred.And (p => p.LastName == lastName);
	}
	
	var persons = ctx.People
      .Include(p => p.Employee)
      .Include(p => p.PersonPhones
          .Select(pp => pp.PhoneNumberType))
      .Include(p => p.EmailAddresses)
      .Include(p => p.Password)
      .Include(p => p.BusinessEntity.BusinessEntityAddresses
          .Select(bea => bea.Address)
          .Select(a => a.StateProvince)
          .Select(sp => sp.CountryRegion))
      .Include(p => p.BusinessEntity.BusinessEntityAddresses
          .Select(bea => bea.AddressType))
	  .AsExpandable()
      .Where(personPred)
      .OrderBy(p => p.LastName);
	  
	foreach (var person in persons) {
		Console.WriteLine(person.FirstName);
		Console.WriteLine(person.LastName);
		var beas = person.BusinessEntity.BusinessEntityAddresses;
		foreach (BusinessEntityAddress addr in beas) {
			Console.WriteLine(addr.Address.StateProvince.Name);
		}

		Console.WriteLine("");
	}
}

// LastName pred with Includes
using (var ctx = new AdventureWorks2012Entities()) {
	var namePred = PredicateBuilder.True<Person>();	// And starts with .True
	namePred = namePred.And (p => p.LastName == "Adams");
	var persons = ctx.People
      .Include(p => p.Employee)
      .Include(p => p.PersonPhones
          .Select(pp => pp.PhoneNumberType))
      .Include(p => p.EmailAddresses)
      .Include(p => p.Password)
      .Include(p => p.BusinessEntity.BusinessEntityAddresses
          .Select(bea => bea.Address)
          .Select(a => a.StateProvince)
          .Select(sp => sp.CountryRegion))
      .Include(p => p.BusinessEntity.BusinessEntityAddresses
          .Select(bea => bea.AddressType))
	  .AsExpandable()
      .Where(namePred)
      .OrderBy(p => p.LastName);
	  
	foreach (var person in persons) {
		var beas = person.BusinessEntity.BusinessEntityAddresses;
		foreach (BusinessEntityAddress addr in beas) {
			Console.WriteLine(addr.Address.StateProvince.Name);
		}

		Console.WriteLine("");
	}
}

// FirstName and LastName pred
using (var context = new AdventureWorks2012Entities()) {
	var namePred = PredicateBuilder..True<Person>();	// And starts with .True
	namePred = namePred.And (p => p.FirstName == "Terri");
	namePred = namePred.And (p => p.LastName.Contains("Duffy"));
	
	var persons = context.People.AsExpandable().Where(namePred);
	foreach (var person in persons) {
		Console.WriteLine("FirstName: {0}", person.FirstName);
		Console.WriteLine("LastName: {0}", person.LastName);
		Console.WriteLine("");
	}
}

// FirstName pred
using (var context = new AdventureWorks2012Entities()) {
	var namePred = PredicateBuilder.True<Person>();	// And starts with .True
	namePred = namePred.And (p => p.LastName == "Adams");
	
	var persons = context.People.AsExpandable().Where(namePred);
	foreach (var person in persons) {
		Console.WriteLine("FirstName: {0}", person.FirstName);
		Console.WriteLine("LastName: {0}", person.LastName);
		Console.WriteLine("");
	}
}