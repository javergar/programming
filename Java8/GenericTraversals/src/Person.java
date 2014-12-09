public class Person extends Cstr {

	Person(String name,Integer age){
		super(2);
		args[0] = name;
		args[1] = age;
	}

	protected Person(int arity,String name,int age)
	{
		super(arity);
		args[0] = name;
		args[1] = age;
	}

	public String toString()
	{
		return "Person";
	}
	public String getName() { return ((String )args[0]);}
	public void setName(String name) { args[0] = name;}
	public Integer getAge() { return ((Integer) args[1]);}
	public void setAge(Integer age) { args[1] = age;}
}
