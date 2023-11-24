using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12.Homework
{
    abstract class Car
    {
        public string Name { get; protected set; }
        public int Speed { get; protected set; }
        public int Position { get; private set; }
        public event EventHandler<string> RaceInfo; // Событие для вывода информации о гонке
        public event EventHandler<string> RaceFinished; // Событие для уведомления о завершении гонки
        public Car(string name, int speed)
        {
                Name = name;
                Speed = speed;
                Position = 0;
        }

            // Метод для выхода на старт.
        public void GoToStart()
        {
                Position = 0;
                OnRaceInfo($"{Name} is ready at the starting line.");
        }

            // Метод для начала гонки
         public void StartRace()
         {
             Random random = new Random();
             while (Position < 100)
             {
                    Position += Speed + random.Next(1, 11); // Увеличиваем позицию на случайное значение в пределах 1-10
                    OnRaceInfo($"{Name} is at position {Position}.");
             }

                OnRaceFinished($"{Name} finished the race!");
         }

            // Метод для вызова события RaceInfo
         protected virtual void OnRaceInfo(string message)
         {
                RaceInfo?.Invoke(this, message);
         }

        // Метод для вызова события RaceFinished
        protected virtual void OnRaceFinished(string message)
        {
            RaceFinished?.Invoke(this, message);
        }
    }

        // Класс "Спортивный автомобиль"
        class SportsCar : Car
        {
            public SportsCar(string name) : base(name, 10)
            {
            }
        }

        // Класс "Легковой автомобиль"
        class PassengerCar : Car
        {
            public PassengerCar(string name) : base(name, 8)
            {
            }
        }

        // Класс "Грузовой автомобиль"
        class Truck : Car
        {
            public Truck(string name) : base(name, 5)
            {
            }
        }

        // Класс "Автобус"
        class Bus : Car
        {
            public Bus(string name) : base(name, 6)
            {
            }
        }

        // Класс игры "Автомобильные гонки"
        class CarRacingGame
        {
            // Делегат для выхода на старт и начала гонки
            public delegate void RaceAction();

            // Метод для проведения гонок.
            public static void StartRace(Car[] cars)
            {
                Console.WriteLine("Let the race begin!");

                foreach (var car in cars)
                {
                    car.GoToStart();
                }

                foreach (var car in cars)
                {
                    car.RaceInfo += DisplayRaceInfo;
                    car.RaceFinished += DisplayRaceFinished;
                }

                // Делегаты для выхода на старт и начала гонки
                RaceAction goToStartDelegate = null;
                RaceAction startRaceDelegate = null;

                foreach (var car in cars)
                {
                    goToStartDelegate += car.GoToStart;
                    startRaceDelegate += car.StartRace;
                }

                // Вызов делегатов для выхода на старт и начала гонки
                goToStartDelegate?.Invoke();
                startRaceDelegate?.Invoke();

                Console.WriteLine("Race is over!");
            }

            // Метод для отображения информации о гонке
            private static void DisplayRaceInfo(object sender, string message)
            {
                Console.WriteLine(message);
            }

            // Метод для отображения информации о завершении гонки
            private static void DisplayRaceFinished(object sender, string message)
            {
                Console.WriteLine(message);
            }

            static void Main()
            {
                // Создание объектов автомобилей
                SportsCar sportsCar = new SportsCar("Sports Car");
                PassengerCar passengerCar = new PassengerCar("Passenger Car");
                Truck truck = new Truck("Truck");
                Bus bus = new Bus("Bus");

                // Создание массива автомобилей для участия в гонках
                Car[] cars = { sportsCar, passengerCar, truck, bus };

                // Запуск гонок
                StartRace(cars);
            }
        }
    }

