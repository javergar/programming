import java.util.function.BiFunction;
import java.util.function.Function;
import java.util.function.Consumer;

class PPoly<a> {

	public BiFunction<Function<Object,List<a>>,Object,List<a>> select;

	PPoly() {
		this.select = (f, o) -> {
		Function<Cstr, List<a>> body =
				(val) -> {
					if (val.isPair())
						return List.append(f.apply(val),
								(List.append(select.apply(f, val.getX()), select.apply(f, val.getY()))));
					else
						return f.apply(val);
				};
		return typecaseCstr1(o,body,f);
	};
	}

	Function<String,Integer> strLength = str -> { return str.length(); };

	public static <b> b typecaseCstr1(Object o,Function<Cstr,b> body,Function<Object,b> r)
	{
		if (o instanceof  Cstr) {
			Cstr cstr = (Cstr) o;
			return body.apply(cstr);
		}
		    return r.apply(o);
	}


	public static void update(Function<Object,Object> f,Object x)
	{
		inUpdate(f, x);
	}

	public static Object typecaseCstr0(Object o,Consumer<Cstr> body,Function<Object,Object> r)
	{
		if (o instanceof  Cstr) {
			Cstr cstr = (Cstr) o;
			body.accept(cstr);
		}
		    return o;
	}

	public static Object inUpdate(Function<Object,Object> f,Object x) {

		typecaseCstr0(x, ((cstr) -> {
			for (int i = 0; i < cstr.arity; i++) {
              cstr.args[i] = inUpdate(f,cstr.args[i]);
			}
		}), f);
		return f.apply(x);
	}


	public static String getString(Object obj) {
		  String acc = "";
		  
		  if( obj instanceof Cstr) {    // Type case
			  Cstr cstr = (Cstr) obj;   // Cast!
			  if (cstr.arity > 0) 
				  acc+="("; 
			  for (int i = 0;i< cstr.arity;i++)
			   {
				  Object aux = cstr.args[i];
				   acc += " "+getString(aux);
			   }
			  if (cstr.arity > 0) 
				  acc+=")"; 
		  }
		  
		  return obj.toString()+acc;
		}
	
	public static void pp(Object o) {
		System.out.println(getString(o));
	}
}
