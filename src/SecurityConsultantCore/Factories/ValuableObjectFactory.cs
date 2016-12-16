using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.FacilityObjects;
using System;
using System.Collections.Generic;

namespace SecurityConsultantCore.Factories
{
    public static class ValuableObjectFactory
    {
        private static ValuableObjectFactoryContainer _container;

        public static ValuableFacilityObject Create(string type)
        {
            return GetContainer().Create(type).Invoke();
        }

        public static List<string> GetConstructables()
        {
            return GetContainer().GetConstructables();
        }

        private static ValuableObjectFactoryContainer GetContainer()
        {
            return _container ?? (_container = new ValuableObjectFactoryContainer());
        }

        private class ValuableObjectFactoryContainer : Container<Func<ValuableFacilityObject>>
        {
            protected override string GetKey(string id)
            {
                return id;
            }

            protected override Dictionary<string, Func<ValuableFacilityObject>> GetObjects()
            {
                return new Dictionary<string, Func<ValuableFacilityObject>>
                {
                    {
                        "Painting*",
                        () =>
                            new ValuableFacilityObject { Type = "Painting" + Rng(1, 11),
                                Name = "Painting",
                                Value = Rng(200, 3000),
                                Liquidity = Liquidity.Low,
                                Publicity = Publicity.Famous
                            }
                    },
                    {
                        "Table",
                        () =>
                            new ValuableFacilityObject
                            {
                                Type = "Table",
                                Name = "An Ordinary Table",
                                Value = Rng(50, 150),
                                Liquidity = Liquidity.Medium,
                                Publicity = Publicity.Obvious
                            }
                    },
                    {
                        "PaintingWideLeft1",
                        () =>
                            new ValuableFacilityObject
                            {
                                Type = "PaintingWideLeft1",
                                Name = "Large Painting",
                                Value = Rng(800, 6000),
                                Liquidity = Liquidity.Low,
                                Publicity = Publicity.Famous
                            }
                    },
                    {
                        "PaintingWideRight1",
                        () =>
                            new ValuableFacilityObject
                            {
                                Type = "PaintingWideRight1",
                                Name = "Large Painting",
                                Value = Rng(800, 6000),
                                Liquidity = Liquidity.Low,
                                Publicity = Publicity.Famous
                            }
                    },
                    {
                        "PaintingWideLeft2",
                        () =>
                            new ValuableFacilityObject
                            {
                                Type = "PaintingWideLeft2",
                                Name = "Large Painting",
                                Value = Rng(800, 6000),
                                Liquidity = Liquidity.Low,
                                Publicity = Publicity.Famous
                            }
                    },
                    {
                        "PaintingWideRight2",
                        () =>
                            new ValuableFacilityObject
                            {
                                Type = "PaintingWideRight2",
                                Name = "Large Painting",
                                Value = Rng(800, 6000),
                                Liquidity = Liquidity.Low,
                                Publicity = Publicity.Famous
                            }
                    },
                    {
                        "GarbageBin",
                        () =>
                            new ValuableFacilityObject
                            {
                                Type = "GarbageBin",
                                Name = "One Man's Trash Can",
                                Value = Rng(5, 15),
                                Liquidity = Liquidity.Low,
                                Publicity = Publicity.Confidential
                            }
                    }
                };
            }

            private int Rng(int min, int max)
            {
                return GameRandom.Random(min, max);
            }
        }
    }
}