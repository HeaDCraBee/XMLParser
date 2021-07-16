using System.Xml;

namespace TestTask.Factorys
{
    class EventTicketFactory : IOffersFactory
    {
        public IOfferInitialize CreateOffer(XmlNode allData)
        {
            return new EventTicket(allData);
        }
    }
}