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
}