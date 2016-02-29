<Query Kind="Program" />

void Main()
{
	NotifyEvent = NotifyRon;
	NotifyEvent += NotifyClara;
	NotifyEvent("Trump Wins Nevada");
	
	"".Dump();
	"Remove Ron".Dump();
	NotifyEvent -= NotifyRon;
	
	NotifyEvent("Trump Wins Texas");
}

// Define other methods and classes here
public delegate void NotifyDelegate(string message);
public event NotifyDelegate NotifyEvent;

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