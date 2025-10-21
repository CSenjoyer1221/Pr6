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


}