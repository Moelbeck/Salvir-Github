﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using deprosa.Repository.DatabaseContext;
using deprosa.Repository.Abstract;
using deprosa.Model;
using deprosa.Interfaces;

namespace deprosa.Repository
{
    public class AdvertiserRepository : GenericRepository< Advertiser>, IAdvertiserRepository
    {
        public AdvertiserRepository(BzaleDatabaseContext context) : base(context)
        {

        }
       public Advertiser GetAdvertiser(int id)
       {
           return GetSingle(e => e.ID == id && e.Deleted==null);
       }

       public Advertiser GetAdvertiser(string name)
       {
           return GetSingle(e => e.Name.Equals(name,StringComparison.CurrentCultureIgnoreCase) && e.Deleted == null);

       }

       public IQueryable<Advertiser> Getadvertisers(int page,int size)
       {
           return Get(e => e.Deleted == null);
       }

       public Advertiser AddNewAdvertiser(Advertiser newAdvertiser)
       {
            Add(newAdvertiser);
            Save();            
           return newAdvertiser;
       }
       public Advertiser UpdateAdvertiser(Advertiser updatedAdvertiser)
       {
            updatedAdvertiser.Updated = DateTime.Now;
            Update(updatedAdvertiser);;
           return GetSingle(e => e.ID == updatedAdvertiser.ID);
       }

        public bool IsAdvertiserInDatabase(Advertiser advertiser)
        {
            return GetSingle(e => e.Name.Equals(advertiser.Name,StringComparison.CurrentCultureIgnoreCase) && e.Deleted ==null)!=null;
        }

    }
}
