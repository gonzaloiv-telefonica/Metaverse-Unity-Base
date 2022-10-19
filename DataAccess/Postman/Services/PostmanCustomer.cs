using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public class PostmanCustomer : IApiEntity<Customer>
    {

        public string id;
        public string name;

        public Customer ToEntity()
        {
            Customer customer = new Customer();
            customer.id = id;
            customer.fullName = name;
            return customer;
        }

    }

}