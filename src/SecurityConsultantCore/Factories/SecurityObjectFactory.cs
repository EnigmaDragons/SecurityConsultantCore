using System;
using System.Collections.Generic;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Engine;
using SecurityConsultantCore.Security.Guards;

namespace SecurityConsultantCore.Factories
{
    public class SecurityObjectFactory
    {
        private readonly IFactory<IGuardBody> _bodyFactory;
        private readonly IEvents _eventNotification;
        private Dictionary<string, Func<XYZ, SecurityObjectBase>> _factories;

        public SecurityObjectFactory(IFactory<IGuardBody> bodyFactory, IEvents eventNotification)
        {
            _bodyFactory = bodyFactory;
            _eventNotification = eventNotification;
            InitFactories();
        }

        private void InitFactories()
        {
            _factories = new Dictionary<string, Func<XYZ, SecurityObjectBase>>
            {
                { "BatonSecurityGuard", xyz => new Guard(_bodyFactory.Create(), xyz, _eventNotification) { Type = "BatonSecurityGuard", ObjectLayer = ObjectLayer.GroundPlaceable} },
            };
        }

        public SecurityObjectBase Create(string type, XYZ location)
        {
            return _factories[type](location);
        }
    }
}