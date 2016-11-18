//using System.Linq;
//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;

//namespace SecurityConsultantCore.Interactors
//{
//    public class SecurityPlacementValidator
//    {
//        private const string ContentNone = "None";
//        private const string AttachmentTrait = "Attachment:";
//        private const string OpenSpaceTrait = "OpenSpace";

//        private readonly FacilityMap _map;

//        public SecurityPlacementValidator(FacilityMap map)
//        {
//            _map = map;
//        }

//        public bool CanPlace(XYZ location, SecurityObject obj)
//        {
//            return CanPlace(location.X, location.Y, location.Z, obj);
//        }

//        public bool CanPlace(double x, double y, int z, SecurityObject obj)
//        {
//            var space = GetSpace(x, y, z);
//            if (space == FacilitySpace.Empty)
//                return false;
//            if (SlotIsTaken(obj, space))
//                return false;
//            if (CannotAttach(obj, space))
//                return false;
//            if (DoesNotHaveSufficientSpace(obj, space))
//                return false;
//            return true;
//        }

//        private bool SlotIsTaken(SecurityObject obj, FacilitySpace space)
//        {
//            var slot = obj.ObjectLayer;
//            if (slot == ObjectLayer.Unknown)
//                return true;
//            if ((slot == ObjectLayer.GroundPlaceable) && IsFilled(space.GroundPlaceable.Type))
//                return true;
//            if ((slot == ObjectLayer.LowerPlaceable) && IsFilled(space.LowerPlaceable.Type))
//                return true;
//            if ((slot == ObjectLayer.UpperPlaceable) && IsFilled(space.UpperPlaceable.Type))
//                return true;
//            return false;
//        }

//        private bool CannotAttach(SecurityObject obj, FacilitySpace space)
//        {
//            return IsAttachment(obj) && !IsAttachable(space, obj);
//        }

//        private bool IsAttachment(SecurityObject securityObject)
//        {
//            return HasTrait(securityObject, AttachmentTrait);
//        }

//        private bool IsAttachable(FacilitySpace space, SecurityObject securityObject)
//        {
//            var targets =
//                securityObject.Traits.Where(x => x.Contains(AttachmentTrait))
//                    .Select(x => x.Split(':')[1].Trim())
//                    .ToList();
//            return space.GetAll().Any(content => targets.Any(content.Type.Contains));
//        }

//        private bool DoesNotHaveSufficientSpace(SecurityObject obj, FacilitySpace space)
//        {
//            return NeedsOpenSpace(obj) && !space.IsOpenSpace;
//        }

//        private bool NeedsOpenSpace(SecurityObject securityObject)
//        {
//            return HasTrait(securityObject, OpenSpaceTrait);
//        }

//        private bool IsFilled(string spaceSlotContent)
//        {
//            return !spaceSlotContent.Equals("None");
//        }

//        private static bool HasTrait(SecurityObject securityObject, string trait)
//        {
//            return securityObject.Traits.Any(x => x.Contains(trait));
//        }

//        private FacilitySpace GetSpace(double x, double y, int z)
//        {
//            return _map[(int)x, (int)y, z];
//        }
//    }
//}