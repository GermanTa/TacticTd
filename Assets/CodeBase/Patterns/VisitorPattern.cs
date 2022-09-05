using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Patterns {
    
    public class VisitorsManager {
        public List<IGreetings> _animals;
        private Visitor _visitor = new Visitor();

        public void Greetings() {
            foreach (var animal in _animals) {
                animal.Visit(_visitor);
            }
        }
    }

    public class Visitor {
        public void Greetings(Dog dog){
            dog.DoVoice();
        }
        
        public void Greetings(Cat cat){
            cat.Jump();
            cat.Jump();
            cat.DoVoice();
        }
    }

    public class Dog : Animal, IGreetings  {
        public void DoVoice() {
            Debug.Log("Bark");
        }

        public void Visit(Visitor visitor) {
            visitor.Greetings(this);
        }
    }    
    
    public class Cat : Animal, IGreetings {
        public void DoVoice() {
            
        }

        public void Jump() {
            
        }
        
        public void Visit(Visitor visitor) {
            visitor.Greetings(this);
        }
    }

    public class Animal {
        
    }
    
    public interface IGreetings {
        public void Visit(Visitor visitor);
    }
}