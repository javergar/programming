public class Nil<a> extends Cstr implements List<a> {	
	Nil() {
		super(0);
	}
	
	public String toString()
	{
		return "Nil";
	}
} 