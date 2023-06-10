using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServicesRepository 
{
    private List<Service> services;
    private ServiceCreator creator;
    public ServicesRepository()
    {
        services = new List<Service>();
        creator = new ServiceCreator();
        FillServices();
    }

    public Service Get(string name)
    {
        return services.FirstOrDefault(s => s.Name.Equals(name));
    }

    private void FillServices()
    {
        var serviceParams = ParamsReader.GetServicesParams();
        services = serviceParams
            .Select(l =>creator
            .Create(l))
            .ToList();
    }
}
