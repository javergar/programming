public class Cons<a> extends List<a>
{
	a head;
	List<a> tail;
	
	 public Cons(a val,List<a> tail)
	    {
	      this.head = val;
	      this.tail = tail;
	    		 
	    }
	
   public boolean isCons() { return true; }
}