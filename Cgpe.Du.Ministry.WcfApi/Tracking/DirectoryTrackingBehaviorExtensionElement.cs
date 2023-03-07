using System;
using System.ServiceModel.Configuration;

namespace Cgpe.Du.Ministry.WcfApi.Tracking
{

    public class DirectoryTrackingBehaviorExtensionElement : BehaviorExtensionElement
    {

        public override Type BehaviorType
        {
            get { return typeof(DirectoryTrackingEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new DirectoryTrackingEndpointBehavior();
        }

    }

}