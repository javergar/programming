public class Main {

	public static void main(String[] args) {
		List ls = new Cons<Integer>(3,new Cons<Integer>(3,new Nil()));
		SumWalkabout sw = new SumWalkabout();
		sw.visit(ls);
		System.out.println(sw.sum);
	}

}
