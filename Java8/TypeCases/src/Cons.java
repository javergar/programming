public class Cons<a> implements List<a> {
	a head;
	List<a> tail;
	
	Cons(a val,List<a> tail)
	{
		this.head = val;
		this.tail = tail;
	}
	
	public String toString()
	{
	   return " Cons "+(this.head.toString())+(this.tail.toString());
	}
}
