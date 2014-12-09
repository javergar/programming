public class Cons<a> extends Cstr implements List<a> {	
	
	Cons(a val,List<a> xs) {
		super(2);
		nargs = 2;
		args[0] = val;
		args[1] = xs;
	}
	
	Cons() { super(2); }

	@SuppressWarnings("unchecked")
	public a head()
	{
		return ((a) args[0]);
	}   // !Cast

	public void setHead(a head)
	{
		args[0] = head;
	}

	public void setTail(List<a> tail)
	{
		args[1] = tail;
	}
	
	@SuppressWarnings("unchecked")
	public List<a> tail()
	{
		return ((List) args[1]);
	} // !Cast
	
	public String toString()
	{
		return "Cons";
	}

}
