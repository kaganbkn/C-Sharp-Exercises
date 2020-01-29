using Polymorphism.Interfaces;

namespace Polymorphism
{
    public class InterfaceExample
    {
        private readonly IPerson _person;
        public InterfaceExample(IPerson person)
        {
            _person = person;
        }
        public void WriteAdd()  //static tanımlayamayız çünkü instance olmazsa hangi sınıfı kullanacağını bilemeyiz.
        {
            _person.Add();
        }
    }
}