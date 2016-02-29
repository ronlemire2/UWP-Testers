<Query Kind="Program" />

void Main()
{
	NotifyDelegate d = NotifyRon;
	//NotifyDelegate d = new NotifyDelegate(NotifyRon);
	d += NotifyClara;
	
	d("Trump wins South Carolina");
	//d.Invoke("Hilary wins");
	
	"".Dump();
	"Remove Ron".Dump();
	d -= NotifyRon;
	
	d("Trump wins Nevada");
}

// Define other methods and classes here
public delegate void NotifyDelegate(string message);

void NotifyRon(string notification) {
	"".Dump();
	"NotifyRon: ".Dump();
	notification.Dump();
}


void NotifyClara(string notification) {
	"".Dump();
	"NotifyClara: ".Dump();
	notification.Dump();
}
