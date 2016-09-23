using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test.Thievery
{
    [TestClass]
    public class ThiefDesiresTests
    {
        private List<SpatialValuable> _valuables;

        [TestInitialize]
        public void Init()
        {
            _valuables = new List<SpatialValuable>();
        }

        [TestMethod]
        public void ThiefDesires_Diamonds_OnlyDiamonds()
        {
            Add("Diamond");
            Add("Sapphire");

            var desires = new ThiefDesires(_valuables, x => x.Name.Contains("Diamond")).Get();

            AssertAllMatch(desires, x => x.Name.Contains("Diamond"));
        }

        [TestMethod]
        public void ThiefDesires_MostValuableItems_Correct()
        {
            Add("Cash", Liquidity.High, 120);
            Add("Umbrella", Liquidity.Low, 10);
            Add("2013 Ford Mustang", Liquidity.Medium, 36000);

            var desires = new ThiefDesires(_valuables, new PreferenceMostValuable()).Get().ToList();

            AssertOrderedItemsAre(desires, "2013 Ford Mustang", "Cash", "Umbrella");
        }

        [TestMethod]
        public void ThiefDesires_MostValuableLiquidItems_Correct()
        {
            Add("Cash", Liquidity.High, 120);
            Add("Diamond", Liquidity.High, 3000);
            Add("Treasury Bonds", Liquidity.High, 10000);
            Add("Umbrella", Liquidity.Low, 10);
            Add("2013 Ford Mustang", Liquidity.Medium, 36000);

            var desires = new ThiefDesires(_valuables, x => x.Liquidity.Equals(Liquidity.High), 
                new PreferenceMostValuable()).Get().ToList();

            AssertOrderedItemsAre(desires, "Treasury Bonds", "Diamond", "Cash");
        }

        [TestMethod]
        public void ThiefDesires_MostValuablePaintings_Correct()
        {
            Add("Bicycle");
            Add("Painting1", 900);
            Add("Painting2", 1200);
            Add("Painting3", 520);

            var desires = new ThiefDesires(_valuables, x => x.Name.Contains("Painting"),
                new PreferenceMostValuable()).Get().ToList();

            AssertOrderedItemsAre(desires, "Painting2", "Painting1", "Painting3");
        }

        [TestMethod]
        public void ThiefDesires_MostValuableMostHidden_Correct()
        {
            Add("Classified Emails", Publicity.Confidential, 50);
            Add("Nuclear Warhead", Publicity.Confidential, 50000);
            Add("Front Sign", Publicity.Famous, 200);
            Add("Conference Room Art", Publicity.Obvious, 320);

            var desires = new ThiefDesires(_valuables, new PreferenceMostValuableSecrets()).Get().ToList();

            AssertOrderedItemsAre(desires, "Nuclear Warhead", "Classified Emails", "Conference Room Art", "Front Sign");
        }

        [TestMethod]
        public void ThiefDesires_NoSapphires_Correct()
        {
            Add("Diamond");
            Add("Sapphire");
            Add("Bicycle");

            var desires = new ThiefDesires(_valuables, x => !x.Name.Contains("Sapphire")).Get();

            AssertAllMatch(desires, x => !x.Name.Contains("Sapphire"));
        }

        private void AssertOrderedItemsAre(List<SpatialValuable> desires, params string[] items)
        {
            Assert.AreEqual(items.Length, desires.Count);
            for (var i = 0; i < items.Length; i++)
                Assert.AreEqual(items[i], desires[i].Obj.Name);
        }

        private void AssertAllMatch(IEnumerable<SpatialValuable> desires, Predicate<IValuable> condition)
        {
            foreach (var valuable in desires)
                Assert.IsTrue(condition.Invoke(valuable.Obj), $"{valuable.Obj.Name} did not match condition");
        }

        private void Add(string item)
        {
            Add(new Valuable { Name = item });
        }

        private void Add(string name, int value)
        {
            Add(new Valuable { Name = name, Value = value });
        }

        private void Add(string name, Publicity publicity, int value)
        {
            Add(new Valuable { Name = name, Publicity = publicity, Value = value });
        }

        private void Add(string name, Liquidity liquidity, int value)
        {
            Add(new Valuable { Name = name, Liquidity = liquidity, Value = value});
        }

        private void Add(IValuable valuable)
        {
            _valuables.Add(new SpatialValuable(new XYZ(0, 0, 0), Orientation.Up, valuable));
        }
    }
}
