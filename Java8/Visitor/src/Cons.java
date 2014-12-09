public class Cons<a> implements List {
	a head;
	List tail;
	
	Cons(a val,List tail)
	{
		this.head = val;
		this.tail = tail;
	}
}

