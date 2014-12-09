public class SumWalkabout extends Walkabout {
	int sum = 0;
	public void visit(Cons<Integer> x) {
		sum = sum + x.head;
		this.visit(x.tail);
	}
}
