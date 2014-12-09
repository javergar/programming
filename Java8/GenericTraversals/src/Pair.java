public class Pair<a,b> extends Cstr {

	Pair(a arg0,b arg1) {super(2); args[0]= arg0; args[1]=arg1; nargs = 2;}
	
	@SuppressWarnings("unchecked")
	public a fst()
	{
		return (a) args[0];
	}
	
	@SuppressWarnings("unchecked")
	public b snd()
	{
		return (b) args[1];
	}
	
	public String toString()
	{
		return "Pair";
	}
}