/**
 * Created by javergar on 14/11/14.
 */
public class Employee extends Person {

    Employee(String name,Integer age,Double salary) {
        super(3,name,age);
        args[2] = salary;
    }

    public Double getSalary() { return ((Double) args[2]);}
	public void setSalary(Double salary) { args[2] = salary;}

    public String toString() {
        return "Employee";
    }
}
