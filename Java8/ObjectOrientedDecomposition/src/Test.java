public class Test {
	
	public static String listToString(String acc,List<?> xs)
	{ 
      if (xs.isCons())
      {
    	  acc += " "+(((Cons<?>) xs).head).toString(); //Type Cast!
    	  List<?> tail = ((Cons<?>) xs).tail;  // Type Cast!
    	  acc = Test.listToString(acc,tail);
    	  return acc;
      }       
      
      return acc;
	}
	
	public static Integer sum(Integer acc,List<Integer> xs)
	{ 
      if (xs.isCons())
      {
    	  acc = acc + ((Cons<Integer>) xs).head;  // Type cast!
    	  List<Integer> tail = ((Cons<Integer>) xs).tail;  // Type cast!
    	  acc = Test.sum(acc,tail);
    	  return acc;
      }       
      
      return acc;
	}
	

	public static void main(String[] args) {
	   List<String>  list1 = new Cons<String>("A",(new Cons<String>("B",new Nil<String>())));
	   List<Integer> list2 = new Cons<Integer>(1,(new Cons<Integer>(2,new Nil<Integer>())));
	   System.out.printf("%s \n",listToString("",list1));
	   System.out.printf("%d \n",sum(0,list2));
	   
	}
}
