using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsters
{
    public class Monster
    {
        public enum EmotionalState
        {
            none,
            happy,
            sad,
            angry,
            bored
        }

        #region FIELDS

        private string _name;
        private int _age;
        private EmotionalState _attitude;
        private string _town;



        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public EmotionalState Attitude
        {
            get { return _attitude; }
            set { _attitude = value; }
        }


        public string Town
        {
            get { return _town; }
            set { _town = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Monster()
        {

        }

        public Monster(string name, int age, EmotionalState attitude)
        {
            _name = name;
            _age = age;
            _attitude = attitude;
        }

        #endregion

        #region METHODS
        
        public string Greeting()
        {
            string greeting;

            switch (_attitude)
            {
                
                case EmotionalState.happy:
                    greeting = $"I'm {_name} and it is a beautiful day.";
                    break;
                case EmotionalState.sad:
                    greeting = $"{_name} is sad today.";
                    break;
                case EmotionalState.angry:
                    greeting = $"{_name} is angry today.";
                    break;
                case EmotionalState.bored:
                    greeting = $"{_name} is bored.";
                    break;
                default:
                    Console.WriteLine(($"Hello, my name is {_name}"));
                    break;
            }

            return greeting;
        }




        #endregion







    }
}
