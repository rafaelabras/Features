using System.Dynamic;
using System.Security.AccessControl;

namespace dependency_injector
{ 
    class Program
    {
        static void Main(string[] args)
        {
          var container = new dependency_injector.Container();
            var baby = new Newborn();
            var teen = new dependency_injector.Teen(baby);
            var adult = new dependency_injector.Adult(teen);
            container.Add(baby);
            container.Add(teen);
            container.Add(adult);
        }
    }

    public class Container
    {
       public Dictionary<Type, object> DependencyContainer = new Dictionary<Type, object>();

       public void AddDependency(Type dependencyType)
       {
           DependencyContainer.Add(dependencyType, Activator.CreateInstance(dependencyType));
       }

       public T Resolve<T>()
       {
           Type type = typeof(T);
           if (DependencyContainer.ContainsKey(type))
           {
               return (T)DependencyContainer[type];
           }

           return (T)Activator.CreateInstance(typeof(T));
       }
       
       public Object Resolve(Type type)
       {
           
           if (DependencyContainer.ContainsKey(type))
           {
               return DependencyContainer[type];
           }
           
           var constructor = type.GetConstructors().First();
           var parameters = constructor.GetParameters();
           var parametersarray = new object[parameters.Length];

           if (parametersarray.Length > 0)
           {
               for (int i = 0; i < parametersarray.Length; i++)
               {
                   var parametertype = parameters[i].ParameterType;
                   var dependency = Resolve(parametertype);
                   parametersarray[i] = dependency;
                   
               }
               return Activator.CreateInstance(type, parametersarray);
           }
           
           return (type) Activator.CreateInstance(type);

       }
    }
    
    
    
    public class Newborn
    {
        public Newborn()
        {
            Console.WriteLine("Hello!");
        }
    }

    public class Teen
    {
        public Newborn _person;

        public Teen(Newborn person)
        {
            _person = person;
            Console.WriteLine("Teen criando com newborn");
        }
    }

    public class Adult
    {
        public Teen _person;

        public Adult(Teen person)
        {
            _person = person;
            Console.WriteLine("Adult criado com teen e newborn");
        }
        
    }
}