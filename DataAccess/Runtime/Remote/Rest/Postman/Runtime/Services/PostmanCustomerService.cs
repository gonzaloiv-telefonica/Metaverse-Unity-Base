using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using RSG;
using UnityEngine;

namespace Meta.DataAccess
{

    public class PostmanCustomerService : IService<Customer>, IRestService
    {

        protected IRestClient client;
        public string Uri => "customers";

        public PostmanCustomerService(IRestClient client)
        {
            this.client = client;
        }

        public Promise<List<Customer>> Get()
        {
            return client.Get<Customer, PostmanCustomer>(Uri);
        }

        public Promise<List<Customer>> Get(Parameters parameters)
        {
            throw new System.NotImplementedException();
        }

    }

}