public class Main {
	
	public static Integer sum(List<Integer> ls) {
		int sum = 0;
		if( ls instanceof Cons<?>) {
		Cons<Integer> xs = (Cons<Integer>) ls;
		   sum = sum + xs.head + sum(xs.tail);
		  }
		
		return sum;
	}	
	
	public static List<Integer> plusOne (List<Integer> ls) {
		
		if( ls instanceof Cons<?>) {
		Cons<Integer> xs = (Cons<Integer>) ls;
		return new Cons<Integer>(xs.head+1,plusOne(xs.tail));
		}
		return new Nil<Integer>();
	}	
	
	public static void main(String[] args) {
		List<Integer> ls = new Cons<Integer>(3,new Cons<Integer>(2,new Nil<Integer>()));
		System.out.println(sum(ls));
		System.out.println((plusOne(ls)).toString());
	}
}
