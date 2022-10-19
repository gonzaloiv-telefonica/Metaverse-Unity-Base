using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public class PostmanSample : MonoBehaviour
    {

        public Settings settings;
        public List<Customer> customers;

        private void Start()
        {
            GetCustomers();
        }

        private void GetCustomers()
        {
            IRestClient client = new PostmanClient(settings);
            IService<Customer> service = new PostmanCustomerService(client);
            service.Get()
                .Then(customers =>
                {
                    this.customers = customers;
                })
                .Catch(Debug.LogException);
        }

    }

}
