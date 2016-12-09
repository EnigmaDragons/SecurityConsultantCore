﻿using SecurityConsultantCore.Domain;
using SecurityConsultantCore.PlayerCommands;
using SecurityConsultantCore.Security.Guards;

namespace SecurityConsultantCore.Test._TestDoubles
{
    public class FakeEngineer : IEngineer
    {
        public SecurityObjectBase ConversingWith { get; private set; }

        public void IAm(Guard guard)
        {
            ConversingWith = guard;
        }
    }
}
