using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Operator_overloading
{
    //Enum, which stores names of PowerUps
    public enum PowerUps
    {
        IncreasedStrength,
        IncreasedSpeed,
        IncreasedIntelligence
    }

    public class Character : ICloneable
    {
        //Private fields with default values 
        int Strength = 27;
        int Speed = 20;
        int Intelligence = 24;
        //Constructor without parameters
        public Character() { }
        //Constructor with parameters
        public Character(int Strength, int Speed, int Intelligence)
        {
            this.Strength = Strength;
            this.Speed = Speed;
            this.Intelligence = Intelligence;
        }

        //ToString() method override
        public override string ToString()
        {
            return $"Character stats:\nStrength: {Strength}\nSpeed: {Speed}\nIntelligence: {Intelligence}\n";
        }

        //Implementation of Clone() method 
        public object Clone()
        {
            return new Character(this.Strength, this.Speed, this.Intelligence);
        }

        //Method, which will set character stats to default when the time goes up
        public void ResetStats(Character character, Character characterBeforeBuff)
        {
            character.Strength = characterBeforeBuff.Strength;
            character.Speed = characterBeforeBuff.Speed;
            character.Intelligence = characterBeforeBuff.Intelligence;
        }

        //Operator overloading
        public static Character operator +(Character character,
                PowerUp powerUp)
        {
            switch (powerUp.CurrentPowerUp)
            {
                case (int)PowerUps.IncreasedIntelligence:
                    character.Intelligence += 10;
                    break;
                case (int)PowerUps.IncreasedSpeed:
                    character.Speed += 10;
                    break;
                case (int)PowerUps.IncreasedStrength:
                    character.Strength += 10;
                    break;
                default:
                    break;
            }
            return character;
        }

        //Method, which shows character stats while power up duration time doesn't go up
        public void ShowStats(Character character, PowerUp powerUp)
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            ConsoleColor initialColor = Console.ForegroundColor;
            Random ran = new Random();

            for (int i = 0; i < powerUp.PowerUpDuration; i++)
            {
                //Randomly change color of text to see results better 
                Console.ForegroundColor = (ConsoleColor)consoleColors.GetValue(ran.Next(1, consoleColors.Length-1));
                Console.WriteLine(character.ToString());
                Thread.Sleep(1000);
            }
            //Assugning intial color to console text
            Console.ForegroundColor = initialColor;
        }
    }


    //Class PowerUp, which randomly generates powerUp and stores its value
    public class PowerUp
    {
        //Private fields
        int currentPowerUp;
        int powerUpDuration = 10;

        //Constructor
        public PowerUp()
        {
            Random ran = new Random();
            //Randomly generate power up
            currentPowerUp = ran.Next(3);
            Console.WriteLine($"Current power up: {Enum.GetName(typeof(PowerUps), currentPowerUp)}\n");
        }

        //Public properties
        public int CurrentPowerUp { get { return currentPowerUp; } }
        public int PowerUpDuration { get { return powerUpDuration; } }

    }
    class Program
    {
        static void Main(string[] args)
        {
            PowerUp pu = new PowerUp();
            Character character = new Character();
            Character characterBeforeBuff = (Character)character.Clone();
            
            character += pu;
            
            character.ShowStats(character, pu);
            character.ResetStats(character, characterBeforeBuff);
            Console.WriteLine(character.ToString());
            
            Console.ReadLine();

        }
    }
}
