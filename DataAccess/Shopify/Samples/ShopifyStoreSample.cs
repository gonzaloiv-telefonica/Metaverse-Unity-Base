using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleGraphQL;
using Meta.DataAccess;
using Meta.FileAccess;
using Meta.FlowControl;
using UnityEngine.UI;

namespace Meta.ShopifyStore
{

    public class ShopifyStoreSample : MonoBehaviour
    {

        [SerializeField] private GraphQLConfig config;
        [SerializeField] private ProductListView view;
        [SerializeField] private ProductSpawner spawner;
        [SerializeField] private Button toProductListButton;

        private GraphQLClient client;
        private IService<Product> service;
        private RemoteFileClient fileClient;
        private ProductsPresenter presenter;

        private void Start()
        {
            this.client = new GraphQLClient(config);
            this.service = new ShopifyProductService(client, config);
            this.fileClient = new RemoteFileClient(new GlbImporter());
            this.presenter = new ProductsPresenter(service, fileClient);
            view.Init(presenter);
            spawner.Init(presenter);
            StateMachineSetup();
        }

        private void StateMachineSetup()
        {
            StateMachine stateMachine = new StateMachine();
            stateMachine.Register(new InitState(toProductListButton));
            stateMachine.Register(
                new StateBuilder<State>()
                    .Route("ProductListState")
                    .Views(view, spawner)
                    .Build()
            );
            Router.Init(stateMachine);
            Router.SetCurrentState<InitState>();
        }

    }

}
