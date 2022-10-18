using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.DataAccess
{

    public class ParametersBuilder
    {

        private Parameters parameters;

        public ParametersBuilder()
        {
            this.parameters = new Parameters();
        }

        public ParametersBuilder AddLimit(int limit)
        {
            parameters.limit = limit;
            return this;
        }

        public Parameters Build()
        {
            return parameters;
        }

    }

}