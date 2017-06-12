using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate = "auctions/", ResponseFormat = WebMessageFormat.Json)]
        List<Auction> getAll();

        [OperationContract]
        [WebGet(UriTemplate = "auctions/{id}", ResponseFormat = WebMessageFormat.Json)]
        Auction getById(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/{id}", Method = "PUT", ResponseFormat = WebMessageFormat.Json)]
        string UpdateAuction(Auction autcion, string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string AddAutcion(Auction auction);
        [OperationContract]
        [WebGet(UriTemplate = "auctions/{id}/offers", ResponseFormat = WebMessageFormat.Json)]
        List<Offer> getOffers(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/{id}/offers", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string addOffer(Offer offer, string id);
        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/{id}/stop", Method = "PUT", ResponseFormat = WebMessageFormat.Json)]
        string stopAuction(string id);
        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/{auctionId}/offers/{offerId}", Method = "PUT", ResponseFormat = WebMessageFormat.Json)]
        string UpdateOffer(string auctionId, string offerId, Offer offer);
        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/{id}", Method = "DELETE", ResponseFormat = WebMessageFormat.Json)]
        string deleteAuction(string id);
        [OperationContract]
        [WebInvoke(UriTemplate = "auctions/{idAuction}/offers/{idOffer}", Method = "DELETE", ResponseFormat = WebMessageFormat.Json)]
        string deleteOffer(string idAuction, string idOffer);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Auction
    {
        int id;
        string name;
        double startPrice;
        bool finished;
        DateTime endDate;
        List<Offer> offerts;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public double StartPrice
        {
            get { return startPrice; }
            set { startPrice = value; }
        }

        [DataMember]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        [DataMember]
        public List<Offer> Offerts
        {
            get { return offerts; }
            set { offerts = value; }
        }
    }

    [DataContract]
    public class Offer
    {
        int offerId;
        int auction;
        double price;
        DateTime date;

        [DataMember]
        public int OfferId
        {
            get { return offerId; }
            set { offerId = value; }
        }

        [DataMember]
        public int Auction
        {
            get { return auction; }
            set { auction = value; }
        }

        [DataMember]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
