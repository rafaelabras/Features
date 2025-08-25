using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Text;

namespace Builder
{
    public enum CarType
    {
        Sedan,
        Crossover
    }
    
    public class Car
    {
        public CarType Type;
        public int WheelSize;
        
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder()
    {
        private class Impl :
            ISpecifyCarType, 
            ISpecifyWheelSize, 
            IBuildCar
        {
            private  Car car = new Car();
            
            public Tuple<String, String> cargetter(string cartype, string wheelsize)
            {
                return new Tuple<string, string>(car.Type.ToString(), car.WheelSize.ToString());
            }
            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }
            
            public IBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"O tamanho das rodas estão errads para o {car.Type}");
                }

                car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                Console.WriteLine( cargetter(car.Type.ToString(), car.WheelSize.ToString()));
                return car;
            }
            
            
        }
        
        public static ISpecifyCarType create()
        {
            return  new Impl();
        }
        
        
    }
    
    public class StepwiseBuilder
    {
        static void Main(string[] args)
        {
         var car = CarBuilder.create()
             .OfType(CarType.Crossover)
             .WithWheels(18)
             .Build();

        }
        
    }
}

    