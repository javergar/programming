public interface List<a> {
	
	public static <a> List<a> append(List<a> xs,List<a> ys){
		if( xs instanceof Cons<?>) {
			Cons<a> aux = (Cons<a>) xs;
			return (new Cons<a>(aux.head(),append(aux.tail(),ys)));
		} 
		return ys;
	}
}
