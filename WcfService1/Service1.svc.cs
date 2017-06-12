using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Service1 : IService1
    {
        private static List<Auction> auctions = new List<Auction>();
        public string AddAutcion(Auction auction)
        {
            if (auction == null)
            {
                return "You are trying to add null";
            }
            int lenght = auctions.Count;
            auction.ID = lenght;
            auctions.Add(auction);
            if (auctions.Count != lenght)
            {
                return "Autcion added";
            }
            else
            {
                return "Autcion not added";
            }

        }

        public string addOffer(Offer offer, string id)
        {
            if (offer == null)
            {
                return "You are trying to put null object";
            }
            int intId = int.Parse(id);
            if (auctions != null)
            {
                int index = auctions.FindIndex(b => b.ID == intId);
                if (auctions[index] != null)
                {
                    if (auctions[index].EndDate >= DateTime.Now || auctions[index].Finished)
                    {
                        return "The auction is finished";
                    }
                    else if (auctions[index].StartPrice > offer.Price)
                    {
                        return "You are trying to add offer which have to low price";
                    }
                    int length = auctions[index].Offerts.Count;
                    offer.OfferId = length;
                    offer.Auction = intId;
                    auctions[index].Offerts.Add(offer);
                    if (auctions[index].Offerts.Count != length)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "You didn't add offer";
                    }
                }
                else { return "You picked wrong auction"; }
            }
            else { return "There is no auctions"; };
        }

        public string deleteAuction(string id)
        {
            int intId = int.Parse(id);
            int lenght = auctions.Count;
            int index = auctions.FindIndex(b => b.ID == intId);
            if (index != -1)
            {
                auctions.RemoveAt(index);
                if (auctions.Count != lenght)
                {
                    return "You removed auction";
                }
                else
                {
                    return "You didn't removed autcion";
                }
            }
            else
            {
                return "The auction don't exists";
            }
        }

        public string deleteOffer(string idAuction, string idOffer)
        {
            int auctionId = int.Parse(idAuction);
            int index = auctions.FindIndex(b => b.ID == auctionId);
            if (index != -1)
            {
                if (auctions[index].EndDate >= DateTime.Now || auctions[index].Finished)
                {
                    return "The auction is finished";
                }
                int offerId = int.Parse(idOffer);
                int indexOffer = auctions[index].Offerts.FindIndex(c => c.OfferId == offerId);
                if (indexOffer != -1)
                {
                    int length = auctions[index].Offerts.Count;
                    auctions[index].Offerts.RemoveAt(indexOffer);
                    if (auctions[index].Offerts.Count != length)
                    {
                        return "You removed your offer";
                    }
                    else
                    {
                        return "You didn't removed your offer";
                    }
                }
                else
                {
                    return "There isn't offer in this list";
                }
            }
            else
            {
                return "There isn't auction";
            }
        }

        public List<Auction> getAll()
        {
            return auctions;
        }

        public Auction getById(string id)
        {
            int intId = int.Parse(id);
            return auctions.Find(b => b.ID == intId);
        }

        public Offer getOfferById(string id, string idOffer)
        {
            int auctionId = int.Parse(id);
            int index = auctions.FindIndex(b => b.ID == auctionId);
            if (index != -1)
            {
                int offerId = int.Parse(idOffer);
                return auctions[index].Offerts.Find(c => c.OfferId == offerId);
            }
            else
            {
                return null;
            }
        }

        public List<Offer> getOffers(string id)
        {
            int auctionId = int.Parse(id);
            int index = auctions.FindIndex(b => b.ID == auctionId);
            if (index != -1)
            {
                return auctions[index].Offerts;
            }
            else
            {
                return null;
            }
        }

        public string stopAuction(string id)
        {
            int auctionId = int.Parse(id);
            int index = auctions.FindIndex(b => b.ID == auctionId);
            if (index != -1)
            {
                auctions[index].Finished = true;
                return "Auction finished";
            }
            else
            {
                return "There is no auction with this id";
            }
        }

        public string UpdateAuction(Auction autcion, string id)
        {
            int intId = int.Parse(id);
            if (autcion.ID != intId) autcion.ID = intId;
            if (auctions != null)
            {
                int index = auctions.FindIndex(b => b.ID == intId);
                if (index != -1)
                {
                    auctions[index] = autcion;
                    return "Success";
                }
                else { return "You picked wron auction"; }
            }
            else { return "You are trying to insert null object"; }
        }

        public string UpdateOffer(string idAuction, string idOffer, Offer offer)
        {
            if (offer == null)
            {
                return "You are trying to add null value";
            }
            int auctionId = int.Parse(idAuction);
            int index = auctions.FindIndex(b => b.ID == auctionId);
            if (index != -1)
            {
                if (auctions[index].EndDate >= DateTime.Now || auctions[index].Finished)
                {
                    return "The auction is finished";
                }
                int offerId = int.Parse(idOffer);
                int indexOffer = auctions[index].Offerts.FindIndex(c => c.OfferId == offerId);
                offer.Auction = index;
                if (indexOffer != -1)
                {
                    int length = auctions[index].Offerts.Count;
                    offer.OfferId = offerId;
                    auctions[index].Offerts[indexOffer] = offer;
                    if (auctions[index].Offerts.Count != length)
                    {
                        return "You updated your offer";
                    }
                    else
                    {
                        return "You didn't updated your offer";
                    }
                }
                else
                {
                    return "There isn't offer in this list";
                }
            }
            else
            {
                return "There isn't auction";
            }
        }
    }
}
