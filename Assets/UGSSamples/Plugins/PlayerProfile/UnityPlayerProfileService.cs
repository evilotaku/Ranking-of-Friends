using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Samples;
using UnityEngine;


public class UnityPlayerProfileService : IPlayerProfileService
{

    public async void SetPlayerNameAsync(string name)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
    }

    public string GetNameAsync(string playerId)
    {
        return AuthenticationService.Instance.GetPlayerNameAsync().Result;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
