using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TestTask
{
    interface IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode AllData);
    }
}