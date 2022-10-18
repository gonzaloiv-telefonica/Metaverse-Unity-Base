using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.DataAccess;

public class MemoryDataCacheSample : MonoBehaviour
{

    [SerializeField] private Settings settings;
    [SerializeField] private List<Customer> customers;

    private void Start()
    {
        IRestClient client = new PostmanClient(settings);
        IService<Customer> service = new PostmanCustomerService(client);
        MemoryDataClient memoryDataClient = new MemoryDataClient();
        service.Get()
            .Then(customers =>
            {
                memoryDataClient.Put(customers);
                this.customers = memoryDataClient.Get<Customer>();
            })
            .Catch(Debug.LogException);
    }

}