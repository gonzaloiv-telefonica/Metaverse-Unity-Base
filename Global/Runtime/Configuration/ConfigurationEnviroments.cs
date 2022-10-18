using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meta.Configuration
{

    [CreateAssetMenu(fileName = "Configuration", menuName = "Meta/Configuration/Configuration")]
    public class ConfigurationEnviroments : ScriptableObject
    {
        public List<EnviromentVar> variables;


        public string GetHostForId(VariablesIds id)
        {
            List<EnviromentCollection> items = null;
            foreach (EnviromentVar var in variables)
            {
                if (var.variable == id)
                    items = var.host;
            }

            if (items != null)
            {
#if DEV
                foreach (EnviromentCollection item in items)
                {
                    if (item.enviroment == Eviroments.DEV)
                        return item.host;
                }
#endif

#if PRE
                    foreach(EnviromentCollection item in items)
                    {
                        if(item.enviroment == Eviroments.PRE)
                            return item.host;
                    }          
#endif

#if MAIN
                    foreach(EnviromentCollection item in items)
                    {
                        if(item.enviroment == Eviroments.MAIN)
                            return item.host;
                    }                
#endif
            }

            return "No text for " + id.ToString();
        }
    }

    [System.Serializable]
    public class EnviromentVar
    {
        public VariablesIds variable;
        public List<EnviromentCollection> host;
    }

    [System.Serializable]
    public class EnviromentCollection
    {
        public Eviroments enviroment;
        public string host;
    }




    public enum Eviroments { DEV, PRE, MAIN }
    public enum VariablesIds
    {
        ENVIROMENT_NAME,
        ANALYTICS_HOST,
        TU_COM,
    }

}
