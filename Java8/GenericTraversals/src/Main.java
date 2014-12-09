import java.util.ArrayList;
import java.util.Arrays;
import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.BiFunction;


public class Main {
	
	public static Function<Object,List<Person>> isPerson = (Object o) -> {
		if( o instanceof Person) {
			return new Cons<Person>((Person) o,new Nil<>());
		}
		return new Nil<Person>();
	};

	public static Function<Object,List<Employee>> isEmp = (Object o) -> {
		if( o instanceof Employee) {
			return new Cons<Employee>((Employee) o,new Nil<>());
		}
		return new Nil<Employee>();
	};

	public static Function<Object,Object> incAge = (Object o) -> {
		if( o instanceof Person) {
			Person p = (Person) o;
			p.setAge(p.getAge()  + 1);
		}
		return o;
	};

	public static Function<Object,Object> incInts = (Object o) -> {
		if (o instanceof Integer)
			o = ( (Integer) o) + 1;
		return o;
	};

	public static Function<Object,Object> incSalary = (Object o) -> {
		if( o instanceof Employee) {
			Employee emp = (Employee) o;
			emp.setSalary(emp.getSalary()+45.00);
		}
		return o;
	};

	public static void main(String[] args) {
		List<Person> ls0 = new Cons<Person>(new Person("Paul",33),
				           new Cons<Person>(new Person("John",25),new Nil<>()));
		Pair<List<Person>,Integer> pair = new Pair<List<Person>,Integer>(ls0,5);
		Pair<Pair<List<Person>,Integer>,Person> pair1 =
				new Pair<Pair<List<Person>,Integer>,Person>(pair,new Employee("Alan",30,1000.00));


	    PPoly<Person>   p = new PPoly<Person>();
		PPoly<Employee> pe = new PPoly<Employee>();

		PPoly.pp(p.select.apply(isPerson, pair1));
		PPoly.pp(pe.select.apply(isEmp,pair1));
		PPoly.update(incAge, pair1);
		PPoly.pp(pair1);
		PPoly.update(incSalary, pair1);
		PPoly.pp(pair1);
		PPoly.update(incInts, pair1);
		PPoly.pp(pair1);
	}
}
