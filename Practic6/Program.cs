using System;
using System.Collections.Generic;
using System.Numerics;

namespace TextRoguelike
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public int Value { get; protected set; }

        protected Item(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public abstract void Use(Player player);
    }

    public class Weapon : Item
    {
        public int AttackPower { get; private set; }

        public Weapon(string name, int attackPower, int value)
            : base(name, value)
        {
            AttackPower = attackPower;
        }

        public override void Use(Player player)
        {
            player.EquipWeapon(this);
        }
    }

    public class Armor : Item
    {
        public int Defense { get; private set; }

        public Armor(string name, int defense, int value)
            : base(name, value)
        {
            Defense = defense;
        }

        public override void Use(Player player)
        {
            player.EquipArmor(this);
        }
    }

    public class Potion : Item
    {
        public int HealAmount { get; private set; }

        public Potion(string name, int healAmount, int value)
            : base(name, value)
        {
            HealAmount = healAmount;
        }

        public override void Use(Player player)
        {
            player.Heal(HealAmount);
        }
    }

    public class Chest
    {
        private static Random random = new Random();

        public Item Open()
        {
            int itemType = random.Next(3);

            switch (itemType)
            {
                case 0:
                    string[] weaponNames = { "Меч", "Топор", "Кинжал", "Булава" };
                    return new Weapon(weaponNames[random.Next(weaponNames.Length)],
                                    random.Next(5, 15),
                                    random.Next(10, 30));

                case 1:
                    string[] armorNames = { "Кольчуга", "Латы", "Кожаная броня", "Щит" };
                    return new Armor(armorNames[random.Next(armorNames.Length)],
                                   random.Next(3, 10),
                                   random.Next(8, 25));

                case 2:
                    return new Potion("Лечебное зелье", 30, 15);

                default:
                    return new Potion("Лечебное зелье", 30, 15);
            }
        }
    }

    public abstract class Enemy
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }

        protected Random random = new Random();

        public Enemy(string name, int health, int attack, int defense)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Attack = attack;
            Defense = defense;
        }

        public abstract void PerformAttack(Player player);
        public abstract void TakeDamage(int damage);
        public abstract bool IsAlive();
    }

    public class Goblin : Enemy
    {
        private double criticalChance = 0.2;

        public Goblin() : base("Гоблин", 30, 8, 3) { }

        public override void PerformAttack(Player player)
        {

        }

        public override void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public override bool IsAlive()
        {
            return Health > 0;
        }
    }

    public class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 25, 10, 2) { }

        public override void PerformAttack(Player player)
        {
            // Логика атаки скелета с игнором защиты
        }

        public override void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public override bool IsAlive()
        {
            return Health > 0;
        }
    }

    public class Mage : Enemy
    {
        private double freezeChance = 0.25; // 25% шанс заморозки

        public Mage() : base("Маг", 20, 12, 1) { }

        public override void PerformAttack(Player player)
        {
            // Логика атаки мага с шансом заморозки
        }

        public override void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public override bool IsAlive()
        {
            return Health > 0;
        }
    }

    public class VVG : Goblin
    {
        public VVG() : base()
        {
            Name = "ВВГ";
            Health = (int)(MaxHealth * 2.0);
            MaxHealth = Health;
            Attack = (int)(Attack * 1.5);
            Defense = (int)(Defense * 1.2);
        }
    }

    public class Kovalsky : Skeleton
    {
        public Kovalsky() : base()
        {
            Name = "Ковальский";
            Health = (int)(MaxHealth * 2.5);
            MaxHealth = Health;
            Attack = (int)(Attack * 1.3);
            Defense = (int)(Defense * 1.4);
        }
    }

    public class ArchmageCPP : Mage
    {
        public ArchmageCPP() : base()
        {
            Name = "Архимаг C++";
            Health = (int)(MaxHealth * 1.8);
            MaxHealth = Health;
            Attack = (int)(Attack * 1.6);
            Defense = (int)(Defense * 1.1);
        }
    }

    public class PestovC : Skeleton
    {
        public PestovC() : base()
        {
            Name = "Пестов С--";
            Health = (int)(MaxHealth * 1.3);
            MaxHealth = Health;
            Attack = (int)(Attack * 1.8);
            Defense = (int)(Defense * 0.6);
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public Weapon CurrentWeapon { get; private set; }
        public Armor CurrentArmor { get; private set; }
        public bool IsFrozen { get; set; }

        public Player(string name)
        {
            Name = name;
            MaxHealth = 100;
            Health = MaxHealth;
            // Стартовое снаряжение
            CurrentWeapon = new Weapon("Старый меч", 5, 5);
            CurrentArmor = new Armor("Кожаный доспех", 3, 5);
        }

        public void EquipWeapon(Weapon weapon)
        {
            CurrentWeapon = weapon;
        }

        public void EquipArmor(Armor armor)
        {
            CurrentArmor = armor;
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public int CalculateAttack()
        {
            return CurrentWeapon?.AttackPower ?? 5;
        }

        public int CalculateDefense()
        {
            return CurrentArmor?.Defense ?? 3;
        }
    }

    public class Game
    {
        private Player player;
        private Random random;
        private int turnCount;

        public Game(string playerName)
        {
            player = new Player(playerName);
            random = new Random();
            turnCount = 0;
        }

        public void StartGame()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру!");
            Console.WriteLine("Введите имя персонажа: ");
            string playerName = Console.ReadLine();
            Game game = new Game(playerName);
            game.StartGame();
        }
    }
}