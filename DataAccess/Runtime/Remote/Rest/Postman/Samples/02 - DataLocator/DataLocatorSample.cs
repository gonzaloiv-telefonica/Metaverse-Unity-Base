using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public class DataLocatorSample : MonoBehaviour
    {

        [Header("Postman")]
        public Settings settings;
        public List<Customer> customers;

        private DataLocator dataLocator;

        private void Start()
        {
            RemoteDataProvider remoteDataProvider = new RemoteDataProvider();
            IRestClient restClient = new PostmanClient(settings);
            IService<Customer> customerService = new PostmanCustomerService(restClient);
            remoteDataProvider.Register(customerService);
            MemoryDataProvider memoryDataProvider = new MemoryDataProvider(new MemoryDataClient());
            this.dataLocator = new DataLocator(remoteDataProvider, memoryDataProvider);
            LoadData();
        }

        private void LoadData()
        {
            // Remote Load
            dataLocator.Get<Customer>()
                .Then(customers =>
                {
                    // Memory Load
                    dataLocator.Get<Customer>()
                        .Then(customers =>
                        {
                            this.customers = customers;
                        })
                        .Catch(Debug.LogException);
                })
                .Catch(Debug.LogException);
        }

    }

}