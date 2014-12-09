public class Cstr {
	int arity;
	int nargs;
	Object[] args;
	
	Cstr(int arity)
	{
		this.arity = arity;
		nargs = 0;
		this.args = new Object[arity];
	}
	
	public boolean isPair()
	{
	  return nargs > 0;
	}
	
	public Object getX()
	{  
		Cstr cstr  = new Cstr(arity); //!Cast
		cstr.nargs = nargs -1;
		cstr.args  = args;
		return cstr;		
	}
	
	public Object getY()
	{  
		return args[nargs-1];
	}
	
}
