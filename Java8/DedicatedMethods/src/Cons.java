public class Cons implements List
{
	Integer head;
	List tail;
	
	 public Cons(Integer val,List tail)
	    {
	      this.head = val;
	      this.tail = tail;	 
	    }

	@Override
	public int sum() {
		return this.head + this.tail.sum();
	}
}