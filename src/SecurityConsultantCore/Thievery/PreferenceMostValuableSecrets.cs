﻿using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class PreferenceMostValuableSecrets : Preference
    {
        public PreferenceMostValuableSecrets() :
            base((x, y) => new CompoundComparer<IValuable>(
                    new PreferenceMostHidden(),
                    new PreferenceMostValuable()).Compare(x, y))
        {
        }
    }
}
